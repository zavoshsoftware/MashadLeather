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
            ViewBag.vb1 = statusId;
            ViewBag.vb2 = status;
            ViewBag.vb3 = start;
            ViewBag.vb4 = end;

            decimal amount = 0;
            List<OrderListViewModel> orders = GetOrders(statusId, status, start, end);

            if (!string.IsNullOrEmpty(status) || !string.IsNullOrEmpty(start) || !string.IsNullOrEmpty(end))
                amount = ReturnTotalAmountOfFilter(orders);

            ViewBag.ddlStatus = new SelectList(db.OrderStatuses, "Id", "Title");
            ViewBag.isDelete = statusId;
            ViewBag.TotalAmount = amount.ToString("n0");
            //   GridviewBind(orders);
            return View(orders);
        }

        public List<OrderListViewModel> GetOrders(int statusId, string status, string start, string end)
        {
            List<OrderListViewModel> orders = new List<OrderListViewModel>();
            //مشاهده همه سفارشات
            if (statusId == 0)
            {
                orders = db.Orders.AsNoTracking()
                    .Where(current => current.IsDeleted == false)
                  .OrderByDescending(o => o.CreationDate).Select(

                        x => new OrderListViewModel()
                        {
                            Code = x.Code,
                            SaleReferenceId = x.SaleReferenceId,
                            OrderStatusTitle = x.OrderStatus.Title,
                            FirstName = x.User.FirstName,
                            LastName = x.User.LastName,
                            CellNum = x.User.CellNum,
                            TotalAmount = x.TotalAmount,
                            CreationDate = x.CreationDate,
                            Id = x.Id,
                            City = x.User.City.Title,
                            Address = x.Address,
                            PaymentType = x.PaymentType,
                            OrderStatusId = x.OrderStatusId,
                            PaymentAmount = x.PaymentAmount

                        }).ToList();


                if (!string.IsNullOrEmpty(status) || !string.IsNullOrEmpty(start) || !string.IsNullOrEmpty(end))
                {

                    orders = ReturnFilteredOrders(orders, status, start, end);
                }



            }
            //مشاهده سفارشات حذف شده
            else if (statusId == 1)
            {
                orders = db.Orders.AsNoTracking()
                    .Where(current => current.IsDeleted == true)
                    .Include(o => o.OrderStatus).OrderByDescending(o => o.CreationDate).Include(o => o.User).Select(

                        x => new OrderListViewModel()
                        {
                            Code = x.Code,
                            SaleReferenceId = x.SaleReferenceId,
                            OrderStatusTitle = x.OrderStatus.Title,
                            FirstName = x.User.FirstName,
                            LastName = x.User.LastName,
                            CellNum = x.User.CellNum,
                            TotalAmount = x.TotalAmount,
                            CreationDate = x.CreationDate,
                            Id = x.Id,
                            City = x.User.City.Title,
                            Address = x.Address,
                            PaymentType = x.PaymentType,
                            OrderStatusId = x.OrderStatusId,
                            PaymentAmount = x.PaymentAmount

                        }).ToList();
            }
            //مشاهده سفارشات پرداخت شده
            else if (statusId == 2)
            {

                Guid isPayedStatus = BankHelper.GetOrderStatusIdByCode(2).Value;

                orders = db.Orders.AsNoTracking()
                    .Where(current => current.OrderStatusId == isPayedStatus && current.IsDeleted == false)
                    .Include(o => o.OrderStatus).OrderByDescending(o => o.CreationDate).Include(o => o.User).Select(

                        x => new OrderListViewModel()
                        {
                            Code = x.Code,
                            SaleReferenceId = x.SaleReferenceId,
                            OrderStatusTitle = x.OrderStatus.Title,
                            FirstName = x.User.FirstName,
                            LastName = x.User.LastName,
                            CellNum = x.User.CellNum,
                            TotalAmount = x.TotalAmount,
                            CreationDate = x.CreationDate,
                            Id = x.Id,
                            City = x.User.City.Title,
                            Address = x.Address,
                            PaymentType = x.PaymentType,
                            OrderStatusId = x.OrderStatusId,
                            PaymentAmount = x.PaymentAmount

                        }).ToList();



            }

            //مشاهده سفارشات ارسال شده
            else if (statusId == 3)
            {
                Guid isPayedStatus = BankHelper.GetOrderStatusIdByCode(3).Value;

                orders = db.Orders.AsNoTracking()
                    .Where(current => current.OrderStatusId == isPayedStatus && current.IsDeleted == false)
                    .Include(o => o.OrderStatus).OrderByDescending(o => o.CreationDate).Include(o => o.User).Select(

                        x => new OrderListViewModel()
                        {
                            Code = x.Code,
                            SaleReferenceId = x.SaleReferenceId,
                            OrderStatusTitle = x.OrderStatus.Title,
                            FirstName = x.User.FirstName,
                            LastName = x.User.LastName,
                            CellNum = x.User.CellNum,
                            TotalAmount = x.TotalAmount,
                            CreationDate = x.CreationDate,
                            Id = x.Id,
                            City = x.User.City.Title,
                            Address = x.Address,
                            PaymentType = x.PaymentType,
                            OrderStatusId = x.OrderStatusId,
                            PaymentAmount = x.PaymentAmount

                        }).ToList();

            }

            return orders;
        }

        public List<OrderListViewModel> ReturnFilteredOrders(List<OrderListViewModel> orders, string status, string start, string end)
        {
            List<OrderListViewModel> orderlist = new List<OrderListViewModel>();
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


            var identity = (System.Security.Claims.ClaimsIdentity)User.Identity;
            string role = (identity.FindFirst(System.Security.Claims.ClaimTypes.Role).Value);

            if (role == "Administrator" || role == "SuperAdministrator")
            {

                ViewBag.OrderStatusId = new SelectList(db.OrderStatuses.OrderBy(c => c.Code), "Id", "Title", order.OrderStatusId);
            }
            else
            {
                Guid canselOrderStatusId = new Guid("D563EBA9-DFB4-4AE6-AEA6-8801CC37B0D4");
                if (order.OrderStatusId == canselOrderStatusId)
                {
                    ViewBag.OrderStatusId = new SelectList(db.OrderStatuses.Where(c => c.Id == canselOrderStatusId).OrderBy(c => c.Code), "Id", "Title", order.OrderStatusId);
                }
                else
                    ViewBag.OrderStatusId = new SelectList(db.OrderStatuses.OrderBy(c => c.Code), "Id", "Title", order.OrderStatusId);

            }
            ViewBag.ExitInventory = new SelectList(GetExitInventoryList(order.ExitInventory), "Value", "Title", order.ExitInventory);

            return View(orderDetailViewModel);

        }

        public List<DropdownCustomViewModel> GetExitInventoryList(string selected)
        {
            List<DropdownCustomViewModel> result = new List<DropdownCustomViewModel>();

            //result.Add(new DropdownCustomViewModel()
            //{
            //    Value = "0",
            //    Title = "انتخاب انبار",
            //    IsSelected = false
            //});
            result.Add(new DropdownCustomViewModel()
            {
                Value = "1",
                Title = "تهران",
                IsSelected = false
            });
            result.Add(new DropdownCustomViewModel()
            {
                Value = "2",
                Title = "مشهد",
                IsSelected = false
            });

            if (selected == "1")
            {
                var teh = result.FirstOrDefault(c => c.Value == "1");
                teh.IsSelected = true;
            }
            else if (selected == "2")
            {
                var mashad = result.FirstOrDefault(c => c.Value == "2");
                mashad.IsSelected = true;
            }
            //else
            //{
            //    var other = result.FirstOrDefault(c => c.Value == "0");
            //    other.IsSelected = true;
            //}

            return result;
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
                Orders = db.Orders.Where(current => current.IsDeleted == false && current.UserId == id).OrderByDescending(c => c.CreationDate).ToList()
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

        public List<ShopCartItemViewModel> UpdateShopCartByAvailability(List<ShopCartItemViewModel> shopCartItems)
        {
            foreach (ShopCartItemViewModel product in shopCartItems.ToList())
            {
                Guid id = new Guid(product.Id);

                if (int.Parse(product.Qty) > 1)
                {
                    if (product.Price == product.Amount)
                    {
                        decimal newAmount = Convert.ToDecimal(product.Price) * Convert.ToDecimal(product.Qty);
                        product.Amount = newAmount.ToString("n0");
                }
                }

                if (product.SizeTitle != "-" && product.colorTitle != "-")
                {
                    Guid sizeId = new Guid(product.size);
                    Guid colorId = new Guid(product.color);

                    if (!db.Products.Any(c =>
                        c.ParentId == id && c.SizeId == sizeId && c.ColorId == colorId && c.IsAvailable &&
                        c.IsDeleted == false && c.IsActive && c.Quantity > 0))
                    {
                        shopCartItems.Remove(product);
                    }
                }

                else if (product.SizeTitle == "-" && product.colorTitle != "-")
                {
                    Guid colorId = new Guid(product.color);

                    if (!db.Products.Any(c =>
                        c.ParentId == id && c.ColorId == colorId && c.IsAvailable &&
                        c.IsDeleted == false && c.IsActive && c.Quantity > 0))
                    {
                        shopCartItems.Remove(product);
                    }
                }

                else if (product.SizeTitle == "-" && product.colorTitle == "-")
                {
                    if (!db.Products.Any(c =>
                        c.Id == id && c.IsAvailable &&
                        c.IsDeleted == false && c.IsActive && c.Quantity > 0))
                    {
                        shopCartItems.Remove(product);
                    }
                }

            }

            return shopCartItems;
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

                    Guid proId = new Guid(orderDetailCount[0]);
                    var product = db.Products.Where(c => c.Id == proId && c.IsAvailable).Select(x => new
                    {
                        x.Id,
                        x.Title,
                        x.IsInPromotion,
                        x.DiscountAmount,
                        x.Amount,
                        x.ImageUrl
                    }).FirstOrDefault();

                    decimal amount = 0;

                    if (product.IsInPromotion)
                    {
                        if (orderDetailCount[2] != "undefined" && orderDetailCount[2] != "nocolor")
                        {
                            Guid colorIdGuid = new Guid(orderDetailCount[2]);

                            Product productItem = db.Products.FirstOrDefault(current =>
                                current.ParentId == product.Id && current.ColorId == colorIdGuid &&
                                current.IsDeleted == false);

                            if (productItem != null)
                            {
                                if (productItem.IsInPromotion && productItem.DiscountAmount != null)
                                    amount = productItem.DiscountAmount.Value;
                                else if (productItem.Amount != null)
                                    amount = productItem.Amount.Value;


                            }
                        }
                        else if (product.DiscountAmount != null)
                            amount = product.DiscountAmount.Value;
                    }
                    else if (product.Amount != null)
                        amount = product.Amount.Value;


                    // Color color = db.Colors.Find(new Guid(orderDetailCount[2]));

                    ShopCartItemViewModel currentShopItem = shopCartItems.FirstOrDefault(current =>
                        current.Id == orderDetailCount[0] && current.color == orderDetailCount[2] &&
                        current.size == orderDetailCount[3]);

                    if (currentShopItem == null)
                    {
                        string sizeId = "nosize";
                        string sizeTitle = string.Empty;
                        if (orderDetailCount[3] != "undefined")
                        {
                            sizeTitle = GetSizeTitle(orderDetailCount[3]);
                            sizeId = orderDetailCount[3];
                        }
                        string colorId = "nocolor";
                        string colorTitle = string.Empty;
                        if (orderDetailCount[2] != "undefined")
                        {
                            colorTitle = GetColorTitle(orderDetailCount[2]);
                            colorId = orderDetailCount[2];
                        }
                        shopCartItems.Add(new ShopCartItemViewModel()
                        {
                            Id = orderDetailCount[0],
                            Title = product.Title,
                            ImageUrl = product.ImageUrl,
                            Amount = (amount * Convert.ToDecimal(orderDetailCount[1])).ToString("N0"),
                            Price = String.Format("{0:n0}", amount),
                            Qty = orderDetailCount[1],
                            color = orderDetailCount[2],
                            size = orderDetailCount[3],
                            colorTitle = colorTitle,
                            SizeTitle = sizeTitle,
                            ColorId = colorId,
                            SizeId = sizeId
                        });
                    }
                    else
                    {
                        currentShopItem.Qty = (Convert.ToInt32(currentShopItem.Qty) + Convert.ToInt32(orderDetailCount[1])).ToString();
                    }
                }

                shopCartItems = UpdateShopCartByAvailability(shopCartItems);

                foreach (ShopCartItemViewModel product in shopCartItems)
                {
                    totalPrice = (decimal)(totalPrice + (Convert.ToDecimal(product.Amount)));
                }

                decimal shippmentprice = 0;

                decimal shippmetFreeLimit =
                    Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings["shippmentFree"]);

                if (totalPrice < shippmetFreeLimit && totalPrice > 0)
                    shippmentprice = Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings["shippment"]);

                decimal discountAmount = GetStepDiscountAmount(totalPrice, shopCartItems);

                decimal wallet = 0;

                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var identity = (System.Security.Claims.ClaimsIdentity)User.Identity;
                    string uid = identity.FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
                    Guid userId = new Guid(uid);
                    var user = db.Users.Find(userId);

                    if (user?.Amount != null)
                        wallet = user.Amount.Value;
                }



                decimal totalPayment = shippmentprice + totalPrice - discountAmount - wallet;
                if (totalPayment <= 0)
                {
                    totalPayment = 0;
                }
                decimal totalBeforWallet = shippmentprice + totalPrice - discountAmount;
                ShopCartList shopCart = new ShopCartList
                {
                    ShopCartItems = shopCartItems,
                    ShippmentPrice = shippmentprice,
                    Amount = totalPrice,
                    Discount = discountAmount,
                    TotalPayment = totalPayment,
                    Wallet = wallet,
                    TotalPaymentBeforWallet = totalBeforWallet
                };

                return shopCart;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public decimal GetStepDiscountAmount(decimal total, List<ShopCartItemViewModel> shopCartProducts)
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
            {
                return GetDiscount();
            }
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult DiscountRequestPost(string coupon, string jsonVar)
        {
            try
            {


                ShopCartList productInCarts = GetShoppingCartInfo(jsonVar);


                DiscountCode discount = db.DiscountCodes.FirstOrDefault(current => current.Code == coupon && current.IsDeleted == false && current.IsActive);

                string result = CheckCouponValidation(discount, productInCarts);

                if (result != "true")
                    return Json(result, JsonRequestBehavior.AllowGet);


                decimal discountAmount = 0;

                //if (discount.Amount >= 30)
                //    discount.Amount = discount.Amount - 15;

                if (productInCarts.Amount > discount.MaxAmount)
                    discountAmount = discount.Amount * discount.MaxAmount / 100;
                else
                    discountAmount = productInCarts.Amount * discount.Amount / 100;

                SetDiscountCookie(discountAmount.ToString(), coupon);

                return Json("true", JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json("true", JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        public void SetDiscountCookie(string discountAmount, string discountCode)
        {
            HttpContext.Response.Cookies.Set(new HttpCookie("mashadleather-discount")
            {
                Name = "mashadleather-discount",
                Value = discountAmount + "/" + discountCode,
                Expires = DateTime.Now.AddDays(1)
            });
        }
        public decimal GetDiscount()
        {
            if (Request.Cookies["mashadleather-discount"] != null)
            {
                try
                {
                    string cookievalue = Request.Cookies["mashadleather-discount"].Value;

                    string[] basketItems = cookievalue.Split('/');
                    return Convert.ToDecimal(basketItems[0]);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            return 0;
        }

        [AllowAnonymous]
        public string CheckCouponValidation(DiscountCode discount, ShopCartList productInCarts)
        {
            var identity = (System.Security.Claims.ClaimsIdentity)User.Identity;
            string id = identity.FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
            Guid userId = new Guid(id);


            if (discount == null)
                return "Invald";

            if (!discount.IsPublic && discount.UserId != userId)
                return "usererror";

            if (!discount.IsMultiUsing)
            {
                if (discount.IsUsed)
                    return "Used";
            }

            if (discount.ExpireDate < DateTime.Today)
                return "Expired";

            string res = "true";
            if (!discount.AvailableInPromotion)
                res = CheckPromotionOnDiscountCode(productInCarts);

            return res;
        }

        public string CheckPromotionOnDiscountCode(ShopCartList productInCarts)
        {
            bool isPromotion = false;

            foreach (var product in productInCarts.ShopCartItems)
            {
                Guid proId = new Guid(product.Id);

                var pro = db.Products.FirstOrDefault(c =>
                    c.Id == proId && c.IsInPromotion);

                if (pro != null)
                {
                    isPromotion = true;
                    break;
                }
            }

            if (isPromotion)
                return "promotionProduct";

            return "true";
        }
        public string GetSizeTitle(string sizeId)
        {
            if (sizeId == "nosize")
            {
                return "-";
            }
            else
            {
                Guid sizeIdGuid = new Guid(sizeId);
                var size = db.Sizes.Where(c => c.Id == sizeIdGuid).Select(x => new { x.Title }).FirstOrDefault();
                if (size != null)
                    return size.Title;
                return "-";
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
                Guid colorIdGuid = new Guid(colorId);
                var color = db.Colors.Where(c => c.Id == colorIdGuid).Select(x => new { x.Title }).FirstOrDefault();
                if (color != null)
                    return color.Title;
                return "-";
            }
        }
        public ActionResult LoadShopCart(string jsonvar)
        {
            return Json(GetShoppingCartInfo(jsonvar), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Finalize(string jsonvar, string firstName, string lastName, string cellNumber, string email, string province, string city, string address, string phone, string postalCode, string bank,string factor)
        {
            try
            {
                cellNumber = cellNumber.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4").Replace("۵", "5").Replace("۶", "6").Replace("v", "7").Replace("۸", "8").Replace("۹", "9");

                bool isValidMobile = Regex.IsMatch(cellNumber, @"(^(09|9)[0123456789][0123456789]\d{7}$)|(^(09|9)[0123456789][0123456789]\d{7}$)", RegexOptions.IgnoreCase);

                //if (paymentType != "online" && city != "2c730dce-774d-4007-88a9-4acb1dd48cea" &&
                //    city != "88e17e07-d8fc-4989-96b6-a9fbf394b521")
                //{
                //    return Json("invalidPaymentType", JsonRequestBehavior.AllowGet);

                //}
                //else { 

                if (isValidMobile)
                {
                    bool isEmail = Regex.IsMatch(email,
                        @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                        RegexOptions.IgnoreCase);

                    if (isEmail)
                    {
                        ShopCartList shopCart = GetShoppingCartInfo(jsonvar);

                        if (!shopCart.ShopCartItems.Any())
                        {
                            return Json("emptybasket", JsonRequestBehavior.AllowGet);
                        }
                        //User user = db.Users
                        //    .FirstOrDefault(current => current.IsDeleted == false && current.CellNum == cellNumber);

                        //if (user == null)
                        //{
                        //    user = new User();

                        //    user.CellNum = cellNumber;
                        //    user.Code = GenerateUserCode();
                        //    user.Address = address;
                        //    user.CityId = new Guid(city);
                        //    user.FirstName = firstName;
                        //    user.LastName = lastName;
                        //    user.Email = email;
                        //    user.RoleId = new Guid("0AEB583A-E4E2-44D6-92AA-39E7D2480127");
                        //    user.IsActive = true;
                        //    user.CreationDate = DateTime.Now;
                        //    user.IsDeleted = false;
                        //    user.Phone = phone;
                        //    user.PostalCode = postalCode;

                        //    db.Users.Add(user);

                        //    //db.SaveChanges();
                        //}

                        var identity = (System.Security.Claims.ClaimsIdentity)User.Identity;
                        string id = identity.FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
                        Guid userId = new Guid(id);

                        User user = db.Users
                             .FirstOrDefault(current => current.IsDeleted == false && current.Id == userId);

                        if (user != null)
                        {
                            user.Address = address;
                            user.FirstName = firstName;
                            user.LastName = lastName;
                            user.Email = email;
                            user.Phone = phone;
                            user.PostalCode = postalCode;
                            user.LastModifiedDate = DateTime.Now;
                            user.Address = address;
                        }

                        //decimal wallet = shopCart.Wallet;
                        //if (shopCart.TotalPaymentBeforWallet < wallet)
                        //    wallet = shopCart.TotalPaymentBeforWallet;
                        decimal wallet = 0;
                        Order order = new Order()
                        {
                            Code = GenerateOrderCode(),
                            UserId = userId,
                            Address = address,
                            OrderStatusId = BankHelper.GetOrderStatusIdByCode(1).Value,
                            IsActive = true,
                            CreationDate = DateTime.Now,
                            IsDeleted = false,
                            CityId = new Guid(city),
                            BankName = bank,
                            SubAmount = shopCart.Amount,
                            ShipmentAmount = shopCart.ShippmentPrice,
                            DiscountAmount = shopCart.Discount,
                            DiscountCodeId = GetDiscountIdByCookie(),
                            WalletAmount = wallet,
                            PaymentAmount = shopCart.TotalPayment,
                            TotalAmount = shopCart.TotalPaymentBeforWallet,
                            PostalCode = postalCode,
                            SendFactor = Convert.ToBoolean(factor)

                            //PaymentType = paymentType
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
                                            current.SizeId == sizeId && current.IsDeleted == false && current.Quantity > 0);

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
                                            current.SizeId == null && current.IsDeleted == false && current.Quantity > 0);

                                //محصولات مراقبت از چرم و جیر
                                else
                                    product = db.Products
                                        .FirstOrDefault(current => current.Id == parentId && current.ColorId == colorId &&
                                                                   current.SizeId == null && current.IsDeleted == false && current.Quantity > 0);



                                if (product == null)

                                    return Json("invalidQty| " + InvalidQuantityMessage(product),
                                        JsonRequestBehavior.AllowGet);


                                else
                                {
                                    if (product.Quantity < quantity)
                                        return Json("invalidQty| " + InvalidQuantityMessage(product),
                                            JsonRequestBehavior.AllowGet);
                                }

                                if (product.IsInPromotion)
                                    amount = product.DiscountAmount.Value;
                                else
                                    amount = product.Amount.Value;

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
                        //if (paymentType == "online")
                        //{
                        if (order.PaymentAmount > 0)
                        {
                            if (bank == "mellat")
                            {
                                string log = PayRequest(order.Id, uniqueOrderId,
                                    order.PaymentAmount.ToString().Split('/')[0]);
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
                            return Json("free|/billing/FreeResult/" + order.Id, JsonRequestBehavior.AllowGet);
                        }
                        //}
                        //else
                        //{
                        //            return Json("true-offlinepayment", JsonRequestBehavior.AllowGet);

                        //}
                    }
                    else
                        return Json("invalidEmail", JsonRequestBehavior.AllowGet);
                }

                else
                    return Json("invalidCellNumber", JsonRequestBehavior.AllowGet);
                // }
            }
            catch (Exception e)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }


        public string[] GetCookie()
        {
            if (Request.Cookies["mashadleather-discount"] != null)
            {
                string cookievalue = Request.Cookies["mashadleather-discount"].Value;

                string[] basketItems = cookievalue.Split('/');

                return basketItems;
            }

            return null;
        }

        public Guid? GetDiscountIdByCookie()
        {
            string[] discountCookie = GetCookie();

            if (discountCookie != null)
            {
                int len = discountCookie.Length;
                string code = discountCookie[len - 1];
                var discountCode = db.DiscountCodes.FirstOrDefault(c => c.Code == code);

                if (discountCode != null)
                    return discountCode.Id;
            }

            return null;
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

        private static object lockobj = new object();
        public int GenerateOrderCode()
        {
            lock (lockobj)
            {
                return FindeLastOrderCode() + 1;
            }

        }

        private int FindeLastOrderCode()
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


        public ActionResult ChangeStatus(string orderId, string statusId, string exitInventory)
        {
            try
            {
                Guid orderIdGuid = new Guid(orderId);
                Guid statusIdGuid = new Guid(statusId);
                Order order = db.Orders.Find(orderIdGuid);
                order.OrderStatusId = statusIdGuid;
                order.ExitInventory = exitInventory;
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

        public decimal ReturnTotalAmountOfFilter(List<OrderListViewModel> orders)
        {
            decimal amount = 0;
            foreach (OrderListViewModel order in orders)
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
                foreach (OrderDetail orderOrderDetail in order.OrderDetails)
                {

                    string colorTitle = "";
                    if (orderOrderDetail.Product.ColorId != null)
                        colorTitle = orderOrderDetail.Product.Color.Title;

                    string sizeTitle = "";
                    if (orderOrderDetail.Product.SizeId != null)
                        sizeTitle = orderOrderDetail.Product.Size.Title;


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
                        CreationDate = order.CreationDate,
                        ProductTitle = orderOrderDetail.Product.Title,
                        ColorTitle = colorTitle,
                        SizeTitle = sizeTitle

                    });
                }
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
            gv.HeaderRow.Cells[10].Text = "محصول";
            gv.HeaderRow.Cells[11].Text = "رنگ";
            gv.HeaderRow.Cells[12].Text = "سایز";

            Session["orders"] = gv;

            //gv.DataSource = gridList;
            //gv.DataBind();
            //Session["orders"] = gv;

        }
        public void GridviewBindViewModel(List<OrderListViewModel> orders)
        {


            //gv.DataSource = gridList;
            //gv.DataBind();
            //Session["orders"] = gv;

        }

        public ActionResult Download(int statusId, string status, string start, string end)
        {
            List<OrderListViewModel> orders = GetOrders(statusId, status, start, end);


            List<ExcelGridviewViewModel> gridList = new List<ExcelGridviewViewModel>();

            foreach (OrderListViewModel order in orders)
            {
                string[] totalAmount = order.TotalAmount.ToString("n0").Split('/');









                gridList.Add(new ExcelGridviewViewModel
                {
                    Code = order.Code,
                    SaleReferenceId = order.SaleReferenceId.ToString(),
                    OrderStatus = order.OrderStatusTitle,
                    FirstName = order.FirstName,
                    LastName = order.LastName,
                    CellNum = order.CellNum,
                    CityTitle = order.City,
                    Address = order.Address,
                    TotalAmount = totalAmount[0],
                    CreationDate = order.CreationDate,

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



            if (Session["orders"] != null)
            {
                return new DownloadFileActionResult((GridView)Session["orders"], "orders.xls");
            }
            else
            {
                return null;
            }
        }



        public ActionResult DownloadTransferPayment(Guid? statusId)
        {
            List<OrderListViewModel> orders = new List<OrderListViewModel>();
            if (statusId != null)
                orders = db.Orders.AsNoTracking()
                    .Where(current => current.PaymentType == "recieve" && current.OrderStatusId == statusId && current.IsDeleted == false)
                    .OrderByDescending(o => o.CreationDate).Select(
                        x => new OrderListViewModel()
                        {
                            Code = x.Code,
                            SaleReferenceId = x.SaleReferenceId,
                            OrderStatusTitle = x.OrderStatus.Title,
                            FirstName = x.User.FirstName,
                            LastName = x.User.LastName,
                            CellNum = x.User.CellNum,
                            TotalAmount = x.TotalAmount,
                            CreationDate = x.CreationDate,
                            Id = x.Id,
                            City = x.User.City.Title,
                            Address = x.Address,
                            PaymentType = x.PaymentType,
                            OrderStatusId = x.OrderStatusId

                        }).ToList();

            else
                orders = db.Orders.AsNoTracking()
                    .Where(current => current.PaymentType == "recieve" && current.IsDeleted == false)
                    .OrderByDescending(o => o.CreationDate).Select(
                        x => new OrderListViewModel()
                        {
                            Code = x.Code,
                            SaleReferenceId = x.SaleReferenceId,
                            OrderStatusTitle = x.OrderStatus.Title,
                            FirstName = x.User.FirstName,
                            LastName = x.User.LastName,
                            CellNum = x.User.CellNum,
                            TotalAmount = x.TotalAmount,
                            CreationDate = x.CreationDate,
                            Id = x.Id,
                            City = x.User.City.Title,
                            Address = x.Address,
                            PaymentType = x.PaymentType,
                            OrderStatusId = x.OrderStatusId

                        }).ToList();

            List<ExcelGridviewViewModel> gridList = new List<ExcelGridviewViewModel>();
            foreach (OrderListViewModel order in orders)
            {
                string[] totalAmount = order.TotalAmount.ToString("n0").Split('/');


                var orderDetails = db.OrderDetails.Where(c => c.OrderId == order.Id).Select(

                    x => new
                    {
                        x.Product.Barcode,

                    }).ToList();


                foreach (var orderOrderDetail in orderDetails)
                {



                    gridList.Add(new ExcelGridviewViewModel
                    {
                        Code = order.Code,
                        SaleReferenceId = order.SaleReferenceId.ToString(),
                        OrderStatus = order.OrderStatusTitle,
                        FirstName = order.FirstName,
                        LastName = order.LastName,
                        CellNum = order.CellNum,
                        CityTitle = order.City,
                        Address = order.Address,
                        TotalAmount = totalAmount[0],
                        CreationDate = order.CreationDate,
                        ProductTitle = orderOrderDetail.Barcode,


                    });
                }
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
            gv.HeaderRow.Cells[10].Text = "بارکد محصول";


            Session["orders-transfer"] = gv;


            if (Session["orders-transfer"] != null)
            {
                return new DownloadFileActionResult((GridView)Session["orders-transfer"], "orders-recievepayment.xls");
            }
            else
            {
                return null;
            }
        }

        public ActionResult DownloadDistinc(Guid? statusId)
        {
            DateTime bfDate = Convert.ToDateTime("2020-11-26");
            List<OrderListViewModel> orders = new List<OrderListViewModel>();


            orders = db.Orders.AsNoTracking()
                .Where(current => current.OrderStatusId == statusId && current.CreationDate > bfDate && current.IsDeleted == false)
                .OrderByDescending(o => o.CreationDate).Select(
                    x => new OrderListViewModel()
                    {
                        Code = x.Code,
                        SaleReferenceId = x.SaleReferenceId,
                        OrderStatusTitle = x.OrderStatus.Title,
                        FirstName = x.User.FirstName,
                        LastName = x.User.LastName,
                        CellNum = x.User.CellNum,
                        TotalAmount = x.TotalAmount,
                        CreationDate = x.CreationDate,
                        Id = x.Id,
                        City = x.User.City.Title,
                        Address = x.Address,
                        PaymentType = x.PaymentType,
                        OrderStatusId = x.OrderStatusId

                    }).ToList();



            List<ExcelGridviewViewModel> gridList = new List<ExcelGridviewViewModel>();
            foreach (OrderListViewModel order in orders)
            {
                string[] totalAmount = order.TotalAmount.ToString("n0").Split('/');



                gridList.Add(new ExcelGridviewViewModel
                {
                    Code = order.Code,
                    SaleReferenceId = order.SaleReferenceId.ToString(),
                    OrderStatus = order.OrderStatusTitle,
                    FirstName = order.FirstName,
                    LastName = order.LastName,
                    CellNum = order.CellNum,
                    CityTitle = order.City,
                    Address = order.Address,
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


            Session["orders-distinc"] = gv;


            if (Session["orders-distinc"] != null)
            {
                return new DownloadFileActionResult((GridView)Session["orders-distinc"], "orders-distinc.xls");
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

        [Route("checkout")]
        [Authorize(Roles = "Customer")]
        public ActionResult CheckOut()
        {
            Helper.BaseViewModelHelper baseViewModelHelper = new BaseViewModelHelper();

            ShopCartViewModel shopCart = new ShopCartViewModel
            {

                MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups(),
                MenuItem = baseViewModelHelper.GetMenuItems(),

            };
            var identity = (System.Security.Claims.ClaimsIdentity)User.Identity;
            string id = identity.FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
            Guid userId = new Guid(id);

            User user = db.Users
                .FirstOrDefault(current => current.IsDeleted == false && current.Id == userId);

            if (user != null)
                ViewBag.cellNumber = user.CellNum;
            ViewBag.provinceId = new SelectList(db.Provinces.OrderBy(current => current.Title), "Id", "Title");
            ViewBag.cityId = ReturnCities(null);
            return View(shopCart);
        }


        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public ActionResult ChangeAddress(Guid? id)
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
            ViewBag.Title = "تغییر آدرس سفارش کد " + order.Code;
            ChangeAddressViewModel orderAddress = new ChangeAddressViewModel()
            {
                Id = order.Id,
                Address = order.Address,
                PostalCode = order.PostalCode,

            };
            ViewBag.CityId = new SelectList(db.Cities.Where(c => c.ProvinceId == order.City.ProvinceId).OrderBy(c => c.Title), "Id", "Title", order.CityId);
            ViewBag.ProvinceId = new SelectList(db.Provinces.OrderBy(c => c.Title), "Id", "Title", order.City.ProvinceId);
            return View(orderAddress);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public ActionResult ChangeAddress(ChangeAddressViewModel orderAddress)
        {
            if (ModelState.IsValid)
            {
                Order order = db.Orders.Find(orderAddress.Id);

                order.Address = orderAddress.Address;
                order.CityId = orderAddress.CityId;
                order.PostalCode = orderAddress.PostalCode;
                order.LastModifiedDate = DateTime.Now;

                order.Description = "تغییر آدرس توسط ادمین";

                db.SaveChanges();
                return RedirectToAction("Details", new { id = orderAddress.Id });
            }
            ViewBag.CityId = new SelectList(db.Cities.Where(c => c.ProvinceId == orderAddress.ProvinceId).OrderBy(c => c.Title), "Id", "Title", orderAddress.CityId);
            ViewBag.ProvinceId = new SelectList(db.Provinces.OrderBy(c => c.Title), "Id", "Title", orderAddress.ProvinceId);

            return View(orderAddress);
        }

    }
}
