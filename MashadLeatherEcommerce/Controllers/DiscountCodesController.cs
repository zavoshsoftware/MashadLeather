﻿using System;
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
    public class DiscountCodesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: DiscountCodes
        public ActionResult Index()
        {
            var discountCodes = db.DiscountCodes.Include(d => d.User).Where(d=>d.IsDeleted==false).OrderByDescending(d=>d.CreationDate);
            return View(discountCodes.ToList());
        }

        // GET: DiscountCodes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountCode discountCode = db.DiscountCodes.Find(id);
            if (discountCode == null)
            {
                return HttpNotFound();
            }
            return View(discountCode);
        }

        // GET: DiscountCodes/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username");
            return View();
        }

        // POST: DiscountCodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,ExpireDate,IsPercent,Amount,IsMultiUsing,UserId,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description,DescriptionEn,DescriptionAr")] DiscountCode discountCode)
        {
            if (ModelState.IsValid)
            {
				discountCode.IsDeleted=false;
				discountCode.CreationDate= DateTime.Now; 
					
                discountCode.Id = Guid.NewGuid();
                db.DiscountCodes.Add(discountCode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", discountCode.UserId);
            return View(discountCode);
        }

        // GET: DiscountCodes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountCode discountCode = db.DiscountCodes.Find(id);
            if (discountCode == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", discountCode.UserId);
            return View(discountCode);
        }

        // POST: DiscountCodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,ExpireDate,IsPercent,Amount,IsMultiUsing,UserId,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description,DescriptionEn,DescriptionAr")] DiscountCode discountCode)
        {
            if (ModelState.IsValid)
            {
				discountCode.IsDeleted=false;
					discountCode.LastModifiedDate=DateTime.Now;
                db.Entry(discountCode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", discountCode.UserId);
            return View(discountCode);
        }

        // GET: DiscountCodes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountCode discountCode = db.DiscountCodes.Find(id);
            if (discountCode == null)
            {
                return HttpNotFound();
            }
            return View(discountCode);
        }

        // POST: DiscountCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            DiscountCode discountCode = db.DiscountCodes.Find(id);
			discountCode.IsDeleted=true;
			discountCode.DeletionDate=DateTime.Now;
 
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
