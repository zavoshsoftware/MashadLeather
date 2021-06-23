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
    public class CarreerPreviousExperiencesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: CarreerPreviousExperiences
        public ActionResult Index()
        {
            var carreerPreviousExperiences = db.CarreerPreviousExperiences.Include(c => c.Carreer).Where(c=>c.IsDeleted==false).OrderByDescending(c=>c.CreationDate);
            return View(carreerPreviousExperiences.ToList());
        }

        // GET: CarreerPreviousExperiences/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarreerPreviousExperience carreerPreviousExperience = db.CarreerPreviousExperiences.Find(id);
            if (carreerPreviousExperience == null)
            {
                return HttpNotFound();
            }
            return View(carreerPreviousExperience);
        }

        // GET: CarreerPreviousExperiences/Create
        public ActionResult Create()
        {
            ViewBag.CarreerId = new SelectList(db.Carreers.Where(c => c.IsDeleted == false), "Id", "FullName");
            return View();
        }

        // POST: CarreerPreviousExperiences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarreerPreviousExperience carreerPreviousExperience)
        {
            if (ModelState.IsValid)
            {
				carreerPreviousExperience.IsDeleted=false;
				carreerPreviousExperience.CreationDate= DateTime.Now; 
					
                carreerPreviousExperience.Id = Guid.NewGuid();
                db.CarreerPreviousExperiences.Add(carreerPreviousExperience);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarreerId = new SelectList(db.Carreers.Where(c => c.IsDeleted == false), "Id", "FullName", carreerPreviousExperience.CarreerId);
            return View(carreerPreviousExperience);
        }

        // GET: CarreerPreviousExperiences/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarreerPreviousExperience carreerPreviousExperience = db.CarreerPreviousExperiences.Find(id);
            if (carreerPreviousExperience == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarreerId = new SelectList(db.Carreers.Where(c => c.IsDeleted == false), "Id", "FullName", carreerPreviousExperience.CarreerId);
            return View(carreerPreviousExperience);
        }

        // POST: CarreerPreviousExperiences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CarreerPreviousExperience carreerPreviousExperience)
        {
            if (ModelState.IsValid)
            {
				carreerPreviousExperience.IsDeleted=false;
					carreerPreviousExperience.LastModifiedDate=DateTime.Now;
                db.Entry(carreerPreviousExperience).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarreerId = new SelectList(db.Carreers.Where(c => c.IsDeleted == false), "Id", "FullName", carreerPreviousExperience.CarreerId);
            return View(carreerPreviousExperience);
        }

        // GET: CarreerPreviousExperiences/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarreerPreviousExperience carreerPreviousExperience = db.CarreerPreviousExperiences.Find(id);
            if (carreerPreviousExperience == null)
            {
                return HttpNotFound();
            }
            return View(carreerPreviousExperience);
        }

        // POST: CarreerPreviousExperiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CarreerPreviousExperience carreerPreviousExperience = db.CarreerPreviousExperiences.Find(id);
			carreerPreviousExperience.IsDeleted=true;
			carreerPreviousExperience.DeletionDate=DateTime.Now;
 
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
