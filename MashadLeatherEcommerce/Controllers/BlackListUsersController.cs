using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;

namespace MashadLeatherEcommerce.Controllers
{
    [Authorize(Roles = "Administrator,SuperAdministrator")]
    public class BlackListUsersController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: BlackListUsers
        public ActionResult Index()
        {
            return View(db.BlackListUsers.Where(a=>a.IsDeleted==false).OrderByDescending(a=>a.CreationDate).ToList());
        }

        // GET: BlackListUsers/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlackListUser blackListUser = db.BlackListUsers.Find(id);
            if (blackListUser == null)
            {
                return HttpNotFound();
            }
            return View(blackListUser);
        }

        // GET: BlackListUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlackListUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CellNumber,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description,DescriptionEn,DescriptionAr")] BlackListUser blackListUser)
        {
            if (ModelState.IsValid)
            {
				blackListUser.IsDeleted=false;
				blackListUser.CreationDate= DateTime.Now; 
					
                blackListUser.Id = Guid.NewGuid();
                db.BlackListUsers.Add(blackListUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blackListUser);
        }

        // GET: BlackListUsers/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlackListUser blackListUser = db.BlackListUsers.Find(id);
            if (blackListUser == null)
            {
                return HttpNotFound();
            }
            return View(blackListUser);
        }

        // POST: BlackListUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CellNumber,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description,DescriptionEn,DescriptionAr")] BlackListUser blackListUser)
        {
            if (ModelState.IsValid)
            {
				blackListUser.IsDeleted=false;
					blackListUser.LastModifiedDate=DateTime.Now;
                db.Entry(blackListUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blackListUser);
        }

        // GET: BlackListUsers/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlackListUser blackListUser = db.BlackListUsers.Find(id);
            if (blackListUser == null)
            {
                return HttpNotFound();
            }
            return View(blackListUser);
        }

        // POST: BlackListUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            BlackListUser blackListUser = db.BlackListUsers.Find(id);
			blackListUser.IsDeleted=true;
			blackListUser.DeletionDate=DateTime.Now;
 
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
    }
}
