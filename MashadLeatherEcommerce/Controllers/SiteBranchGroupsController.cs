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
    public class SiteBranchGroupsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: SiteBranchGroups
        public ActionResult Index()
        {
            return View(db.SiteBranchGroups.Where(a=>a.IsDeleted==false).OrderByDescending(a=>a.CreationDate).ToList());
        }

        // GET: SiteBranchGroups/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteBranchGroup siteBranchGroup = db.SiteBranchGroups.Find(id);
            if (siteBranchGroup == null)
            {
                return HttpNotFound();
            }
            return View(siteBranchGroup);
        }

        // GET: SiteBranchGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SiteBranchGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( SiteBranchGroup siteBranchGroup)
        {
            if (ModelState.IsValid)
            {
				siteBranchGroup.IsDeleted=false;
				siteBranchGroup.CreationDate= DateTime.Now; 
					
                siteBranchGroup.Id = Guid.NewGuid();
                db.SiteBranchGroups.Add(siteBranchGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(siteBranchGroup);
        }

        // GET: SiteBranchGroups/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteBranchGroup siteBranchGroup = db.SiteBranchGroups.Find(id);
            if (siteBranchGroup == null)
            {
                return HttpNotFound();
            }
            return View(siteBranchGroup);
        }

        // POST: SiteBranchGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SiteBranchGroup siteBranchGroup)
        {
            if (ModelState.IsValid)
            {
				siteBranchGroup.IsDeleted=false;
					siteBranchGroup.LastModifiedDate=DateTime.Now;
                db.Entry(siteBranchGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(siteBranchGroup);
        }

        // GET: SiteBranchGroups/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteBranchGroup siteBranchGroup = db.SiteBranchGroups.Find(id);
            if (siteBranchGroup == null)
            {
                return HttpNotFound();
            }
            return View(siteBranchGroup);
        }

        // POST: SiteBranchGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            SiteBranchGroup siteBranchGroup = db.SiteBranchGroups.Find(id);
			siteBranchGroup.IsDeleted=true;
			siteBranchGroup.DeletionDate=DateTime.Now;
 
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
