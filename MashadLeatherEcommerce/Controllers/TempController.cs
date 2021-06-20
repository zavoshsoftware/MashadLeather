using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MashadLeatherEcommerce.Controllers
{
    public class TempController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        public ActionResult Index()
        {

            for (int i = 38; i <=58; i++)
            {
                Models.Size size = new Size()
                {
                    Id = Guid.NewGuid(),
                    Title=i.ToString(),
                    BarCodeProductGroup="w",
                    CreationDate=DateTime.Now,
                    IsActive=true,
                    IsDeleted=false,
                    
                };

                db.Sizes.Add(size);
                db.SaveChanges();
            }

            return View();
        }
    }
}