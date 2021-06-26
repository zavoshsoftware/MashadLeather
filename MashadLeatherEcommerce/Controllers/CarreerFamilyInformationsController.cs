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
    public class CarreerFamilyInformationsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: CarreerFamilyInformations
        public ActionResult Index()
        {
            var carreerFamilyInformations = db.CarreerFamilyInformations.Include(c => c.Carreer).Where(c=>c.IsDeleted==false).OrderByDescending(c=>c.CreationDate);
            return View(carreerFamilyInformations.ToList());
        }

        // GET: CarreerFamilyInformations/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarreerFamilyInformation carreerFamilyInformation = db.CarreerFamilyInformations.Find(id);
            if (carreerFamilyInformation == null)
            {
                return HttpNotFound();
            }
            return View(carreerFamilyInformation);
        }

        // GET: CarreerFamilyInformations/Create
        public ActionResult Create()
        {
            ViewBag.CarreerId = new SelectList(db.Carreers.Where(c => c.IsDeleted == false), "Id", "FullName");
            return View();
        }

        // POST: CarreerFamilyInformations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarreerFamilyInformation carreerFamilyInformation)
        {
            if (ModelState.IsValid)
            {
				carreerFamilyInformation.IsDeleted=false;
				carreerFamilyInformation.CreationDate= DateTime.Now; 
					
                carreerFamilyInformation.Id = Guid.NewGuid();
                db.CarreerFamilyInformations.Add(carreerFamilyInformation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarreerId = new SelectList(db.Carreers.Where(c => c.IsDeleted == false), "Id", "FullName", carreerFamilyInformation.CarreerId);
            return View(carreerFamilyInformation);
        }

        // GET: CarreerFamilyInformations/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarreerFamilyInformation carreerFamilyInformation = db.CarreerFamilyInformations.Find(id);
            if (carreerFamilyInformation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarreerId = new SelectList(db.Carreers.Where(c => c.IsDeleted == false), "Id", "FullName", carreerFamilyInformation.CarreerId);
            return View(carreerFamilyInformation);
        }

        // POST: CarreerFamilyInformations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CarreerFamilyInformation carreerFamilyInformation)
        {
            if (ModelState.IsValid)
            {
				carreerFamilyInformation.IsDeleted=false;
					carreerFamilyInformation.LastModifiedDate=DateTime.Now;
                db.Entry(carreerFamilyInformation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarreerId = new SelectList(db.Carreers.Where(c => c.IsDeleted == false), "Id", "FullName", carreerFamilyInformation.CarreerId);
            return View(carreerFamilyInformation);
        }

        // GET: CarreerFamilyInformations/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarreerFamilyInformation carreerFamilyInformation = db.CarreerFamilyInformations.Find(id);
            if (carreerFamilyInformation == null)
            {
                return HttpNotFound();
            }
            return View(carreerFamilyInformation);
        }

        // POST: CarreerFamilyInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CarreerFamilyInformation carreerFamilyInformation = db.CarreerFamilyInformations.Find(id);
			carreerFamilyInformation.IsDeleted=true;
			carreerFamilyInformation.DeletionDate=DateTime.Now;
 
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
