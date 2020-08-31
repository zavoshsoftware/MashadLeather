using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
            List<User> users = db.Users.Include(u => u.City).Where(u => u.IsDeleted == false&&u.Role.Name== "Customer")
                .OrderByDescending(u => u.CreationDate).Include(u => u.Role).ToList();

            GridviewBind(users);

            return View(users);
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
				user.IsDeleted=false;
				user.CreationDate= DateTime.Now; 
					
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
				user.IsDeleted=false;
					user.LastModifiedDate=DateTime.Now;
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
			user.IsDeleted=true;
			user.DeletionDate=DateTime.Now;
 
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
    }
}
