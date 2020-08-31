﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;

namespace MashadLeatherEcommerce.Controllers
{
    public class TextsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Texts
        public ActionResult Index(Guid id)
        {
            var texts = db.Texts.Include(t => t.TextType).Where(t=>t.TextTypeId==id&& t.IsDeleted==false).OrderByDescending(t=>t.CreationDate);
            return View(texts.ToList());
        }
         
        public ActionResult Create()
        {
            ViewBag.TextTypeId = new SelectList(db.TextTypes, "Id", "Title");
            return View();
        }

        // POST: Texts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Text text, HttpPostedFileBase fileupload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                string newFilenameUrl = string.Empty;
                if (fileupload != null)
                {
                    string filename = Path.GetFileName(fileupload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/text/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    text.ImageUrl = newFilenameUrl;
                }
                #endregion
                text.IsDeleted=false;
				text.CreationDate= DateTime.Now; 
					
                text.Id = Guid.NewGuid();
                db.Texts.Add(text);
                db.SaveChanges();
                return RedirectToAction("Index",new{id=text.TextTypeId});
            }

            ViewBag.TextTypeId = new SelectList(db.TextTypes, "Id", "Title", text.TextTypeId);
            return View(text);
        }

        // GET: Texts/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Text text = db.Texts.Find(id);
            if (text == null)
            {
                return HttpNotFound();
            }
            ViewBag.TextTypeId = text.TextTypeId;
            return View(text);
        }

        // POST: Texts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Text text, HttpPostedFileBase fileupload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                string newFilenameUrl = string.Empty;
                if (fileupload != null)
                {
                    string filename = Path.GetFileName(fileupload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/text/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    text.ImageUrl = newFilenameUrl;
                }
                #endregion
                text.IsDeleted=false;
					text.LastModifiedDate=DateTime.Now;
                db.Entry(text).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index",new {id=text.TextTypeId});
            }
            ViewBag.TextTypeId = text.TextTypeId;

            return View(text);
        }

        // GET: Texts/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Text text = db.Texts.Find(id);
            if (text == null)
            {
                return HttpNotFound();
            }
            return View(text);
        }

        // POST: Texts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Text text = db.Texts.Find(id);
			text.IsDeleted=true;
			text.DeletionDate=DateTime.Now;
 
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
