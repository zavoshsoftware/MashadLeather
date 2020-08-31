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
                OrderDetailHistoryViewModel orderDetailHistoryViewModel = new OrderDetailHistoryViewModel()
                {
                    MenuItem = baseViewModelHelper.GetMenuItems(),
                    OrderDetails = orderDetails,
                    ShippmentPrice = "0",
                    Amount = order.TotalAmount.ToString().Split('.')[0],
                    Vat = ((order.TotalAmount * 9) / 100).ToString().Split('.')[0],
                    TotalPayment = (order.TotalAmount + ((order.TotalAmount * 9) / 100)).ToString().Split('.')[0],
                    User=db.Users.Where(current=>current.Id==order.UserId).FirstOrDefault(),
                    MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups()
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