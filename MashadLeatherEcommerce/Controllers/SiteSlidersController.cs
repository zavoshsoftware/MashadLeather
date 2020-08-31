using System;
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
    public class SiteSlidersController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: SiteSliders
        public ActionResult Index()
        {
            return View(db.SiteSliders.Where(a=>a.IsDeleted==false).OrderByDescending(a=>a.CreationDate).ToList());
        }

        // GET: SiteSliders/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteSlider siteSlider = db.SiteSliders.Find(id);
            if (siteSlider == null)
            {
                return HttpNotFound();
            }
            return View(siteSlider);
        }

        // GET: SiteSliders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SiteSliders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SiteSlider siteSlider, HttpPostedFileBase fileupload, HttpPostedFileBase fileuploadEn, HttpPostedFileBase fileuploadAr)
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

                    newFilenameUrl = "/Uploads/slider/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    siteSlider.ImageUrl = newFilenameUrl;
                }
                if (fileuploadEn != null)
                {
                    string filename = Path.GetFileName(fileuploadEn.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrlEn = "/Uploads/product/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrlEn);
                    fileuploadEn.SaveAs(physicalFilename);
                    siteSlider.ImageUrlEn = newFilenameUrlEn;
                }
                if (fileuploadAr != null)
                {
                    string filename = Path.GetFileName(fileuploadAr.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrlAr = "/Uploads/product/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrlAr);
                    fileuploadAr.SaveAs(physicalFilename);
                    siteSlider.ImageUrlAr = newFilenameUrlAr;
                }
                #endregion
                siteSlider.IsDeleted=false;
				siteSlider.CreationDate= DateTime.Now; 
					
                siteSlider.Id = Guid.NewGuid();
                db.SiteSliders.Add(siteSlider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(siteSlider);
        }

        // GET: SiteSliders/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteSlider siteSlider = db.SiteSliders.Find(id);
            if (siteSlider == null)
            {
                return HttpNotFound();
            }
            return View(siteSlider);
        }

        // POST: SiteSliders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SiteSlider siteSlider, HttpPostedFileBase fileupload, HttpPostedFileBase fileuploadEn, HttpPostedFileBase fileuploadAr)
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

                    newFilenameUrl = "/Uploads/slider/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    siteSlider.ImageUrl = newFilenameUrl;
                }
                if (fileuploadEn != null)
                {
                    string filename = Path.GetFileName(fileuploadEn.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrlEn = "/Uploads/product/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrlEn);
                    fileuploadEn.SaveAs(physicalFilename);
                    siteSlider.ImageUrlEn = newFilenameUrlEn;
                }
                if (fileuploadAr != null)
                {
                    string filename = Path.GetFileName(fileuploadAr.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrlAr = "/Uploads/product/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrlAr);
                    fileuploadAr.SaveAs(physicalFilename);
                    siteSlider.ImageUrlAr = newFilenameUrlAr;
                }
                #endregion
                siteSlider.IsDeleted=false;
					siteSlider.LastModifiedDate=DateTime.Now;
                db.Entry(siteSlider).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(siteSlider);
        }

        // GET: SiteSliders/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteSlider siteSlider = db.SiteSliders.Find(id);
            if (siteSlider == null)
            {
                return HttpNotFound();
            }
            return View(siteSlider);
        }

        // POST: SiteSliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            SiteSlider siteSlider = db.SiteSliders.Find(id);
			siteSlider.IsDeleted=true;
			siteSlider.DeletionDate=DateTime.Now;
 
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
