using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Helper;
using Models;
using ViewModels;

namespace MashadLeatherEcommerce.Controllers
{

    public class BillingController : Controller
    {

        public static readonly string PgwSite = ConfigurationManager.AppSettings["PgwSite"];
        public static readonly string CallBackUrl = ConfigurationManager.AppSettings["CallBackUrl"];
        public static readonly string TerminalId = ConfigurationManager.AppSettings["TerminalId"];
        public static readonly string UserName = ConfigurationManager.AppSettings["UserName"];
        public static readonly string UserPassword = ConfigurationManager.AppSettings["UserPassword"];

        private DatabaseContext db = new DatabaseContext();

        // GET: Billing
        public ActionResult Result()
        {
            Helper.BaseViewModelHelper baseViewModelHelper = new BaseViewModelHelper();

            BillingResultViewModel billing = new BillingResultViewModel();
            billing.MenuItem = baseViewModelHelper.GetMenuItems();
            billing.MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups();



            MellatReturn();

            return View(billing);
        }



        public ActionResult FreeResult(Guid id)
        {
            Helper.BaseViewModelHelper baseViewModelHelper = new BaseViewModelHelper();

            BillingResultViewModel billing = new BillingResultViewModel();
            billing.MenuItem = baseViewModelHelper.GetMenuItems();
            billing.MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups();

            try
            {
                Order order = db.Orders.Find(id);

                if (order != null)
                {
                    Int64 resCode = GetRefCodeForFree(id);
                    OrderStatus orderStatus = db.OrderStatuses.FirstOrDefault(current => current.IsDeleted == false && current.Code == 2);

                    if (orderStatus != null)
                        order.OrderStatusId = orderStatus.Id;

                    order.SaleReferenceId = resCode;

                    if (order.WalletAmount > 0)
                    {
                        User user = db.Users.Find(order.UserId);
                        if (user != null)
                        {
                            user.Amount -= order.WalletAmount;
                            user.LastModifiedDate = DateTime.Now;
                        }

                    }

                    ViewBag.Message = "پرداخت با موفقیت انجام شد.";
                    ViewBag.SaleReferenceId = resCode;
                    //تراکنش تایید و ستل شده است 


                    //SendMessageToUser(order.User.CellNum, order.Code.ToString());
                    ViewBag.Code = order.Code;
                    ViewBag.CellNumber = order.User.CellNum;
                    if (order.DiscountCodeId != null)
                    {
                        DiscountCode discountCode = db.DiscountCodes.Find(order.DiscountCodeId);

                        if (discountCode != null)
                        {
                            discountCode.IsUsed = true;
                            discountCode.LastModifiedDate = DateTime.Now;

                            db.SaveChanges();
                        }
                    }
                    ChangeProductStock(order);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return View(billing);
        }


        public Int64 GetRefCodeForFree(Guid orderId)
        {
            PaymentFreeCode paymentFreeCode = db.PaymentFreeCodes.Where(c => c.IsDeleted == false)
               .OrderByDescending(c => c.CreationDate).FirstOrDefault();

            Int64 code = 100000;

            if (paymentFreeCode != null)
                code = paymentFreeCode.Code + 1;

            if (paymentFreeCode != null)
            {
                PaymentFreeCode oPaymentFreeCode = new PaymentFreeCode()
                {
                    Id = Guid.NewGuid(),
                    OrderId = orderId,
                    CreationDate = DateTime.Now,
                    IsDeleted = false,
                    IsActive = true,
                    Code = code
                };

                db.PaymentFreeCodes.Add(oPaymentFreeCode);
            }

            return code;

        }



        public Guid GetOrginalOrderId(long uniqeOrderId)
        {
            return db.PaymentUniqeCodes.Find((uniqeOrderId)).OrderId;
        }
        private void MellatReturn()
        {
            MellatWebService.PaymentGatewayClient bpService = new MellatWebService.PaymentGatewayClient();

            BankHelper.BypassCertificateError();
            if (string.IsNullOrEmpty(Request.Params["SaleReferenceId"]))
            {
                //ResCode=StatusPayment
                if (!string.IsNullOrEmpty(Request.Params["ResCode"]))
                {
                    ViewBag.Message = BankHelper.MellatResult(Request.Params["ResCode"]);
                    ViewBag.SaleReferenceId = "**************";
                }
                else
                {
                    ViewBag.Message = "رسید قابل قبول نیست";
                    ViewBag.SaleReferenceId = "**************";



                }
            }
            else
            {
                try
                {
                    string terminalId = ConfigurationManager.AppSettings["TerminalId"];
                    string userName = ConfigurationManager.AppSettings["UserName"];
                    string userPassword = ConfigurationManager.AppSettings["UserPassword"];
                    long saleOrderId = 0;  //PaymentID 
                    long saleReferenceId = 0;
                    string refId = null;

                    try
                    {
                        saleOrderId = long.Parse(Request.Params["SaleOrderId"].TrimEnd());
                        saleReferenceId = long.Parse(Request.Params["SaleReferenceId"].TrimEnd());
                        refId = Request.Params["RefId"].TrimEnd();
                    }
                    catch (Exception ex)
                    {
                        ViewBag.message = ex + "<br>" + " وضعیت:مشکلی در پرداخت بوجود آمده ، در صورتی که وجه پرداختی از حساب بانکی شما کسر شده است آن مبلغ به صورت خودکار برگشت داده خواهد شد ";
                        ViewBag.SaleReferenceId = "**************";
                        return;
                    }
                    string Result;
                    Result = bpService.bpVerifyRequest(long.Parse(terminalId), userName, userPassword, saleOrderId, saleOrderId, saleReferenceId);

                    if (!string.IsNullOrEmpty(Result))
                    {
                        if (Result == "0")
                        {
                            string qresult;
                            qresult = bpService.bpInquiryRequest(long.Parse(terminalId), userName, userPassword, saleOrderId, saleOrderId, saleReferenceId);
                            if (qresult == "0")
                            {
                                //long paymentID = Convert.ToInt64(saleOrderId);
                                Guid orderId = GetOrginalOrderId(saleOrderId);

                                // پرداخت نهایی
                                string Sresult;
                                // تایید پرداخت
                                Sresult = bpService.bpSettleRequest(long.Parse(terminalId), userName, userPassword, saleOrderId, saleOrderId, saleReferenceId);
                                if (Sresult != null)
                                {
                                    if (Sresult == "0" || Sresult == "45")
                                    {
                                        BankHelper.UpdatePayment(orderId, Result, saleReferenceId, refId, true);
                                        ViewBag.Message = "پرداخت با موفقیت انجام شد.";
                                        ViewBag.SaleReferenceId = saleReferenceId;
                                        //تراکنش تایید و ستل شده است 
                                        Order order = db.Orders.Find(orderId);
                                        if (order != null)
                                        {
                                            //SendMessageToUser(order.User.CellNum, order.Code.ToString());
                                            ViewBag.Code = order.Code;
                                            ViewBag.CellNumber = order.User.CellNum;
                                            if (order.DiscountCodeId != null)
                                            {
                                                DiscountCode discountCode = db.DiscountCodes.Find(order.DiscountCodeId);

                                                if (discountCode != null)
                                                {
                                                    discountCode.IsUsed = true;
                                                    discountCode.LastModifiedDate = DateTime.Now;

                                                    db.SaveChanges();
                                                }
                                            }
                                            ChangeProductStock(order);
                                            db.SaveChanges();
                                        }
                                    }
                                    else
                                    {
                                        BankHelper.UpdatePayment(orderId, Result, saleReferenceId, refId, false);
                                        ViewBag.Message = "مشکلی در پرداخت به وجود آمده است ، در صورتیکه وجه پرداختی از حساب بانکی شما کسر شده است آن مبلغ به صورت خودکار برگشت داده خواهد شد";
                                        ViewBag.SaleReferenceId = "**************";
                                        //تراکنش تایید شده ولی ستل نشده است
                                    }
                                }
                            }
                            else
                            {
                                //string Rvresult;
                                ////عملیات برگشت دادن مبلغ
                                //Rvresult = bpService.bpReversalRequest(long.Parse(terminalId), userName, userPassword, saleOrderId, saleOrderId, saleReferenceId);
                                //Guid orderId = GetOrginalOrderId(saleOrderId);
                                //BankHelper.UpdatePayment(orderId, Result, saleReferenceId, refId, false);
                                //ViewBag.Message = "تراکنش بازگشت داده شد";
                                //ViewBag.SaleReferenceId = "**************";
                                //long paymentId = Convert.ToInt64(saleOrderId);
                                //BankHelper.UpdatePayment(GetOrginalOrderId(saleOrderId), Result, saleReferenceId, refId, false);
                            }
                        }
                        else
                        {
                            Guid orderId = GetOrginalOrderId(saleOrderId);
                            BankHelper.UpdatePayment(orderId, Result, saleReferenceId, refId, false);

                            ViewBag.Message = BankHelper.MellatResult(Result);
                            ViewBag.SaleReferenceId = "**************";
                            long paymentId = Convert.ToInt64(saleOrderId);

                        }
                    }
                    else
                    {
                        Guid orderId = GetOrginalOrderId(saleOrderId);
                        BankHelper.UpdatePayment(orderId, Result, saleReferenceId, refId, false);

                        ViewBag.Message = "شماره رسید قابل قبول نیست";
                        ViewBag.SaleReferenceId = "**************";
                    }
                }
                catch (Exception ex)
                {
                    string errors = ex.Message;
                    ViewBag.Message = "مشکلی در پرداخت به وجود آمده است ، در صورتیکه وجه پرداختی از حساب بانکی شما کسر شده است آن مبلغ به صورت خودکار برگشت داده خواهد شد";
                    ViewBag.SaleReferenceId = "**************";
                }
            }
        }

        //public string TestSms()
        //{
        //    SendMessageToUser("09124806404", "111");
        //    return "";
        //}

        public ActionResult SendMessageToUser(string cellNumber, string orderCode)
        {
            try
            {
                SmsMessageHelper.SendSms(cellNumber,
                    SmsMessageHelper.OrderCompletionText(orderCode));

                return Json("Ok", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("NotOk", JsonRequestBehavior.AllowGet);

            }
            // قسمت ارسال اس ام اس خوش امدگویی به کاربر جدید


        }

        public void ChangeProductStock(Order order)
        {
            List<OrderDetail> orderDetails = db.OrderDetails.Where(c => c.OrderId == order.Id).ToList();

            foreach (OrderDetail orderDetail in orderDetails)
            {
                Product pro = db.Products.Find(orderDetail.ProductId);

                if (pro != null)
                {
                    pro.Quantity = pro.Quantity - orderDetail.Quantity;

                    if (pro.Quantity <= 0)
                        pro.IsAvailable = false;

                    pro.LastModifiedDate = DateTime.Now;

                    var otherProductWithSameBarcode = db.Products
                        .Where(c => c.Barcode == pro.Barcode && c.IsDeleted == false && c.Id != pro.Id).ToList();

                    foreach (Product product in otherProductWithSameBarcode)
                    {
                        product.Quantity = product.Quantity - orderDetail.Quantity;

                        if (product.Quantity <= 0)
                            product.IsAvailable = false;

                        product.LastModifiedDate = DateTime.Now;
                    }
                }
            }

        }

        public ActionResult EventSetup(string orderCode)
        {
            try
            {
                int code = Convert.ToInt32(orderCode);

                Order order = db.Orders.FirstOrDefault(c => c.Code == code);


                List<EventStatusProduct> products = new List<EventStatusProduct>();

                List<OrderDetail> orderDetails = db.OrderDetails.Where(c => c.OrderId == order.Id).Include(c => c.Product).ToList();

                foreach (OrderDetail orderDetail in orderDetails)
                {
                    Product pro = db.Products.Find(orderDetail.ProductId);

                    //if (pro != null)
                    //{
                    //    pro.Quantity = pro.Quantity - orderDetail.Quantity;

                    //    if (pro.Quantity <= 0)
                    //        pro.IsAvailable = false;

                    //    pro.LastModifiedDate=DateTime.Now;

                    //    db.SaveChanges();
                    //}

                    Product parentPro = db.Products.Find(pro.ParentId);
                    ProductCategory productCategory = db.ProductCategories.Find(parentPro.ProductCategoryId);


                    products.Add(new EventStatusProduct()
                    {
                        Id = pro.Id.ToString(),
                        Title = pro.Title,
                        Amount = (orderDetail.Price / 10).ToString().Split('/')[0],
                        Category = productCategory.UrlParam,
                        Color = pro.Color.Title,
                        Quantity = orderDetail.Quantity
                    });
                }

                EventStatusViewModel eventStatus = new EventStatusViewModel()
                {
                    Code = orderCode,
                    Amount = (order.TotalAmount / 10).ToString().Split('/')[0],
                    Products = products
                };

                return Json(eventStatus, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("NotOk", JsonRequestBehavior.AllowGet);

            }

        }


    }
}