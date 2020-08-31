using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;
using System.IO;

namespace MashadLeatherEcommerce.Controllers
{
    [Authorize(Roles = "Administrator,SuperAdministrator")]
    public class ProductImagesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: ProductImages
        public ActionResult Index(Guid id)
        {
            var productImages = db.ProductImages.Include(p => p.Product).Where(p=>p.IsDeleted==false&&p.ProductId==id).OrderByDescending(p=>p.CreationDate);
            ViewBag.productId = db.Products.Find(id).ParentId;
            return View(productImages.ToList());
        }

        // GET: ProductImages/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImage productImage = db.ProductImages.Find(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            return View(productImage);
        }

        // GET: ProductImages/Create
        public ActionResult Create(Guid id)
        {
            //ViewBag.ProductId = new SelectList(db.Products, "Id", "Title");
            ViewBag.id = id;
            return View();
        }

        // POST: ProductImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductImage productImage,Guid id,HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                string newFilenameUrl = string.Empty;
                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/Product/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    
                }
                #endregion
                productImage.ImageUrl = newFilenameUrl;
                productImage.IsDeleted=false;
				productImage.CreationDate= DateTime.Now;
                productImage.ProductId = id;
                productImage.Id = Guid.NewGuid();
                db.ProductImages.Add(productImage);
                db.SaveChanges();
                return RedirectToAction("Index",new { id=id});
            }

            //ViewBag.ProductId = new SelectList(db.Products, "Id", "Title", productImage.ProductId);
            return View(productImage);
        }

        // GET: ProductImages/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImage productImage = db.ProductImages.Find(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            //ViewBag.ProductId = new SelectList(db.Products, "Id", "Title", productImage.ProductId);
            ViewBag.id = productImage.ProductId;
            return View(productImage);
        }

        // POST: ProductImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductImage productImage, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                string newFilenameUrl = productImage.ImageUrl;
                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/Product/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);


                }
                #endregion
                productImage.ImageUrl = newFilenameUrl;
                productImage.IsDeleted=false;
					productImage.LastModifiedDate=DateTime.Now;
                db.Entry(productImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index",new { id=productImage.ProductId});
            }
            //ViewBag.ProductId = new SelectList(db.Products, "Id", "Title", productImage.ProductId);
            ViewBag.id = productImage.ProductId;
            return View(productImage);
        }

        // GET: ProductImages/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImage productImage = db.ProductImages.Find(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = productImage.ProductId;
            return View(productImage);
        }

        // POST: ProductImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ProductImage productImage = db.ProductImages.Find(id);
			productImage.IsDeleted=true;
			productImage.DeletionDate=DateTime.Now;
 
            db.SaveChanges();
            ViewBag.id = productImage.Id;
            return RedirectToAction("Index",new { id=productImage.ProductId});
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
