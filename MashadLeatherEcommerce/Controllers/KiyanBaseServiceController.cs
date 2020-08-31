using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;

namespace MashadLeatherEcommerce.Controllers
{
    public class KiyanBaseServiceController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult GetProductCategory()
        {
            try
            {
                KiyanService.KyanOnlineSaleServiceSoapClient kiyanService = new KiyanService.KyanOnlineSaleServiceSoapClient();

                KiyanService.ValidationSoapHeader header = new KiyanService.ValidationSoapHeader();
                header.Token = "Charm@#$568";

                var list = kiyanService.GetPosDepartmentList(header);


                foreach (var item in list.ResponseResult)
                {
                    KiyanProductCategory kianCategory = db.KiyanProductCategories.Where(current =>
                        current.IsDeleted == false && current.PosDepartmentId == item.POSDepartmentID).FirstOrDefault();

                    if (kianCategory == null)
                    {
                        KiyanProductCategory kiyanProductCategory = new KiyanProductCategory()
                        {
                            Name = item.Name,
                            PosDepartmentId = item.POSDepartmentID,
                            CreationDate = DateTime.Now,
                            IsDeleted = false,
                            IsActive = true

                        };

                        db.KiyanProductCategories.Add(kiyanProductCategory);
                    }
                    else if (kianCategory.Name != item.Name)
                    {
                        kianCategory.Name = item.Name;
                        kianCategory.LastModifiedDate=DateTime.Now;
                    }
                }

                db.SaveChanges();

                return Json("true", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }
    }
}