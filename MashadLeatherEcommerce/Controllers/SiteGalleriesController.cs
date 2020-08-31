using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eShopMvc;
using Helper;
using Models;
using ViewModels;

namespace MashadLeatherEcommerce.Controllers
{
    public class SiteGalleriesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: SiteGalleries
        public ActionResult Index()
        {
            var siteGalleries = db.SiteGalleries.Include(s => s.SiteGalleryGroup).Where(s => s.IsDeleted == false).OrderByDescending(s => s.CreationDate);
            return View(siteGalleries.ToList());
        }
 
        public ActionResult Create()
        {
            ViewBag.SiteGalleryGroupId = new SelectList(db.SiteGalleryGroups, "Id", "Title");
            return View();
        }

        // POST: SiteGalleries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SiteGallery siteGallery, HttpPostedFileBase fileupload, HttpPostedFileBase fileuploadFile)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed

                string newFilenameUrl = string.Empty;
                string thumbnailUrl = string.Empty;
                if (fileupload != null)
                {
                    string filename = Path.GetFileName(fileupload.FileName);
                    string newFilename = Guid.NewGuid().ToString()
                                             .Replace("-", string.Empty) +
                                         Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/gallery/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    fileupload.SaveAs(physicalFilename);
                 //   thumbnailUrl = Utils.CreateThumbnail(physicalFilename);
                    siteGallery.ImageUrl = newFilenameUrl;
                   // siteGallery.ImageUrlThumb = thumbnailUrl;
                }

                if (fileuploadFile != null)
                {
                    string filename = Path.GetFileName(fileuploadFile.FileName);
                    string newFilename = Guid.NewGuid().ToString()
                                             .Replace("-", string.Empty) +
                                         Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/gallery/files/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    fileuploadFile.SaveAs(physicalFilename);
                 
                    siteGallery.FileUrl = newFilenameUrl;
                }


                #endregion
                siteGallery.IsDeleted = false;
                siteGallery.CreationDate = DateTime.Now;

                siteGallery.Id = Guid.NewGuid();
                db.SiteGalleries.Add(siteGallery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SiteGalleryGroupId = new SelectList(db.SiteGalleryGroups, "Id", "Title", siteGallery.SiteGalleryGroupId);
            return View(siteGallery);
        }

        // GET: SiteGalleries/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteGallery siteGallery = db.SiteGalleries.Find(id);
            if (siteGallery == null)
            {
                return HttpNotFound();
            }
            ViewBag.SiteGalleryGroupId = new SelectList(db.SiteGalleryGroups, "Id", "Title", siteGallery.SiteGalleryGroupId);
            return View(siteGallery);
        }

        // POST: SiteGalleries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SiteGallery siteGallery, HttpPostedFileBase fileupload, HttpPostedFileBase fileuploadFile)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed

                string newFilenameUrl = string.Empty;
                string thumbnailUrl = string.Empty;
                if (fileupload != null)
                {
                    string filename = Path.GetFileName(fileupload.FileName);
                    string newFilename = Guid.NewGuid().ToString()
                                             .Replace("-", string.Empty) +
                                         Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/gallery/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    fileupload.SaveAs(physicalFilename);
                //    thumbnailUrl = Utils.CreateThumbnail(physicalFilename);
                    siteGallery.ImageUrl = newFilenameUrl;
                //    siteGallery.ImageUrlThumb = thumbnailUrl;
                }

                if (fileuploadFile != null)
                {
                    string filename = Path.GetFileName(fileuploadFile.FileName);
                    string newFilename = Guid.NewGuid().ToString()
                                             .Replace("-", string.Empty) +
                                         Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/gallery/files/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    fileuploadFile.SaveAs(physicalFilename);

                    siteGallery.FileUrl = newFilenameUrl;
                }


                #endregion
                siteGallery.IsDeleted = false;
                siteGallery.LastModifiedDate = DateTime.Now;
                db.Entry(siteGallery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SiteGalleryGroupId = new SelectList(db.SiteGalleryGroups, "Id", "Title", siteGallery.SiteGalleryGroupId);
            return View(siteGallery);
        }

        // GET: SiteGalleries/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteGallery siteGallery = db.SiteGalleries.Find(id);
            if (siteGallery == null)
            {
                return HttpNotFound();
            }
            return View(siteGallery);
        }

        // POST: SiteGalleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            SiteGallery siteGallery = db.SiteGalleries.Find(id);
            siteGallery.IsDeleted = true;
            siteGallery.DeletionDate = DateTime.Now;

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

        Helper.BaseViewModelHelper baseViewModelHelper = new BaseViewModelHelper();

        [AllowAnonymous]
        [Route("gallery/{urlParam}")]
        public ActionResult List(string urlParam)
        {
            SiteGalleryGroup siteGalleryGroup = db.SiteGalleryGroups.FirstOrDefault(current => current.UrlParam == urlParam);

            if (siteGalleryGroup == null)
                return RedirectPermanent("/");

            GalleryListViewModel galleryList = new GalleryListViewModel()
            {
                MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups(),
                MenuItem = baseViewModelHelper.GetMenuItems(),
                GalleryGroup = siteGalleryGroup,
                Galleries = db.SiteGalleries
                    .Where(current => current.SiteGalleryGroupId == siteGalleryGroup.Id && current.IsActive&&current.IsDeleted==false).OrderBy(x=>x.Order).ToList()
            };

            return View(galleryList);
        }


        [AllowAnonymous]
        [Route("gallery/{groupUrlParam}/{urlParam}")]
        public ActionResult Details(string groupUrlParam, string urlParam)
        {
           
            SiteGallery siteGallery = db.SiteGalleries.FirstOrDefault(c=>c.UrlParam==urlParam);

            if (siteGallery == null)
            {
                return HttpNotFound();
            }

            if (siteGallery.SiteGalleryGroup.UrlParam != groupUrlParam)
                return RedirectPermanent("/gallery/" + siteGallery.SiteGalleryGroup.UrlParam + "/" +
                                         siteGallery.UrlParam);

            if (!string.IsNullOrEmpty(siteGallery.FileUrl))
                return Redirect(siteGallery.FileUrl);

            GalleryDetailViewModel gallery=new GalleryDetailViewModel()
            {
                Gallery = siteGallery,
                MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups(),
                MenuItem = baseViewModelHelper.GetMenuItems(),
            };
            return View(gallery);
        }

    }
}

