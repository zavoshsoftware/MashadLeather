using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Helper;
using Models;
using ViewModels;

namespace MashadLeatherEcommerce.Controllers
{
    [Authorize(Roles = "Administrator,SuperAdministrator")]
    public class UsersController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            List<User> users = db.Users.Include(u => u.City).Where(u => u.IsDeleted == false && u.Role.Name == "Customer")
                .OrderByDescending(u => u.CreationDate).Include(u => u.Role).ToList();

            GridviewBind(users);

            return View(users);
        }

        public ActionResult GetUserExcel()
        {
            List<UserExcellViewModel> gridList = new List<UserExcellViewModel>();
            List<User> users = db.Users.Where(c => c.Role.Name == "customer" && c.IsDeleted == false).ToList();

            foreach (User user in users)
            {
                gridList.Add(new UserExcellViewModel
                {
                    CellNumber = user.CellNum,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                });
            }

            GridView gv = new GridView();
            gv.DataSource = gridList;
            gv.DataBind();
            gv.HeaderRow.Cells[0].Text = "نام";
            gv.HeaderRow.Cells[1].Text = "نام خانوادگی";
            gv.HeaderRow.Cells[2].Text = "شماره موبایل";
            gv.HeaderRow.Cells[3].Text = "ایمیل";

            Session["users"] = gv;

            return new DownloadFileActionResult((GridView)Session["users"], "users.xls");
        }


        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Title");
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Title");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,Password,CellNum,FirstName,LastName,Code,CityId,Address,PostalCode,Email,Token,RoleId,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] User user)
        {
            if (ModelState.IsValid)
            {
                user.IsDeleted = false;
                user.CreationDate = DateTime.Now;

                user.Id = Guid.NewGuid();
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(db.Cities, "Id", "Title", user.CityId);
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Title", user.RoleId);
            return View(user);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Title", user.CityId);
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Title", user.RoleId);
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Password,CellNum,FirstName,LastName,Code,CityId,Address,PostalCode,Email,Token,RoleId,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] User user)
        {
            if (ModelState.IsValid)
            {
                user.IsDeleted = false;
                user.LastModifiedDate = DateTime.Now;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Title", user.CityId);
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Title", user.RoleId);
            return View(user);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            User user = db.Users.Find(id);
            user.IsDeleted = true;
            user.DeletionDate = DateTime.Now;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public void GridviewBind(List<User> users)
        {
            List<ExcelGridviewUserReportViewModel> gridList = new List<ExcelGridviewUserReportViewModel>();
            foreach (User user in users)
            {

                gridList.Add(new ExcelGridviewUserReportViewModel
                {
                    Code = user.Code,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    CellNum = user.CellNum,
                    CreationDate = user.CreationDate
                });
            }

            GridView gv = new GridView();
            gv.DataSource = gridList;
            gv.DataBind();
            gv.HeaderRow.Cells[0].Text = "کد کاربر";
            gv.HeaderRow.Cells[1].Text = "نام";
            gv.HeaderRow.Cells[2].Text = "نام خانوادگی";
            gv.HeaderRow.Cells[3].Text = "موبایل";
            gv.HeaderRow.Cells[4].Text = "تاریخ ثبت نام";

            Session["users"] = gv;

            //gv.DataSource = gridList;
            //gv.DataBind();
            //Session["orders"] = gv;

        }


        public ActionResult DownloadReport()
        {
            if (Session["users"] != null)
            {
                return new DownloadFileActionResult((GridView)Session["users"], "users.xls");
            }
            else
            {
                return null;
            }
        }

        public void InsertOperators()
        {
            Insertinuser("ahmadabad", "S34531");
            Insertinuser("arak", "S34874");
            Insertinuser("ardebil", "S35217");
            Insertinuser("urmie", "S35560");
            Insertinuser("e-azar", "S35903");
            Insertinuser("e-charbagh", "S36246");
            Insertinuser("e-city", "S36589");
            Insertinuser("ardebil2", "S36932");
            Insertinuser("anzali", "S37275");
            Insertinuser("ahvaz", "S37618");
            Insertinuser("elam", "S37961");
            Insertinuser("armitaj", "S38304");
            Insertinuser("pasdaran", "S38647");
            Insertinuser("amol", "S38990");
            Insertinuser("babol", "S39333");
            Insertinuser("babolsar", "S39676");
            Insertinuser("bojnord", "S40019");
            Insertinuser("opal", "S40362");
            Insertinuser("boshehr", "S40705");
            Insertinuser("birjand", "S41048");
            Insertinuser("proma", "S41391");
            Insertinuser("tabriz", "S41734");
            Insertinuser("tamadon", "S42077");
            Insertinuser("janat", "S42420");
            Insertinuser("danesh", "S42763");
            Insertinuser("online-m", "S43106");
            Insertinuser("roze", "S43449");
            Insertinuser("rasht", "S43792");
            Insertinuser("reza", "S44135");
            Insertinuser("zahedan", "S44478");
            Insertinuser("zanjan", "S44821");
            Insertinuser("sari", "S45164");
            Insertinuser("sajad", "S45507");
            Insertinuser("online-t", "S45850");
            Insertinuser("hyper-m", "S57214");
            Insertinuser("semnan", "S58255");
            Insertinuser("sanandaj", "S59296");
            Insertinuser("razavi", "S60337");
            Insertinuser("kord", "S61378");
            Insertinuser("shiraz", "S62419");
            Insertinuser("ferdowsi", "S63460");
            Insertinuser("maysa", "S64501");
            Insertinuser("karfor", "S65542");
            Insertinuser("karaj", "S66583");
            Insertinuser("kohsangi", "S67624");
            Insertinuser("kerman", "S68665");
            Insertinuser("vanak", "S69706");
            Insertinuser("gorgan", "S70747");
            Insertinuser("golestan", "S71788");
            Insertinuser("panorama", "S72829");
            Insertinuser("maraghe", "S73870");
            Insertinuser("moalem", "S74911");
            Insertinuser("milad", "S75952");
            Insertinuser("rasht-gol", "S76993");
            Insertinuser("valiasr", "S78034");
            Insertinuser("ghasr", "S79075");
            Insertinuser("pazh", "S80116");
            Insertinuser("hamedan", "S81157");
            Insertinuser("shiraz-satar", "S82198");
            Insertinuser("yazd-safa", "S83239");
            Insertinuser("yazd-fa", "S84280");
            Insertinuser("tonekabon", "S85321");
            Insertinuser("golshan", "S86362");
            Insertinuser("yazd-markar", "S87403");
            Insertinuser("iran", "S88444");
            Insertinuser("sivan", "S89485");
            Insertinuser("arg", "S90526");
            Insertinuser("borojerd", "S91567");
            Insertinuser("dolat", "S92608");
        }

        public void Insertinuser(string cellNumber, string pass)
        {
            try
            {

        
            Guid roleid = new Guid("F145BD6B-966C-4E19-AF46-5F3B7B571C7B");
           Models.User user = new Models.User()
            {
                Id = Guid.NewGuid(),
                CellNum = cellNumber,
                Password = pass,
                Code = 1,
                CreationDate = DateTime.Now,
                IsActive = true,
                IsDeleted = false,
                RoleId = roleid,
              

            };
            db.Users.Add(user);
            db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {

                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }
}
