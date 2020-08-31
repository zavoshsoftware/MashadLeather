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
    public class SiteGalleryGroupsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: SiteGalleryGroups
        public ActionResult Index()
        {
            return View(db.SiteGalleryGroups.Where(a=>a.IsDeleted==false).OrderByDescending(a=>a.CreationDate).ToList());
        }

        // GET: SiteGalleryGroups/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteGalleryGroup siteGalleryGroup = db.SiteGalleryGroups.Find(id);
            if (siteGalleryGroup == null)
            {
                return HttpNotFound();
            }
            return View(siteGalleryGroup);
        }

        // GET: SiteGalleryGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SiteGalleryGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SiteGalleryGroup siteGalleryGroup)
        {
            if (ModelState.IsValid)
            {
				siteGalleryGroup.IsDeleted=false;
				siteGalleryGroup.CreationDate= DateTime.Now; 
					
                siteGalleryGroup.Id = Guid.NewGuid();
                db.SiteGalleryGroups.Add(siteGalleryGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(siteGalleryGroup);
        }

        // GET: SiteGalleryGroups/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteGalleryGroup siteGalleryGroup = db.SiteGalleryGroups.Find(id);
            if (siteGalleryGroup == null)
            {
                return HttpNotFound();
            }
            return View(siteGalleryGroup);
        }

        // POST: SiteGalleryGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SiteGalleryGroup siteGalleryGroup)
        {
            if (ModelState.IsValid)
            {
				siteGalleryGroup.IsDeleted=false;
					siteGalleryGroup.LastModifiedDate=DateTime.Now;
                db.Entry(siteGalleryGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(siteGalleryGroup);
        }

        // GET: SiteGalleryGroups/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteGalleryGroup siteGalleryGroup = db.SiteGalleryGroups.Find(id);
            if (siteGalleryGroup == null)
            {
                return HttpNotFound();
            }
            return View(siteGalleryGroup);
        }

        // POST: SiteGalleryGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            SiteGalleryGroup siteGalleryGroup = db.SiteGalleryGroups.Find(id);
			siteGalleryGroup.IsDeleted=true;
			siteGalleryGroup.DeletionDate=DateTime.Now;
 
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
