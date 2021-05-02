using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helper;
using Models;
using ViewModels;

namespace MashadLeatherEcommerce.Controllers
{
        [Authorize(Roles = "Administrator,SuperAdministrator,eshopadmin")]
    public class DashboardController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult OrderList(string startDate, string endDate)
        {
            if (startDate == null)
                startDate = DateTime.Today.ToShortDateString();
            if (endDate == null)
                endDate = DateTime.Today.AddDays(1).ToShortDateString();

            ViewBag.startDate = startDate;
            ViewBag.endDate = endDate;

            List<Order> orders = GetOrders(startDate, endDate);
            Guid statusOnprogress = new Guid("BABB5BB5-E1FB-44D3-99BD-A2C39FC63930");
            Guid statusFinal = new Guid("3D3A706E-2DEB-4913-BB15-2A31EDE340D6");
            OrderDashboardViewModel result = new OrderDashboardViewModel()
            {
                TotalOrderQty = orders.Count,
                TotamOrderAmount = orders.Sum(c => c.TotalAmount),
                OrderByProvince = GetOrderByProvince(orders),
                TotalOnProgressOrderQty = orders.Count(c=>c.OrderStatusId==statusOnprogress),
                TotalFinalOrderQty = orders.Count(c=>c.OrderStatusId== statusFinal)

            };

            return View(result);
        }

        public List<OrderByCityViewModel> GetOrderByProvince(List<Order> orders)
        {
            List<OrderByCityViewModel> result = new List<OrderByCityViewModel>();

            List<Province> provinces = db.Provinces.Where(c => c.IsDeleted == false).OrderBy(c=>c.Title).ToList();

            foreach (var province in provinces)
            {

                result.Add(new OrderByCityViewModel()
                {
                    ProvinceId = province.Id,
                    ProvinceTitle = province.Title,
                    TotalOrderQtyByProvince = orders.Where(c => c.City.ProvinceId == province.Id).Count(),
                    TotamOrderAmountByProvince = orders.Where(c => c.City.ProvinceId == province.Id).Sum(c => c.TotalAmount),
                });
            }

            return result;
        }

        public List<Order> GetOrders(string start, string end)
        {
            DateTime startDate = Convert.ToDateTime(start);
            DateTime endDate = Convert.ToDateTime(end);

            //List<Order> orders = db.Orders.AsNoTracking()
            //    .Where(current => current.IsDeleted == false)
            //  .OrderByDescending(o => o.CreationDate).ToList();
           var orderlist = db.Orders.AsNoTracking().Where(current =>
                current.IsDeleted == false &&current.SaleReferenceId!=null&&
                (current.CreationDate >= startDate.Date && current.CreationDate <= endDate.Date));

            return orderlist.ToList();
        }

        public List<Order> ReturnFilteredOrders(List<Order> orders, string start, string end)
        {
            List<Order> orderlist = new List<Order>();
            if (!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
            {

                DateTime startDate = Convert.ToDateTime(start);
                DateTime endDate = Convert.ToDateTime(end);
                ViewBag.startDate = startDate.ToShortDateString();
                ViewBag.endDate = endDate.ToShortDateString();
                orderlist = orders.Where(current => (current.CreationDate >= startDate.Date && current.CreationDate <= endDate.Date)).ToList();

            }

            return orderlist;
        }

    }
}