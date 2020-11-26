using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Helper;
using Models;
using ViewModels;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Configuration;
using MashadLeatherEcommerce.MellatWebService;
namespace MashadLeatherEcommerce.Controllers
{
    public class CityItem
    {
        public string Text { get; set; }
        public string Value { get; set; }

    }
    public class OrdersController : Controller
    {
        public static readonly string PgwSite = ConfigurationManager.AppSettings["PgwSite"];
        public static readonly string CallBackUrl = ConfigurationManager.AppSettings["CallBackUrl"];
        public static readonly string TerminalId = ConfigurationManager.AppSettings["TerminalId"];
        public static readonly string UserName = ConfigurationManager.AppSettings["UserName"];
        public static readonly string UserPassword = ConfigurationManager.AppSettings["UserPassword"];
        private DatabaseContext db = new DatabaseContext();


        #region CRUD

        [Authorize(Roles = "Administrator,SuperAdministrator,eshopadmin")]
        [Route("orders/{statusId:int}")]
        public ActionResult Index(int statusId, string status, string start, string end)
        {
            decimal amount = 0;
            List<Order> orders = new List<Order>();
            //مشاهده همه سفارشات
            if (statusId == 0)
            {
                orders = db.Orders.Include(o => o.OrderStatus).Where(o => o.IsDeleted == false).OrderByDescending(o => o.CreationDate).Include(o => o.User).Where(o => o.IsDeleted == false).OrderByDescending(o => o.CreationDate).ToList();
                if (!string.IsNullOrEmpty(status) || !string.IsNullOrEmpty(start) || !string.IsNullOrEmpty(end))
                {

                    orders = ReturnFilteredOrders(orders, status, start, end);
                    amount = ReturnTotalAmountOfFilter(orders);
                }



            }
            //مشاهده سفارشات حذف شده
            else if (statusId == 1)
            {
                orders = db.Orders.Where(current => current.IsDeleted == true).Include(o => o.OrderStatus).Where(o => o.IsDeleted == true).OrderByDescending(o => o.CreationDate).Include(o => o.User).ToList();
            }
            //مشاهده سفارشات پرداخت شده
            else if (statusId == 2)
            {
                Guid isPayedStatus = BankHelper.GetOrderStatusIdByCode(2).Value;

                orders = db.Orders
                    .Where(current => current.IsDeleted == false && current.OrderStatusId == isPayedStatus)
                    .Include(o => o.OrderStatus).OrderByDescending(o => o.CreationDate).Include(o => o.User).ToList();

            }

            //مشاهده سفارشات ارسال شده
            else if (statusId == 3)
            {
                Guid isPayedStatus = BankHelper.GetOrderStatusIdByCode(3).Value;

                orders = db.Orders
                    .Where(current => current.IsDeleted == false && current.OrderStatusId == isPayedStatus)
                    .Include(o => o.OrderStatus).OrderByDescending(o => o.CreationDate).Include(o => o.User).ToList();

            }
            ViewBag.ddlStatus = new SelectList(db.OrderStatuses, "Id", "Title");
            ViewBag.isDelete = statusId;
            ViewBag.TotalAmount = amount.ToString("n0");
            GridviewBind(orders);
            return View(orders);
        }
        public List<Order> ReturnFilteredOrders(List<Order> orders, string status, string start, string end)
        {
            List<Order> orderlist = new List<Order>();
            if (!string.IsNullOrEmpty(status) && !string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
            {

                Guid statusId = new Guid(status);
                DateTime startDate = Convert.ToDateTime(start);
                DateTime endDate = Convert.ToDateTime(end);
                ViewBag.startDate = startDate.ToShortDateString();
                ViewBag.endDate = endDate.ToShortDateString();
                ViewBag.status = statusId;
                orderlist = orders.Where(current => current.OrderStatusId == statusId
                  && (current.CreationDate >= startDate.Date && current.CreationDate <= endDate.Date)).ToList();
            }
            else if (!string.IsNullOrEmpty(status) && !string.IsNullOrEmpty(start) && string.IsNullOrEmpty(end))
            {
                Guid statusId = new Guid(status);
                DateTime startDate = Convert.ToDateTime(start);
                ViewBag.startDate = startDate.ToShortDateString();
                ViewBag.status = statusId;
                orderlist = orders.Where(current => current.OrderStatusId == statusId
                 && current.CreationDate >= startDate.Date).ToList();
            }
            else if (string.IsNullOrEmpty(status) && !string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
            {

                DateTime startDate = Convert.ToDateTime(start);
                DateTime endDate = Convert.ToDateTime(end);
                ViewBag.startDate = startDate.ToShortDateString();
                ViewBag.endDate = endDate.ToShortDateString();
                orderlist = orders.Where(current => (current.CreationDate >= startDate.Date && current.CreationDate <= endDate.Date)).ToList();

            }
            else if (!string.IsNullOrEmpty(status) && string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
            {
                Guid statusId = new Guid(status);
                DateTime endDate = Convert.ToDateTime(end);
                ViewBag.endDate = endDate.ToShortDateString();
                ViewBag.status = statusId;
                orderlist = orders.Where(current => current.OrderStatusId == statusId
                  && current.CreationDate <= endDate.Date).ToList();

            }
            else if (!string.IsNullOrEmpty(status) && string.IsNullOrEmpty(start) && string.IsNullOrEmpty(end))
            {
                Guid statusId = new Guid(status);
                ViewBag.status = statusId;
                orderlist = orders.Where(current => current.OrderStatusId == statusId).ToList();

            }
            else if (string.IsNullOrEmpty(status) && !string.IsNullOrEmpty(start) && string.IsNullOrEmpty(end))
            {

                DateTime startDate = Convert.ToDateTime(start);
                ViewBag.startDate = startDate.ToShortDateString();
                orderlist = orders.Where(current => current.CreationDate >= startDate.Date).ToList();

            }
            else if (string.IsNullOrEmpty(status) && string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
            {

                DateTime endDate = Convert.ToDateTime(end);
                ViewBag.endDate = endDate.ToShortDateString();
                orderlist = orders.Where(current => current.CreationDate <= endDate.Date).ToList();

            }
            return orderlist;
        }
        // GET: Orders/Details/5
        [Authorize(Roles = "Administrator,SuperAdministrator,eshopadmin")]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = db.Orders.Find(id);

            if (order == null)
            {
                return HttpNotFound();
            }

            List<OrderDetail> orderDetails = db.OrderDetails
                .Where(current => current.OrderId == order.Id && current.IsDeleted == false).ToList();

            ViewModels.OrderDetailViewModel orderDetailViewModel = new ViewModels.OrderDetailViewModel()
            {
                Order = order,
                OrderDetails = orderDetails
            };

            ViewBag.OrderStatusId = new SelectList(db.OrderStatuses, "Id", "Title", order.OrderStatusId);

            return View(orderDetailViewModel);
        }

        // GET: Orders/Create
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public ActionResult Create()
        {
            ViewBag.OrderStatusId = new SelectList(db.OrderStatuses, "Id", "Title");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public ActionResult Create([Bind(Include = "Id,Code,UserId,Address,TotalAmount,OrderStatusId,OrderFileUrl,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.IsDeleted = false;
                order.CreationDate = DateTime.Now;

                order.Id = Guid.NewGuid();
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderStatusId = new SelectList(db.OrderStatuses, "Id", "Title", order.OrderStatusId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", order.UserId);
            return View(order);
        }

        // GET: Orders/Edit/5
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderStatusId = new SelectList(db.OrderStatuses, "Id", "Title", order.OrderStatusId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", order.UserId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public ActionResult Edit([Bind(Include = "Id,Code,UserId,Address,TotalAmount,OrderStatusId,OrderFileUrl,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.IsDeleted = false;
                order.LastModifiedDate = DateTime.Now;
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderStatusId = new SelectList(db.OrderStatuses, "Id", "Title", order.OrderStatusId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", order.UserId);
            return View(order);
        }

        // GET: Orders/Delete/5
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public ActionResult Delete(Guid? id, int isDelete)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.isDelete = isDelete;
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public ActionResult DeleteConfirmed(Guid id, int isDelete)
        {
            Order order = db.Orders.Find(id);
            order.IsDeleted = true;
            order.DeletionDate = DateTime.Now;

            db.SaveChanges();
            return RedirectToAction("Index", new { statusId = isDelete });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        [Route("History")]
        [Authorize(Roles = "Customer")]
        public ActionResult OrderHistory()
        {
            var identity = (System.Security.Claims.ClaimsIdentity)User.Identity;
            Guid id = new Guid(identity.FindFirst(System.Security.Claims.ClaimTypes.Name).Value);


            Helper.BaseViewModelHelper baseViewModelHelper = new BaseViewModelHelper();
            OrderHistoryViewModel orderHistory = new OrderHistoryViewModel()
            {
                MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups(),
                MenuItem = baseViewModelHelper.GetMenuItems(),
                Orders = db.Orders.Where(current => current.IsDeleted == false && current.UserId == id).ToList()
            };
            return View(orderHistory);
        }

        public ActionResult ShopCart()
        {
            Helper.BaseViewModelHelper baseViewModelHelper = new BaseViewModelHelper();

            ShopCartViewModel shopCart = new ShopCartViewModel
            {

                MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups(),
                MenuItem = baseViewModelHelper.GetMenuItems(),

            };
            ViewBag.provinceId = new SelectList(db.Provinces.OrderBy(current => current.Title), "Id", "Title");
            ViewBag.cityId = ReturnCities(null);
            return View(shopCart);
        }
        public SelectList ReturnCities(Guid? id)
        {
            SelectList cities;
            if (id == null)
            {
                cities = new SelectList(db.Cities.OrderBy(current => current.Title), "Id", "Title");
            }
            else
            {
                cities = new SelectList((db.Cities.Where(c => c.ProvinceId == id).OrderBy(current => current.Title)));
            }
            return cities;
        }
        public ShopCartList GetShoppingCartInfo(string jsonvar)
        {
            try
            {
                //string currency = (System.Configuration.ConfigurationManager.AppSettings["currency"]);

                decimal totalPrice = 0;
                List<ShopCartItemViewModel> shopCartItems = new List<ShopCartItemViewModel>();

                string[] orderDetailItems = jsonvar.Split('/');

                for (int i = 0; i < orderDetailItems.Length - 1; i++)
                {
                    string[] orderDetailCount = orderDetailItems[i].Split('.');
                    if (orderDetailCount[0].Contains("undefined"))
                    {
                        orderDetailCount[0] = orderDetailCount[0].Replace("undefined", "");
                    }
                    Product product = db.Products.Find(new Guid(orderDetailCount[0]));

                    decimal amount = 0;

                    if (product.IsInPromotion)
                    {
                        if (orderDetailCount[2] != "undefined" && orderDetailCount[2] != "nocolor")
                        {
                            Guid colorIdGuid = new Guid(orderDetailCount[2]);
                            Product productItem = db.Products.FirstOrDefault(current =>
                                    current.ParentId == product.Id && current.ColorId == colorIdGuid && current.IsDeleted == false);

                            if (productItem?.DiscountAmount != null)
                                amount = productItem.DiscountAmount.Value;
                        }
                        else if (product.DiscountAmount != null)
                            amount = product.DiscountAmount.Value;
                    }
                    else if (product.Amount != null)
                        amount = product.Amount.Value;


                    // Color color = db.Colors.Find(new Guid(orderDetailCount[2]));

                    ShopCartItemViewModel currentShopItem = shopCartItems.Where(current =>
                        current.Id == orderDetailCount[0] && current.color == orderDetailCount[2] &&
                        current.size == orderDetailCount[3]).FirstOrDefault();

                    if (currentShopItem == null)
                    {
                        string sizeTitle = string.Empty;
                        if (orderDetailCount[3] != "undefined")
                            sizeTitle = GetSizeTitle(orderDetailCount[3]);

                        string colorTitle = string.Empty;
                        if (orderDetailCount[2] != "undefined")
                            colorTitle = GetColorTitle(orderDetailCount[2]);


                        shopCartItems.Add(new ShopCartItemViewModel()
                        {
                            Id = orderDetailCount[0],
                            Title = product.TitleSrt,
                            ImageUrl = product.ImageUrl,
                            Amount = (amount * Convert.ToDecimal(orderDetailCount[1])).ToString().Split('.')[0],
                            Price = String.Format("{0:n0}", amount),
                            Qty = orderDetailCount[1],
                            color = orderDetailCount[2],
                            size = orderDetailCount[3],
                            colorTitle = colorTitle,
                            SizeTitle = sizeTitle
                        });
                    }
                    else
                    {
                        currentShopItem.Qty = (Convert.ToInt32(currentShopItem.Qty) + Convert.ToInt32(orderDetailCount[1])).ToString();
                    }
                    totalPrice = (decimal)(totalPrice + (amount * Convert.ToDecimal(orderDetailCount[1])));
                }

                //String.Format("{0:n0}", product.Amount);

                decimal shippmentprice = 0;

                decimal shippmetFreeLimit =
                    Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings["shippmentFree"]);

                if (totalPrice < shippmetFreeLimit && totalPrice > 0)
                    shippmentprice = Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings["shippment"]);

                decimal discountAmount = GetDiscountAmount(totalPrice, shopCartItems);
                ShopCartList shopCart = new ShopCartList
                {
                    ShopCartItems = shopCartItems,
                    ShippmentPrice = String.Format("{0:n0}", shippmentprice),
                    Amount = String.Format("{0:n0}", totalPrice),
                    Discount = discountAmount.ToString("n0"),
                    TotalPayment = String.Format("{0:n0}", shippmentprice + totalPrice - discountAmount)
                };

                return shopCart;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public decimal GetDiscountAmount(decimal total, List<ShopCartItemViewModel> shopCartProducts)
        {
            decimal newTotal = total;
            StepDiscount stepDiscount =
                db.StepDiscounts.FirstOrDefault(current => current.IsActive && current.IsDeleted == false);

            if (stepDiscount != null)
            {
                List<StepDiscountDetail> stepDiscountDetails = db.StepDiscountDetails
                    .Where(current => current.StepDiscountId == stepDiscount.Id)
                    .OrderByDescending(current => current.TargetValue).ToList();

                foreach (ShopCartItemViewModel product in shopCartProducts)
                {
                    Guid productId = new Guid(product.Id);

                    Product oProduct = db.Products.Find(productId);

                    if (oProduct != null)
                    {
                        if (oProduct.IsInPromotion)
                        {
                            if (oProduct.DiscountAmount != null)
                                newTotal = newTotal - oProduct.DiscountAmount.Value;
                        }
                    }
                }
                decimal discount = 0;
                for (int i = 0; i < stepDiscountDetails.Count; i++)
                {
                    if (total > stepDiscountDetails[i].TargetValue)
                    {
                        discount = newTotal * stepDiscountDetails[i].DiscountPercent / 100;
                        break;
                    }
                }

                return discount;
            }
            else
                return 0;
        }

        public string GetSizeTitle(string sizeId)
        {
            if (sizeId == "nosize")
            {
                return "-";
            }
            else
            {
                string sizeTitle = db.Sizes.Find(new Guid(sizeId))?.Title;
                return sizeTitle;
            }
        }


        public string GetColorTitle(string colorId)
        {
            if (colorId == "nocolor")
            {
                return "-";
            }
            else
            {
                string colorTitle = db.Colors.Find(new Guid(colorId))?.TitleSrt;
                return colorTitle;
            }
        }
        public ActionResult LoadShopCart(string jsonvar)
        {
            return Json(GetShoppingCartInfo(jsonvar), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Finalize(string jsonvar, string firstName, string lastName, string cellNumber, string email, string province, string city, string address, string phone, string postalCode, string bank,string paymentType)
        {
            try
            {
                cellNumber = cellNumber.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4").Replace("۵", "5").Replace("۶", "6").Replace("v", "7").Replace("۸", "8").Replace("۹", "9");

                bool isValidMobile = Regex.IsMatch(cellNumber, @"(^(09|9)[0123456789][0123456789]\d{7}$)|(^(09|9)[0123456789][0123456789]\d{7}$)", RegexOptions.IgnoreCase);

                if (paymentType != "online" && city != "2c730dce-774d-4007-88a9-4acb1dd48cea" &&
                    city != "88e17e07-d8fc-4989-96b6-a9fbf394b521")
                {
                    return Json("invalidPaymentType", JsonRequestBehavior.AllowGet);

                }
                else { 

                if (isValidMobile)
                {
                    bool isEmail = Regex.IsMatch(email,
                        @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                        RegexOptions.IgnoreCase);

                    if (isEmail)
                    {
                        ShopCartList shopCart = GetShoppingCartInfo(jsonvar);

                        User user = db.Users
                            .FirstOrDefault(current => current.IsDeleted == false && current.CellNum == cellNumber);

                        if (user == null)
                        {
                            user = new User();

                            user.CellNum = cellNumber;
                            user.Code = GenerateUserCode();
                            user.Address = address;
                            user.CityId = new Guid(city);
                            user.FirstName = firstName;
                            user.LastName = lastName;
                            user.Email = email;
                            user.RoleId = new Guid("0AEB583A-E4E2-44D6-92AA-39E7D2480127");
                            user.IsActive = true;
                            user.CreationDate = DateTime.Now;
                            user.IsDeleted = false;
                            user.Phone = phone;
                            user.PostalCode = postalCode;

                            db.Users.Add(user);

                            //db.SaveChanges();
                        }

                        Order order = new Order()
                        {
                            Code = GenerateOrderCode(),
                            UserId = user.Id,
                            Address = address,
                            TotalAmount = Convert.ToDecimal(shopCart.TotalPayment),
                            OrderStatusId = BankHelper.GetOrderStatusIdByCode(1).Value,
                            IsActive = true,
                            CreationDate = DateTime.Now,
                            IsDeleted = false,
                            CityId = new Guid(city),
                            BankName = bank,
                            PaymentType = paymentType
                        };
                        db.Orders.Add(order);

                        //     db.SaveChanges();
                        foreach (ShopCartItemViewModel item in shopCart.ShopCartItems)
                        {
                            //Guid productId = new Guid(item.Id);
                            Guid parentId = new Guid(item.Id);
                            Guid? colorId = null;

                            if (item.color != "nocolor")
                                colorId = new Guid(item.color);

                            string size = item.size;
                            int quantity = Convert.ToInt32(item.Qty);
                            //Product product = db.Products.Find(productId);
                            Product product = new Product();
                            decimal amount = 0;
                            if (size != "nosize")
                            {
                                Guid sizeId = new Guid(item.size);

                                if (colorId != null)
                                    product = db.Products
                                        .FirstOrDefault(current => current.ParentId == parentId && current.ColorId == colorId &&
                                            current.SizeId == sizeId);

                                if (product != null)
                                {
                                    if (product.IsInPromotion)
                                        amount = product.DiscountAmount.Value;
                                    else
                                        amount = product.Amount.Value;
                                    if (product.Quantity < quantity)
                                        return Json("invalidQty| " + InvalidQuantityMessage(product),
                                            JsonRequestBehavior.AllowGet);
                                }
                            }

                            else
                            {
                                //محصولات عادی
                                if (colorId != null)
                                    product = db.Products
                                        .FirstOrDefault(current =>
                                            current.ParentId == parentId && current.ColorId == colorId &&
                                            current.SizeId == null);

                                //محصولات مراقبت از چرم و جیر
                                else
                                    product = db.Products
                                        .FirstOrDefault(current => current.Id == parentId && current.ColorId == colorId &&
                                                                   current.SizeId == null);


                                if (product.IsInPromotion)
                                    amount = product.DiscountAmount.Value;
                                else
                                    amount = product.Amount.Value;

                                if (product != null)
                                {
                                    if (product.Quantity < quantity)
                                        return Json("invalidQty| " + InvalidQuantityMessage(product),
                                            JsonRequestBehavior.AllowGet);
                                }
                            }
                            OrderDetail orderDetail = new OrderDetail()
                            {
                                OrderId = order.Id,
                                ProductId = product.Id,
                                Quantity = Convert.ToInt32(item.Qty),
                                Price = amount,
                                Amount = amount * Convert.ToInt32(item.Qty),
                                IsActive = true,
                                IsDeleted = false,
                                CreationDate = DateTime.Now,
                            };
                            db.OrderDetails.Add(orderDetail);
                            //db.SaveChanges();
                        }
                        db.SaveChanges();

                        //CreateKianCustomer(user);


                        //قسمت ارسال اس ام اس به مدیران
                        //       Guid superAdministrator = new Guid("c0474d55-c607-498b-bf0d-219b244a9dc9");
                        //       Guid administrator = new Guid("e407f264-d292-400d-b3ad-44675a1e4e97");
                        //       string nextLine = "\n";
                        //       string message = "چرم مشهد" + nextLine +
                        //"سفارشی با شماره  " + order.Code.ToString() + " با موفقیت ثبت گردید. ";

                        //       List<User> users = db.Users.Where(current => current.IsActive == true && current.IsDeleted == false && (current.RoleId == superAdministrator || current.RoleId == administrator)).ToList();
                        //       foreach (var item in users)
                        //       {
                        //           SmsMessageHelper.SendSms(item.CellNum, message);
                        //       }

                        //       db.SaveChanges();
                        string uniqueOrderId = GetUniqueOrderId(order.Id);
                        if (paymentType == "online")
                        {
                            if (bank == "mellat")
                            {
                                string log = PayRequest(order.Id, uniqueOrderId, order.TotalAmount.ToString());
                                // string log = PayRequest(order.Id, uniqueOrderId, "3000");
                                if (!log.Contains("false"))
                                {
                                    return Json("true-" + log, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json("bankerror|" + log.Split('-')[1], JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                return Json("true-" + uniqueOrderId, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                                    return Json("true-offlinepayment", JsonRequestBehavior.AllowGet);

                        }
                    }
                    else
                        return Json("invalidEmail", JsonRequestBehavior.AllowGet);
                }

                else
                    return Json("invalidCellNumber", JsonRequestBehavior.AllowGet);
            }
            }
            catch (Exception e)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        public string InvalidQuantityMessage(Product product)
        {
            string colorTitle = product.Color.Title;
            string quantity = product.Quantity.ToString().Split('.')[0];
            string title = product.Title;
            string message;

            if (product.Size != null)
            {
                string sizeTitle = product.Size.Title;
                message = "حداکثر موجودی قابل سفارش محصول " + title + " رنگ " + colorTitle + " سایز " + sizeTitle +
                          " برابر است با " + quantity + " عدد";
            }
            else
                message = "حداکثر موجودی قابل سفارش محصول " + title + " رنگ " + colorTitle +
                          " برابر است با " + quantity + " عدد";

            return message;
        }
        public int GenerateUserCode()
        {
            return FindeLastUserCode() + 1;
        }

        public int FindeLastUserCode()
        {
            User user = db.Users.Where(current => current.IsDeleted == false).OrderByDescending(current => current.Code).FirstOrDefault();

            if (user != null)
                return user.Code;
            else
                return 999;
        }


        public int GenerateOrderCode()
        {
            return FindeLastOrderCode() + 1;
        }

        public int FindeLastOrderCode()
        {
            Order order = db.Orders.Where(current => current.IsDeleted == false).OrderByDescending(current => current.Code).FirstOrDefault();

            if (order != null)
                return order.Code;
            else
                return 9999;
        }

        public ActionResult FillCities(string id)
        {
            Guid provinceId = new Guid(id);
            //   ViewBag.cityId = ReturnCities(provinceId);
            var cities = db.Cities.Where(c => c.ProvinceId == provinceId).OrderBy(current => current.Title).ToList();
            List<CityItem> cityItems = new List<CityItem>();
            foreach (City city in cities)
            {
                cityItems.Add(new CityItem()
                {
                    Text = city.Title,
                    Value = city.Id.ToString()
                });
            }
            return Json(cityItems, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ChangeStatus(string orderId, string statusId)
        {
            try
            {
                Guid orderIdGuid = new Guid(orderId);
                Guid statusIdGuid = new Guid(statusId);
                Order order = db.Orders.Find(orderIdGuid);
                order.OrderStatusId = statusIdGuid;
                order.LastModifiedDate = DateTime.Now;
                db.SaveChanges();

                return Json("true", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }

        }


        #region Payment


        public string GetUniqueOrderId(Guid orderId)
        {
            PaymentUniqeCodes paymentUniqeCodes = new PaymentUniqeCodes()
            {
                OrderId = orderId
            };

            db.PaymentUniqeCodes.Add(paymentUniqeCodes);
            db.SaveChanges();
            return paymentUniqeCodes.Id.ToString();
        }

        public string PayRequest(Guid orderId, string uniqueOrderId, string price)
        {
            try
            {
                string payDate = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
                string payTime = DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');

                string result;

                BankHelper.BypassCertificateError();

                PaymentGatewayClient bpService = new PaymentGatewayClient();

                Int64 uniqueorderIdInt = Int64.Parse(uniqueOrderId);

                result = bpService.bpPayRequest(
                                  Int64.Parse(TerminalId),
                                  UserName,
                                  UserPassword,
                                  uniqueorderIdInt,
                                  Convert.ToInt64(price),
                                  payDate,
                                  payTime,
                                 null,
                               CallBackUrl,
                                 0);

                if (result != null)
                {
                    // 45zm24554654,0 
                    string[] ResultArray = result.Split(',');

                    if (ResultArray[0].ToString() == "0")
                    {
                        return ResultArray[1];
                    }
                    else
                    {
                        BankHelper.UpdatePayment(orderId, ResultArray[0], 0, null, false);

                        //  UpdatePayment(paymentid, ResultArray[0].ToString(), 0, null, false);
                        return "false-" + BankHelper.MellatResult(ResultArray[0]);
                    }
                }
                return "false-" + BankHelper.MellatResult(result);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        //public string TestDargah()
        //{
        //    string PayDate = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
        //    string PayTime = DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');
        //   BankHelper.BypassCertificateError();

        //    //   var payment = new ir.shaparak.bpm.PaymentGatewayImplService();
        //    MashadLeatherEcommerce.MellatWebService.PaymentGatewayClient bpService = new MashadLeatherEcommerce.MellatWebService.PaymentGatewayClient();

        //    string result = bpService.bpPayRequest(
        //        Int64.Parse(TerminalId),
        //        UserName,
        //        UserPassword,
        //      999,
        //        1000,
        //        PayDate,
        //        PayTime,
        //        "خرید از سایت تحلیل داده",
        //        "",
        //       Int64.Parse("0")
        //    );
        //    if (result != null)
        //    {
        //        // 45zm24554654,0 

        //        return BankHelper.PostRequest("https://bpm.shaparak.ir/pgwchannel/startpay.mellat", "45zm24554654");
        //    }
        //    else
        //    {
        //        ViewBag.Message = "امکان اتصال به درگاه بانک وجود ندارد";
        //        return "false";
        //    }
        //}


        #endregion
        public ActionResult FilteredList(string startDate, string endDate, string selectedStatus)
        {
            try
            {

                return Json("true", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }

        }

        public decimal ReturnTotalAmountOfFilter(List<Order> orders)
        {
            decimal amount = 0;
            foreach (Order order in orders)
            {
                amount = amount + order.TotalAmount;
            }
            return amount;
        }

        public void GridviewBind(List<Order> orders)
        {
            List<ExcelGridviewViewModel> gridList = new List<ExcelGridviewViewModel>();
            foreach (Order order in orders)
            {
                string[] totalAmount = order.TotalAmount.ToString("n0").Split('/');
                gridList.Add(new ExcelGridviewViewModel
                {
                    Code = order.Code,
                    SaleReferenceId = order.SaleReferenceId.ToString(),
                    OrderStatus = order.OrderStatus.Title,
                    FirstName = order.User.FirstName,
                    LastName = order.User.LastName,
                    CellNum = order.User.CellNum,
                    CityTitle = order.User.City.Title,
                    Address = order.User.Address,
                    TotalAmount = totalAmount[0],
                    CreationDate = order.CreationDate

                });
            }

            GridView gv = new GridView();
            gv.DataSource = gridList;
            gv.DataBind();
            gv.HeaderRow.Cells[0].Text = "کد سفارش";
            gv.HeaderRow.Cells[1].Text = "کد رهگیری پرداخت";
            gv.HeaderRow.Cells[2].Text = "وضعیت سفارش";
            gv.HeaderRow.Cells[3].Text = "نام";
            gv.HeaderRow.Cells[4].Text = "نام خانوادگی";
            gv.HeaderRow.Cells[5].Text = "موبایل";
            gv.HeaderRow.Cells[6].Text = "شهر";
            gv.HeaderRow.Cells[7].Text = "آدرس";
            gv.HeaderRow.Cells[8].Text = "جمع کل سفارش";
            gv.HeaderRow.Cells[9].Text = "تاریخ";

            Session["orders"] = gv;

            //gv.DataSource = gridList;
            //gv.DataBind();
            //Session["orders"] = gv;

        }

        public ActionResult Download()
        {
            if (Session["orders"] != null)
            {
                return new DownloadFileActionResult((GridView)Session["orders"], "orders.xls");
            }
            else
            {
                return null;
            }
        }


        public ActionResult DownloadImages()
        {
            GridviewImageBind();
            if (Session["productwhitoutimage"] != null)
            {
                return new DownloadFileActionResult((GridView)Session["productwhitoutimage"], "products.xls");
            }
            else
            {
                return null;
            }
        }
        public void GridviewImageBind()
        {
            List<ExcelGridviewForImageViewModel> gridList = new List<ExcelGridviewForImageViewModel>();

            List<Models.Product> products = db.Products.Where(current =>
                current.ParentId == null && current.IsDeleted == false &&
                (current.ImageUrl == null || current.ImageUrl == "")).ToList();

            foreach (Product product in products)
            {
                gridList.Add(new ExcelGridviewForImageViewModel
                {
                    ProductCode = product.Barcode
                });
            }

            GridView gv = new GridView();
            gv.DataSource = gridList;
            gv.DataBind();
            gv.HeaderRow.Cells[0].Text = "بارکد محصولات بدون عکس";


            Session["productwhitoutimage"] = gv;

            //gv.DataSource = gridList;
            //gv.DataBind();
            //Session["orders"] = gv;

        }

        #region Tracking / پیگیری سفارش
        public ActionResult OrderTracking()
        {
            TempData["WrongReference"] = null;
            ViewBag.ShowDetail = "";
            Helper.BaseViewModelHelper baseViewModelHelper = new BaseViewModelHelper();
            OrderTrackingDetailViewModel orderTracking = new OrderTrackingDetailViewModel()
            {
                MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups(),
                MenuItem = baseViewModelHelper.GetMenuItems(),
            };
            return View(orderTracking);
        }
        [HttpPost]
        public ActionResult OrderTracking(OrderTrackingDetailViewModel order)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(order.ReferenceId))
                {
                    order.ReferenceId = order.ReferenceId.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4").Replace("۵", "5").Replace("۶", "6").Replace("v", "7").Replace("۸", "8").Replace("۹", "9");

                    long referenceId = Convert.ToInt64(order.ReferenceId);
                    Order orderTracking = db.Orders.Where(current => current.SaleReferenceId == referenceId
                    && current.IsDeleted == false && current.IsActive == true).FirstOrDefault();
                    if (orderTracking == null)
                    {
                        ViewBag.ShowDetail = "";
                         TempData["WrongReference"] = "شماره پیگیری صحیح نمی باشد";
                    }
                    else
                    {
                        User user = db.Users.Where(current => current.Id == orderTracking.UserId).FirstOrDefault();
                        order.CustomerName = user.FirstName + " " + user.LastName;
                        order.OrderDate = orderTracking.CreationDate;
                        OrderStatus orderStatus = db.OrderStatuses.Where(current => current.Id == orderTracking.OrderStatusId).FirstOrDefault();
                        order.Status = orderStatus.Title;
                        ViewBag.ShowDetail = "show";
                    }
                }
                else
                {
                    ViewBag.ShowDetail = "";
                    TempData["WrongReference"] = "شماره پیگیری را وارد نمایید";
                }
            }
            Helper.BaseViewModelHelper baseViewModelHelper = new BaseViewModelHelper();
            order.MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups();
            order.MenuItem = baseViewModelHelper.GetMenuItems();
            return View(order);
        }
      
        #endregion Tracking / پیگیری سفارش
    }
}
