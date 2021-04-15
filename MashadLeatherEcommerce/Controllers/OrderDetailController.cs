using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Helper;
using Models;
using ViewModels;

namespace MashadLeatherEcommerce.Controllers
{
    public class OrderDetailController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
       
        [Route("historyDetails/{id:Guid}")]
        [Authorize(Roles = "Customer")]
        public ActionResult OrderDetailHistory(Guid? id)
        {
            if (id != null)
            {
                Helper.BaseViewModelHelper baseViewModelHelper = new BaseViewModelHelper();
                List<OrderDetail> orderDetails = db.OrderDetails.Where(current => current.OrderId == id&&current.IsDeleted==false).ToList();
                Order order = db.Orders.Find(id);

                string discountAmount = "0";
                if (order.DiscountAmount != null)
                    discountAmount = order.DiscountAmount.Value.ToString("n0");

                string wallet = "0";
                if (order.WalletAmount != null)
                    wallet = order.WalletAmount.Value.ToString("n0");

                OrderDetailHistoryViewModel orderDetailHistoryViewModel = new OrderDetailHistoryViewModel()
                {
                    MenuItem = baseViewModelHelper.GetMenuItems(),
                    OrderDetails = orderDetails,
                    ShippmentPrice = "0",
                    Amount = order.TotalAmount.ToString("n0"),
                    Vat = ((order.TotalAmount * 9) / 100).ToString("n0"),
                    TotalPayment = order.PaymentAmount.Value.ToString("n0"),
                    User=db.Users.Where(current=>current.Id==order.UserId).FirstOrDefault(),
                    MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups(),
                    Code = order.Code.ToString(),
                    DiscountAmount = discountAmount,
                    Wallet = wallet
                };
                return View(orderDetailHistoryViewModel);
            }
            else
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
        }
    }
}