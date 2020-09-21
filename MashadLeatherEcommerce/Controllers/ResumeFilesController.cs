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
    public class ResumeFilesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: ResumeFiles
        public ActionResult Index()
        {
            return View(db.ResumeFiles.Where(a=>a.IsDeleted==false).OrderByDescending(a=>a.CreationDate).ToList());
        }

        // GET: ResumeFiles/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResumeFile resumeFile = db.ResumeFiles.Find(id);
            if (resumeFile == null)
            {
                return HttpNotFound();
            }
            return View(resumeFile);
        }

        // GET: ResumeFiles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ResumeFiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FileUrl,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description,DescriptionEn,DescriptionAr")] ResumeFile resumeFile)
        {
            if (ModelState.IsValid)
            {
				resumeFile.IsDeleted=false;
				resumeFile.CreationDate= DateTime.Now; 
					
                resumeFile.Id = Guid.NewGuid();
                db.ResumeFiles.Add(resumeFile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(resumeFile);
        }

        // GET: ResumeFiles/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResumeFile resumeFile = db.ResumeFiles.Find(id);
            if (resumeFile == null)
            {
                return HttpNotFound();
            }
            return View(resumeFile);
        }

        // POST: ResumeFiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FileUrl,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description,DescriptionEn,DescriptionAr")] ResumeFile resumeFile)
        {
            if (ModelState.IsValid)
            {
				resumeFile.IsDeleted=false;
					resumeFile.LastModifiedDate=DateTime.Now;
                db.Entry(resumeFile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(resumeFile);
        }

        // GET: ResumeFiles/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResumeFile resumeFile = db.ResumeFiles.Find(id);
            if (resumeFile == null)
            {
                return HttpNotFound();
            }
            return View(resumeFile);
        }

        // POST: ResumeFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ResumeFile resumeFile = db.ResumeFiles.Find(id);
			resumeFile.IsDeleted=true;
			resumeFile.DeletionDate=DateTime.Now;
 
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
