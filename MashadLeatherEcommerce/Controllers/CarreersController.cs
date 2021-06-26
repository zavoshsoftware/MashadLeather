using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Helper;
using Models;
using ViewModels;

namespace MashadLeatherEcommerce.Controllers
{
    public class CarreersController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Carreers
        public ActionResult Index()
        {
            return View(db.Carreers.Where(a=>a.IsDeleted==false).OrderByDescending(a=>a.CreationDate).ToList());
        }

        // GET: Carreers/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carreer carreer = db.Carreers.Find(id);
            if (carreer == null)
            {
                return HttpNotFound();
            }
            return View(carreer);
        }

        // GET: Carreers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Carreers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Carreer carreer, HttpPostedFileBase fileupload)
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

                    newFilenameUrl = "/Uploads/Carreer/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    fileupload.SaveAs(physicalFilename);
                    carreer.ResumeFile = newFilenameUrl;
                }


                #endregion
                carreer.IsDeleted=false;
				carreer.CreationDate= DateTime.Now; 
					
                carreer.Id = Guid.NewGuid();
                db.Carreers.Add(carreer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carreer);
        }

        // GET: Carreers/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carreer carreer = db.Carreers.Find(id);
            if (carreer == null)
            {
                return HttpNotFound();
            }
            return View(carreer);
        }

        // POST: Carreers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Carreer carreer, HttpPostedFileBase fileupload)
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

                    newFilenameUrl = "/Uploads/Carreer/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    fileupload.SaveAs(physicalFilename);
                    carreer.ResumeFile = newFilenameUrl;
                }


                #endregion
                carreer.IsDeleted=false;
					carreer.LastModifiedDate=DateTime.Now;
                db.Entry(carreer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carreer);
        }

        // GET: Carreers/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carreer carreer = db.Carreers.Find(id);
            if (carreer == null)
            {
                return HttpNotFound();
            }
            return View(carreer);
        }

        // POST: Carreers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Carreer carreer = db.Carreers.Find(id);
			carreer.IsDeleted=true;
			carreer.DeletionDate=DateTime.Now;
 
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("carreer")]
        public ActionResult CreateByUser()
        {
            Helper.BaseViewModelHelper baseViewModelHelper = new BaseViewModelHelper();

            CarreerViewModel carreerViewModel = new CarreerViewModel
            {

                MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups(),
                MenuItem = baseViewModelHelper.GetMenuItems(),
                //Carreer = new Carreer()
                


            };
            return View(carreerViewModel);
        }

        [Route("carreer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateByUser(CarreerViewModel carreer, HttpPostedFileBase fileupload)
        {
            Carreer modelCarreer = new Carreer();
            Helper.BaseViewModelHelper baseViewModelHelper = new BaseViewModelHelper();


            carreer.MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups();
            carreer.MenuItem = baseViewModelHelper.GetMenuItems();
 
   
            if (ModelState.IsValid)
            {

                #region Upload and resize image if needed
                string newFilenameUrl = string.Empty;
                if (fileupload != null)
                {
                    string filename = Path.GetFileName(fileupload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/Carreer/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    fileupload.SaveAs(physicalFilename);
                    carreer.ResumeFile = newFilenameUrl;
                }


                #endregion

                modelCarreer.FullName = carreer.FullName;
                modelCarreer.Email = carreer.Email;
                modelCarreer.Id = Guid.NewGuid();
                modelCarreer.ResumeFile = carreer.ResumeFile;
                modelCarreer.CreationDate = DateTime.Now;
                modelCarreer.CellNumber = carreer.CellNumber;
                modelCarreer.IsDeleted = false;
                db.Carreers.Add(modelCarreer);
                db.SaveChanges();
                //return RedirectToAction("CreateByUser");

                TempData["successMessage"] = "درخواست شما با موفقیت ثبت گردید.";
            }
            return View(carreer);
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
