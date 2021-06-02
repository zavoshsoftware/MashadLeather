using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Reports.Models;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;

namespace MashadLeatherEcommerce.Controllers
{
    public class ReportController : Controller
    {
        public ActionResult Invoice(Guid id)
        {

            TempData["id"] = id;
            return View();
        }



        public ActionResult LoadInvoiceReportSnapshot()
        {
            Guid orderId = new Guid(TempData["id"].ToString());


            Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHn0s4gy0Fr5YoUZ9V00Y0igCSFQzwEqYBh/N77k4f0fWXTHW5rqeBNLkaurJDenJ9o97TyqHs9HfvINK18Uwzsc/bG01Rq+x3H3Rf+g7AY92gvWmp7VA2Uxa30Q97f61siWz2dE5kdBVcCnSFzC6awE74JzDcJMj8OuxplqB1CYcpoPcOjKy1PiATlC3UsBaLEXsok1xxtRMQ283r282tkh8XQitsxtTczAJBxijuJNfziYhci2jResWXK51ygOOEbVAxmpflujkJ8oEVHkOA/CjX6bGx05pNZ6oSIu9H8deF94MyqIwcdeirCe60GbIQByQtLimfxbIZnO35X3fs/94av0ODfELqrQEpLrpU6FNeHttvlMc5UVrT4K+8lPbqR8Hq0PFWmFrbVIYSi7tAVFMMe2D1C59NWyLu3AkrD3No7YhLVh7LV0Tttr/8FrcZ8xirBPcMZCIGrRIesrHxOsZH2V8t/t0GXCnLLAWX+TNvdNXkB8cF2y9ZXf1enI064yE5dwMs2fQ0yOUG/xornE";


            var path = System.Web.HttpContext.Current.Server.MapPath("~/Content/license.key");

            Stimulsoft.Base.StiLicense.LoadFromFile(path);

            var report = new StiReport();
            report.Load(Server.MapPath("~/Reports/MRT/Invoice.mrt"));
            report.RegBusinessObject("KhoshdastInvoice", GetInvoice(orderId));
            //  report.Dictionary.Variables.Add("today", DateTime.Today());
            return StiMvcViewer.GetReportResult(report);
        }

        public virtual ActionResult ViewerEvent()
        {
            return StiMvcViewer.ViewerEventResult();
        }

        public virtual ActionResult PrintReport()
        {
            return StiMvcViewer.PrintReportResult();
        }

        public virtual ActionResult ExportReport()
        {
            return StiMvcViewer.ExportReportResult();
        }

        private DatabaseContext db = new DatabaseContext();

        public InvoiceReportViewModel GetInvoice(Guid id)
        {
            var order = db.Orders.Find(id);

            string cellNumber = "";
            if (order.UserId != null)
            {
                cellNumber = db.Users.FirstOrDefault(c => c.Id == order.UserId).CellNum;
            }
            //string city = "-";
            //if (order.CityId != null)
            //    city = order.City.Title;

            InvoiceReportViewModel invoice = new InvoiceReportViewModel()
            {
                CellNumber = cellNumber,
                OrderCode = order.Code.ToString(),
                Total = order.TotalAmount.ToString("N0"),
                City = order.City.Title,
                Address = order.Address,
                PostalCode = order.PostalCode,
                FullName = order.User.FirstName + " - " + order.User.LastName,
            };

            return invoice;
            //}

            //return new InvoiceReportViewModel();
        }


    }
}