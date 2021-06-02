using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helper;
using MashadLeatherEcommerce.KiyanService;

namespace MashadLeatherEcommerce.Controllers
{
    public class _TestApiController : Controller
    {
        // GET: _TestApi
        public ActionResult Index()
        {
         

            //if (list.ResponseResult != null)
            //    InsertIntoKiyanLog(list.ResponseResult.Length, inventoryId);


            return View();
        }
    }

    public class FiledName
    {
        public string SocialSecurityNumber { get; set; }
        public string MemberID { get; set; }
        public string CustomerID { get; set; }
        public string Mobile { get; set; }
    }
}