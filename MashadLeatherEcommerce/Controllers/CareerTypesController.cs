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
    public class CareerTypesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: CareerTypes
        public ActionResult Index()
        {
            return View(db.CareerTypes.Where(a=>a.IsDeleted==false).OrderByDescending(a=>a.CreationDate).ToList());
        }

        // GET: CareerTypes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CareerType careerType = db.CareerTypes.Find(id);
            if (careerType == null)
            {
                return HttpNotFound();
            }
            return View(careerType);
        }

        // GET: CareerTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CareerTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description,DescriptionEn,DescriptionAr")] CareerType careerType)
        {
            if (ModelState.IsValid)
            {
				careerType.IsDeleted=false;
				careerType.CreationDate= DateTime.Now; 
					
                careerType.Id = Guid.NewGuid();
                db.CareerTypes.Add(careerType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(careerType);
        }

        // GET: CareerTypes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CareerType careerType = db.CareerTypes.Find(id);
            if (careerType == null)
            {
                return HttpNotFound();
            }
            return View(careerType);
        }

        // POST: CareerTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description,DescriptionEn,DescriptionAr")] CareerType careerType)
        {
            if (ModelState.IsValid)
            {
				careerType.IsDeleted=false;
					careerType.LastModifiedDate=DateTime.Now;
                db.Entry(careerType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(careerType);
        }

        // GET: CareerTypes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CareerType careerType = db.CareerTypes.Find(id);
            if (careerType == null)
            {
                return HttpNotFound();
            }
            return View(careerType);
        }

        // POST: CareerTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CareerType careerType = db.CareerTypes.Find(id);
			careerType.IsDeleted=true;
			careerType.DeletionDate=DateTime.Now;
 
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
