using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helper;
using Models;
using ViewModels;

namespace MashadLeatherEcommerce.Controllers
{
    public class SamanPaymentController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        [Route("saman")]
        public ActionResult Saman(int refId)
        {
            ViewBag.refId = refId;

            PaymentUniqeCodes uniqeCode = db.PaymentUniqeCodes.FirstOrDefault(current => current.Id == refId);

            if (uniqeCode != null)
            {
                Order order = db.Orders.FirstOrDefault(current => current.Id == uniqeCode.OrderId);

                if (order != null)
                {
                    ViewBag.amount = order.TotalAmount.ToString().Split('/')[0];
                }
                else
                    ViewBag.amount = 1;
            }
            else
                ViewBag.amount = 1;
            //کد یونیک و پیغام در کالبک  
            return View();
        }

        Helper.BaseViewModelHelper baseViewModelHelper = new BaseViewModelHelper();

        [Route("saman/callback")]
        public ActionResult SamanCallBack()
        {
            SamanCallbackViewModel samanCallback = new SamanCallbackViewModel();
            string state = Request.Form["State"].ToString();
            string stateCode = Request.Form["StateCode"].ToString();
            string resNum = Request.Form["ResNum"].ToString();
            //  string MID = Request.Form["MID"].ToString();
            string refNum = Request.Form["RefNum"].ToString();
            string CID = Request.Form["CID"].ToString();
            string TRACENO = Request.Form["TRACENO"].ToString();
            string RRN = Request.Form["RRN"].ToString();
            double amount = double.Parse(Request.Form["Amount"]);
            string website = Request.Form["website"].ToString();
            string securePan = Request.Form["SecurePan"].ToString();


            Guid orderId = GetOrginalOrderId(resNum);

            if (state.Equals("OK"))
            {
                if (refNum.Equals(string.Empty))
                {
                    samanCallback.IsSuccess = false;
                    samanCallback.Message = "گويا خريد شما توسط بانک تاييد شده است اما رسيد ديجيتالي شما تاييد نگشت";
                    samanCallback.RefrenceNumber = "مشکلي در فرايند رزرو خريد شما پيش آمده است";
                    samanCallback.TrackNumber = "مشکلي در فرايند رزرو خريد شما پيش آمده است";

                    BankHelper.UpdatePayment(orderId, stateCode, Convert.ToInt64(TRACENO), refNum, false);

                }
                else
                {
                    Session["RefNum"] = Request.Form["RefNum"].ToString();
                    try
                    {


                        SamanWebService.PaymentIFBindingSoap saman = new SamanWebService.PaymentIFBindingSoapClient();
                        string MID = "21284935";
                        var Result = saman.verifyTransaction(refNum, MID);
                        double strTempRes = Result;
                        string strNodeType;
                        if (strTempRes > 0)
                        {
                            strTempRes = 1;
                        }
                        switch ((int)strTempRes)
                        {
                            case 1:     //VERIFIED
                                        //connection.Open()
                                if (Result < amount) //Total Amount of Basket
                                {
                                    samanCallback.IsSuccess = false;
                                    samanCallback.Message = "مبلغ انتقالي کمتر از مبلغ کل فاکتور ميباشد";
                                    samanCallback.RefrenceNumber = "مشکلي در فرايند رزرو خريد شما پيش آمده است";
                                    samanCallback.TrackNumber = "مشکلي در فرايند رزرو خريد شما پيش آمده است";
                                    BankHelper.UpdatePayment(orderId, stateCode, Convert.ToInt64(TRACENO), refNum, false);


                                }
                                else if (Result.Equals(amount)) //Total Amount of Basket
                                {
                                    Order order = db.Orders.Find(orderId);
                                    if (order != null && amount < (double) order.TotalAmount)
                                    {

                                        samanCallback.IsSuccess = false;
                                        samanCallback.Message = "مبلغ انتقالي کمتر از مبلغ کل فاکتور ميباشد";
                                        samanCallback.RefrenceNumber = "مشکلي در فرايند رزرو خريد شما پيش آمده است";
                                        samanCallback.TrackNumber = "مشکلي در فرايند رزرو خريد شما پيش آمده است";
                                        BankHelper.UpdatePayment(orderId, stateCode, Convert.ToInt64(TRACENO),
                                            refNum, false);

                                    }
                                    else
                                    {
                                        samanCallback.IsSuccess = true;
                                        samanCallback.Message =
                                            "بانک صحت رسيد ديجيتالي شما را تصديق نمود. فرايند خريد تکميل گشت";
                                        samanCallback.RefrenceNumber = RRN;
                                        samanCallback.TrackNumber = TRACENO;
                                        BankHelper.UpdatePayment(orderId, stateCode, Convert.ToInt64(TRACENO), refNum,
                                            true);
                                    }

                                }
                                else if (Result > amount) //Total Amount of Basket
                                {
                                    samanCallback.IsSuccess = false;
                                    samanCallback.Message = string.Format("خريد شما تاييد و نهايي گشت اما مبلغ انتقالي {0} ريال بيش از مبلغ خريد ميباشد", Result);
                                    samanCallback.RefrenceNumber = "مشکلي در فرايند رزرو خريد شما پيش آمده است";
                                    samanCallback.TrackNumber = "مشکلي در فرايند رزرو خريد شما پيش آمده است";
                                    BankHelper.UpdatePayment(orderId, stateCode, Convert.ToInt64(TRACENO), refNum, false);

                                }
                                break;
                            case -1:        //TP_ERROR
                                samanCallback.IsSuccess = false;
                                samanCallback.Message = "بروز خطا درهنگام بررسي صحت رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                                samanCallback.RefrenceNumber = "مشکلي در فرايند رزرو خريد شما پيش آمده است";
                                samanCallback.TrackNumber = "مشکلي در فرايند رزرو خريد شما پيش آمده است";
                                BankHelper.UpdatePayment(orderId, stateCode, Convert.ToInt64(TRACENO), refNum, false);


                                break;
                            case -2:        //ACCOUNTS_DONT_MATCH
                                samanCallback.IsSuccess = false;
                                samanCallback.Message = "بروز خطا در هنگام تاييد رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                                samanCallback.RefrenceNumber = "مشکلي در فرايند رزرو خريد شما پيش آمده است";
                                samanCallback.TrackNumber = "مشکلي در فرايند رزرو خريد شما پيش آمده است";
                                BankHelper.UpdatePayment(orderId, stateCode, Convert.ToInt64(TRACENO), refNum, false);

                                break;
                            case -3:        //BAD_INPUT
                                samanCallback.IsSuccess = false;
                                samanCallback.Message = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                                samanCallback.RefrenceNumber = "مشکلي در فرايند رزرو خريد شما پيش آمده است";
                                samanCallback.TrackNumber = "مشکلي در فرايند رزرو خريد شما پيش آمده است";
                                BankHelper.UpdatePayment(orderId, stateCode, Convert.ToInt64(TRACENO), refNum, false);

                                break;
                            case -4:        //INVALID_PASSWORD_OR_ACCOUNT
                                samanCallback.IsSuccess = false;
                                samanCallback.Message = "خطاي دروني سيستم درهنگام بررسي صحت رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                                samanCallback.RefrenceNumber = "مشکلي در فرايند رزرو خريد شما پيش آمده است";
                                samanCallback.TrackNumber = "مشکلي در فرايند رزرو خريد شما پيش آمده است";
                                break;
                            case -5:        //DATABASE_EXCEPTION
                                samanCallback.IsSuccess = false;
                                samanCallback.Message = "خطاي دروني سيستم درهنگام بررسي صحت رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                                samanCallback.RefrenceNumber = "مشکلي در فرايند رزرو خريد شما پيش آمده است";
                                samanCallback.TrackNumber = "مشکلي در فرايند رزرو خريد شما پيش آمده است";
                                BankHelper.UpdatePayment(orderId, stateCode, Convert.ToInt64(TRACENO), refNum, false);

                                break;
                            case -7:        //ERROR_STR_NULL
                                samanCallback.IsSuccess = false;
                                samanCallback.Message = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                                samanCallback.RefrenceNumber = "مشکلي در فرايند رزرو خريد شما پيش آمده است";
                                samanCallback.TrackNumber = "مشکلي در فرايند رزرو خريد شما پيش آمده است";
                                BankHelper.UpdatePayment(orderId, stateCode, Convert.ToInt64(TRACENO), refNum, false);

                                break;
                            case -8:        //ERROR_STR_TOO_LONG
                                samanCallback.IsSuccess = false;
                                samanCallback.Message = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                                samanCallback.RefrenceNumber = "مشکلي در فرايند رزرو خريد شما پيش آمده است";
                                samanCallback.TrackNumber = "مشکلي در فرايند رزرو خريد شما پيش آمده است";
                                BankHelper.UpdatePayment(orderId, stateCode, Convert.ToInt64(TRACENO), refNum, false);

                                break;
                            case -9:        //ERROR_STR_NOT_AL_NUM
                                samanCallback.IsSuccess = false;
                                samanCallback.Message = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                                samanCallback.RefrenceNumber = "مشکلي در فرايند رزرو خريد شما پيش آمده است";
                                samanCallback.TrackNumber = "مشکلي در فرايند رزرو خريد شما پيش آمده است";
                                BankHelper.UpdatePayment(orderId, stateCode, Convert.ToInt64(TRACENO), refNum, false);

                                break;
                            case -10:   //ERROR_STR_NOT_BASE64
                                samanCallback.IsSuccess = false;
                                samanCallback.Message = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                                samanCallback.RefrenceNumber = "مشکلي در فرايند رزرو خريد شما پيش آمده است";
                                samanCallback.TrackNumber = "مشکلي در فرايند رزرو خريد شما پيش آمده است";
                                BankHelper.UpdatePayment(orderId, stateCode, Convert.ToInt64(TRACENO), refNum, false);

                                break;
                            case -11:   //ERROR_STR_TOO_SHORT
                                samanCallback.IsSuccess = false;
                                samanCallback.Message = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                                samanCallback.RefrenceNumber = "مشکلي در فرايند رزرو خريد شما پيش آمده است";
                                samanCallback.TrackNumber = "مشکلي در فرايند رزرو خريد شما پيش آمده است";
                                BankHelper.UpdatePayment(orderId, stateCode, Convert.ToInt64(TRACENO), refNum, false);

                                break;
                            default:
                                samanCallback.IsSuccess = false;

                                break;
                        }

                        ViewBag.resiidtemp = strTempRes;

                    }
                    catch (Exception ex)
                    {
                        samanCallback.IsSuccess = false;
                        samanCallback.Message = "سرور بانک براي تاييد رسيد ديجيتالي در دسترس نيست<br><br><div dir ='ltr' align='left'>" + ex.Message + "</div>";
                        samanCallback.RefrenceNumber = "مشکلي در فرايند رزرو خريد شما پيش آمده است";
                        samanCallback.TrackNumber = "مشکلي در فرايند رزرو خريد شما پيش آمده است";
                        BankHelper.UpdatePayment(orderId, stateCode, Convert.ToInt64(TRACENO), refNum, false);

                    }
                }
            }

            else
            {
                samanCallback.IsSuccess = true;
                samanCallback.Message = string.Format("{0} متاسفانه بانک خريد شما را تاييد نکرده است", Request.Form["State"]);
                samanCallback.RefrenceNumber = "خريد شما تاييد نگشته است";
                samanCallback.TrackNumber = "مشکلي در فرايند رزرو خريد شما پيش آمده است";
                BankHelper.UpdatePayment(orderId, stateCode, Convert.ToInt64(TRACENO), refNum, false);


            }
            samanCallback.MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups();
            samanCallback.MenuItem = baseViewModelHelper.GetMenuItems();
            return View(samanCallback);
        }

        public Guid GetOrginalOrderId(string uniqeOrderId)
        {
            long id = Convert.ToInt64(uniqeOrderId);
            return db.PaymentUniqeCodes.Find(id).OrderId;
        }
    }
}