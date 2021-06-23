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
    public class CarreerIntroducedsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: CarreerIntroduceds
        public ActionResult Index()
        {
            var carreerIntroduceds = db.CarreerIntroduceds.Include(c => c.Carreer).Where(c=>c.IsDeleted==false).OrderByDescending(c=>c.CreationDate);
            return View(carreerIntroduceds.ToList());
        }

        // GET: CarreerIntroduceds/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarreerIntroduced carreerIntroduced = db.CarreerIntroduceds.Find(id);
            if (carreerIntroduced == null)
            {
                return HttpNotFound();
            }
            return View(carreerIntroduced);
        }

        // GET: CarreerIntroduceds/Create
        public ActionResult Create()
        {
            ViewBag.CarreerId = new SelectList(db.Carreers.Where(c => c.IsDeleted == false), "Id", "FullName");
            return View();
        }

        // POST: CarreerIntroduceds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarreerIntroduced carreerIntroduced)
        {
            if (ModelState.IsValid)
            {
				carreerIntroduced.IsDeleted=false;
				carreerIntroduced.CreationDate= DateTime.Now; 
					
                carreerIntroduced.Id = Guid.NewGuid();
                db.CarreerIntroduceds.Add(carreerIntroduced);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarreerId = new SelectList(db.Carreers.Where(c => c.IsDeleted == false), "Id", "FullName", carreerIntroduced.CarreerId);
            return View(carreerIntroduced);
        }

        // GET: CarreerIntroduceds/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarreerIntroduced carreerIntroduced = db.CarreerIntroduceds.Find(id);
            if (carreerIntroduced == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarreerId = new SelectList(db.Carreers.Where(c => c.IsDeleted == false), "Id", "FullName", carreerIntroduced.CarreerId);
            return View(carreerIntroduced);
        }

        // POST: CarreerIntroduceds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CarreerIntroduced carreerIntroduced)
        {
            if (ModelState.IsValid)
            {
				carreerIntroduced.IsDeleted=false;
					carreerIntroduced.LastModifiedDate=DateTime.Now;
                db.Entry(carreerIntroduced).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarreerId = new SelectList(db.Carreers.Where(c => c.IsDeleted == false), "Id", "FullName", carreerIntroduced.CarreerId);
            return View(carreerIntroduced);
        }

        // GET: CarreerIntroduceds/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarreerIntroduced carreerIntroduced = db.CarreerIntroduceds.Find(id);
            if (carreerIntroduced == null)
            {
                return HttpNotFound();
            }
            return View(carreerIntroduced);
        }

        // POST: CarreerIntroduceds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CarreerIntroduced carreerIntroduced = db.CarreerIntroduceds.Find(id);
			carreerIntroduced.IsDeleted=true;
			carreerIntroduced.DeletionDate=DateTime.Now;
 
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
