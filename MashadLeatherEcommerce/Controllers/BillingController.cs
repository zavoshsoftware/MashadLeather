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
                                            // SendMessageToUser(order.User.CellNum, order.Code.ToString());
                                            ViewBag.Code = order.Code;
                                            ViewBag.CellNumber = order.User.CellNum;
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
        public ActionResult EventSetup(string orderCode)
        {
            try
            {
                int code = Convert.ToInt32(orderCode);

                Order order = db.Orders.FirstOrDefault(c => c.Code == code);


                List<EventStatusProduct> products=new List<EventStatusProduct>();

                List<OrderDetail> orderDetails = db.OrderDetails.Where(c => c.OrderId == order.Id).Include(c=>c.Product).ToList();

                foreach (OrderDetail orderDetail in orderDetails)
                {
                    Product pro = db.Products.Find(orderDetail.ProductId);

                    Product parentPro = db.Products.Find(pro.ParentId);
                    ProductCategory productCategory = db.ProductCategories.Find(parentPro.ProductCategoryId);



                    products.Add(new EventStatusProduct()
                    {
                        Id = pro.Id.ToString(),
                        Title = pro.Title,
                        Amount = (orderDetail.Price/10).ToString().Split('/')[0],
                        Category = productCategory.UrlParam,
                        Color = pro.Color.Title,
                        Quantity = orderDetail.Quantity
                    });
                }

                EventStatusViewModel eventStatus = new EventStatusViewModel()
                {
                    Code = orderCode,
                    Amount = (order.TotalAmount/10).ToString().Split('/')[0],
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