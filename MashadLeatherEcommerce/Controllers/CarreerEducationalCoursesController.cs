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
    public class CarreerEducationalCoursesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: CarreerEducationalCourses
        public ActionResult Index()
        {
            var carreerEducationalCourses = db.CarreerEducationalCourses.Include(c => c.Carreer).Where(c=>c.IsDeleted==false).OrderByDescending(c=>c.CreationDate);
            return View(carreerEducationalCourses.ToList());
        }

        // GET: CarreerEducationalCourses/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarreerEducationalCourse carreerEducationalCourse = db.CarreerEducationalCourses.Find(id);
            if (carreerEducationalCourse == null)
            {
                return HttpNotFound();
            }
            return View(carreerEducationalCourse);
        }

        // GET: CarreerEducationalCourses/Create
        public ActionResult Create()
        {
            ViewBag.CarreerId = new SelectList(db.Carreers.Where(c => c.IsDeleted == false), "Id", "FullName");
            return View();
        }

        // POST: CarreerEducationalCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarreerEducationalCourse carreerEducationalCourse)
        {
            if (ModelState.IsValid)
            {
				carreerEducationalCourse.IsDeleted=false;
				carreerEducationalCourse.CreationDate= DateTime.Now; 
					
                carreerEducationalCourse.Id = Guid.NewGuid();
                db.CarreerEducationalCourses.Add(carreerEducationalCourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarreerId = new SelectList(db.Carreers.Where(c => c.IsDeleted == false), "Id", "FullName", carreerEducationalCourse.CarreerId);
            return View(carreerEducationalCourse);
        }

        // GET: CarreerEducationalCourses/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarreerEducationalCourse carreerEducationalCourse = db.CarreerEducationalCourses.Find(id);
            if (carreerEducationalCourse == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarreerId = new SelectList(db.Carreers.Where(c => c.IsDeleted == false), "Id", "FullName", carreerEducationalCourse.CarreerId);
            return View(carreerEducationalCourse);
        }

        // POST: CarreerEducationalCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CarreerEducationalCourse carreerEducationalCourse)
        {
            if (ModelState.IsValid)
            {
				carreerEducationalCourse.IsDeleted=false;
					carreerEducationalCourse.LastModifiedDate=DateTime.Now;
                db.Entry(carreerEducationalCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarreerId = new SelectList(db.Carreers.Where(c => c.IsDeleted == false), "Id", "FullName", carreerEducationalCourse.CarreerId);
            return View(carreerEducationalCourse);
        }

        // GET: CarreerEducationalCourses/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarreerEducationalCourse carreerEducationalCourse = db.CarreerEducationalCourses.Find(id);
            if (carreerEducationalCourse == null)
            {
                return HttpNotFound();
            }
            return View(carreerEducationalCourse);
        }

        // POST: CarreerEducationalCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CarreerEducationalCourse carreerEducationalCourse = db.CarreerEducationalCourses.Find(id);
			carreerEducationalCourse.IsDeleted=true;
			carreerEducationalCourse.DeletionDate=DateTime.Now;
 
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
