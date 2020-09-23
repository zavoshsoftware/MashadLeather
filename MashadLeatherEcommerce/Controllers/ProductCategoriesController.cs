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
using ViewModels;
using Helper;

namespace MashadLeatherEcommerce.Controllers
{
    [Authorize(Roles = "Administrator,SuperAdministrator,eshopadmin")]
    public class ProductCategoriesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index(Guid? id)
        {
            List<ProductCategory> ProductCategories = new List<ProductCategory>();
            if (id == null)
            {
                ProductCategories = db.ProductCategories.Where(a => a.IsDeleted == false && a.ParentId == null).OrderBy(a => a.Priority)
                    .ToList();
                ViewBag.Title = "مدیریت گروه محصولات";
                ViewBag.hidden = "html-hidden";
            }
            else
            {
                ProductCategories = db.ProductCategories.Where(a => a.IsDeleted == false && a.ParentId == id)
                    .OrderBy(a => a.Priority)
                    .ToList();

                ViewBag.template = @"Html.ActionLink('مدیریت زیر گروه ها', 'Index', new { id = @item.Id }, new { @class = 'k-button' })";
                ViewBag.Title = $"مدیریت زیر گروه محصولات {db.ProductCategories.Find(id)?.Title}";
                ViewBag.parentId = id;
                ViewBag.classItem = "html-hidden";


            }
            return View(ProductCategories);
        }

        // GET: ProductCategories/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory ProductCategory = db.ProductCategories.Find(id);
            if (ProductCategory == null)
            {
                return HttpNotFound();
            }
            return View(ProductCategory);
        }

        // GET: ProductCategories/Create
        public ActionResult Create(Guid? id)
        {
            ViewBag.Title = id == null ? "افزودن گروه محصول" : $"افزودن زیر گروه به گروه {db.ProductCategories.Find(id)?.Title}";
            return View();
        }

        // POST: ProductCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCategory ProductCategory, Guid? id, HttpPostedFileBase fileupload, HttpPostedFileBase fileupload2)
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

                    newFilenameUrl = "/Uploads/ProductCategory/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    fileupload.SaveAs(physicalFilename);
                    ProductCategory.ImageUrl = newFilenameUrl;
                }


                #endregion
                #region Upload and resize Slideimage if needed
                string newFilenameUrl2 = string.Empty;
                if (fileupload2 != null)
                {
                    string filename2 = Path.GetFileName(fileupload2.FileName);
                    string newFilename2 = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename2);

                    newFilenameUrl2 = "/Uploads/ProductCategory/" + newFilename2;
                    string physicalFilename2 = Server.MapPath(newFilenameUrl2);
                    fileupload2.SaveAs(physicalFilename2);
                    ProductCategory.SlideImageUrl = newFilenameUrl2;
                }


                #endregion
                ProductCategory.ParentId = id;
                ProductCategory.IsDeleted = false;
                ProductCategory.CreationDate = DateTime.Now;
                ProductCategory.Id = Guid.NewGuid();
                db.ProductCategories.Add(ProductCategory);
                db.SaveChanges();

                if (id == null)
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Index", new { id = id });

            }

            return View(ProductCategory);
        }

        // GET: ProductCategories/Edit/5
        public ActionResult Edit(Guid? id, Guid? parentId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductCategory ProductCategory = db.ProductCategories.Find(id);
            if (ProductCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.parentId = parentId;
            return View(ProductCategory);
        }

        // POST: ProductCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategory ProductCategory, HttpPostedFileBase fileupload, HttpPostedFileBase fileupload2)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                string newFilenameUrl = ProductCategory.ImageUrl;
                if (fileupload != null)
                {
                    string filename = Path.GetFileName(fileupload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/ProductCategory/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    fileupload.SaveAs(physicalFilename);
                    ProductCategory.ImageUrl = newFilenameUrl;
                }


                #endregion
                #region Upload and resize Slideimage if needed
                string newFilenameUrl2 = ProductCategory.SlideImageUrl;
                if (fileupload2 != null)
                {
                    string filename2 = Path.GetFileName(fileupload2.FileName);
                    string newFilename2 = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename2);

                    newFilenameUrl2 = "/Uploads/ProductCategory/" + newFilename2;
                    string physicalFilename2 = Server.MapPath(newFilenameUrl2);
                    fileupload2.SaveAs(physicalFilename2);
                    ProductCategory.SlideImageUrl = newFilenameUrl2;
                }


                #endregion
                ProductCategory.IsDeleted = false;
                ProductCategory.LastModifiedDate = DateTime.Now;

                db.Entry(ProductCategory).State = EntityState.Modified;
                db.SaveChanges();
                if (ProductCategory.ParentId == null)
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Index", new { id = ProductCategory.ParentId });

            }
            ViewBag.parentId = ProductCategory.ParentId;
            return View(ProductCategory);
        }

        // GET: ProductCategories/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory ProductCategory = db.ProductCategories.Find(id);
            if (ProductCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.parentId = ProductCategory.ParentId;
            return View(ProductCategory);
        }

        // POST: ProductCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ProductCategory ProductCategory = db.ProductCategories.Find(id);
            ProductCategory.IsDeleted = true;
            ProductCategory.DeletionDate = DateTime.Now;

            if (db.ProductCategories.Any(current => current.ParentId == id && current.IsDeleted == false))
            {
                List<ProductCategory> oProductCategories = db.ProductCategories
                    .Where(current => current.ParentId == id && current.IsDeleted == false).ToList();
                foreach (ProductCategory item in oProductCategories)
                {
                    item.IsDeleted = true;
                    item.DeletionDate = DateTime.Now;
                }
            }
            db.SaveChanges();
            if (ProductCategory.ParentId == null)
                return RedirectToAction("Index");
            else
                return RedirectToAction("Index", new { id = ProductCategory.ParentId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



        [AllowAnonymous]
        [Route("productCategory/list/{parentId:Guid?}")]
        public ActionResult List(Guid? parentId)
        {
            if(parentId==null)
                return RedirectPermanent("/category" );

            ProductCategory pc = db.ProductCategories.Find(parentId);

            if (pc != null)
                return RedirectPermanent("/category/" + pc.UrlParam);

            return RedirectPermanent("/");
        }

        [AllowAnonymous]
        [Route("category")]
        public ActionResult ParentList()
        {
            Helper.BaseViewModelHelper baseViewModelHelper = new BaseViewModelHelper();

            ProductCategoryViewModel productCategoryList = new ProductCategoryViewModel
            {
                MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups(),
                MenuItem = baseViewModelHelper.GetMenuItems(),
                //  ParentProductCategory = GetParentProductCategory(parentId),
                ProductCategories = GetParentProductGroup(),
                //  IsParent = GetIsParent(parentId),
            };

            return View(productCategoryList);
        }

        public List<ProductCategoryListItem> GetParentProductGroup()
        {
            List<ProductCategoryListItem> productCategories = new List<ProductCategoryListItem>();
            ViewBag.Title = Resources.Label.MashadLeatherStore;
            ViewBag.PageTitle = Resources.Label.MashadLeatherStore;
            List<ProductCategory> productCategoryList = db.ProductCategories.Where(current =>
                current.IsDeleted == false && current.IsActive == true && current.ParentId == null).OrderBy(current => current.Priority).ToList();

            foreach (ProductCategory productCategory in productCategoryList)
            {
                productCategories.Add(new ProductCategoryListItem()
                {
                    ProductCategory = productCategory,
                    IsParent = GetIsParent(productCategory.Id)
                });
            }
            ViewBag.SlideImage = "/images/mashad-faq-img.jpg";

            return productCategories;
        }


        [AllowAnonymous]
        [Route("category/{urlParam}")]
        public ActionResult List(string urlParam)
        {
            Helper.BaseViewModelHelper baseViewModelHelper = new BaseViewModelHelper();

            ProductCategory oProductCategory = db.ProductCategories.FirstOrDefault(c => c.UrlParam == urlParam);


            if (!db.ProductCategories.Any(c => c.ParentId == oProductCategory.Id))
                return RedirectPermanent("/product/" + urlParam);
            
            ProductCategoryViewModel productCategoryList = new ProductCategoryViewModel
            {
                MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups(),
                MenuItem = baseViewModelHelper.GetMenuItems(),
                //  ParentProductCategory = GetParentProductCategory(parentId),
                ProductCategories = GetProductCategory(oProductCategory),
                //  IsParent = GetIsParent(parentId),
                BreadcrumpItems = GetProductCategoryBreadcrump(oProductCategory)
            };

            return View(productCategoryList);
        }

        public List<BreadcrumpItemViewModel> GetProductCategoryBreadcrump(ProductCategory currentProductCategory)
        {
            List<BreadcrumpItemViewModel> list = new List<BreadcrumpItemViewModel>();

            list.Add(new BreadcrumpItemViewModel()
            {
                Order = 10,
                Title = currentProductCategory.TitleSrt,
                UrlParam = currentProductCategory.UrlParam,
            });

            if (currentProductCategory.ParentId != null)
            {
                list.Add(new BreadcrumpItemViewModel()
                {
                    Order = 9,
                    Title = currentProductCategory.Parent.TitleSrt,
                UrlParam = currentProductCategory.Parent.UrlParam,
                });

                if (currentProductCategory.Parent.ParentId != null)
                {
                    list.Add(new BreadcrumpItemViewModel()
                    {
                        Order = 8,
                        Title = currentProductCategory.Parent.Parent.TitleSrt,
                UrlParam = currentProductCategory.Parent.Parent.UrlParam,
                    });
                }
                }
            return list.OrderBy(c=>c.Order).ToList();
        }
      
        public List<ProductCategoryListItem> GetProductCategory(ProductCategory oProductCategory)
        {

            if (oProductCategory == null)
                return null;
             

            List<ProductCategoryListItem> productCategories = new List<ProductCategoryListItem>();
            ViewBag.FontSize = "50px;";

            ViewBag.Canonical = "https://www.mashadleather.com/category/"+ oProductCategory.UrlParam.ToLower();
            ViewBag.Description = "خرید اینترنتی محصولات " + oProductCategory.Title +
                                  " از فروشگاه اینترنتی چرم مشهد. بهره مندی از تخفیفات و امتیازات باشگاه مشتریان چرم مشهد با خرید آنلاین محصولات چرم مشهد";

            ViewBag.Title = oProductCategory?.Title + " - " + Resources.Label.MashadLeatherStore;

            ViewBag.PageTitle = Resources.Label.Products + " " + oProductCategory?.TitleSrt + " " + Resources.Label.MashadLeather;

            List<ProductCategory> productCategoryList = db.ProductCategories.Where(current =>
                current.IsDeleted == false && current.IsActive == true && current.ParentId == oProductCategory.Id).OrderBy(current => current.Priority).ToList();

            foreach (ProductCategory productCategory in productCategoryList)
            {
                productCategories.Add(new ProductCategoryListItem()
                {
                    ProductCategory = productCategory,
                    IsParent = GetIsParent(productCategory.Id)
                });
            }
            if (oProductCategory.ParentId != null)
            {
                if (oProductCategory.Parent.SlideImageUrl != null)
                    ViewBag.SlideImage = oProductCategory.Parent.SlideImageUrl;

                else
                    ViewBag.SlideImage = "/images/mashad-faq-img.jpg";

                if (oProductCategory.Parent.Size != null)
                    ViewBag.FontSize = oProductCategory.Parent.Size.ToString() + "px;";
            }
            else
            {
                if (oProductCategory.SlideImageUrl != null)
                    ViewBag.SlideImage = oProductCategory.SlideImageUrl;

                else
                    ViewBag.SlideImage = "/images/mashad-faq-img.jpg";

                if (oProductCategory.Size != null)
                    ViewBag.FontSize = oProductCategory.Size.ToString() + "px;";
            }

            return productCategories;
        }

        public bool GetIsParent(Guid? productCategoryId)
        {
            if (productCategoryId == null)
                return true;

            else
            {
                if (db.ProductCategories.Any(current => current.ParentId == productCategoryId
                        && current.IsDeleted == false
                        && current.IsActive == true))
                    return true;

                else
                    return false;


            }
        }
        //[AllowAnonymous]
        //public ProductCategory GetParentProductCategory(Guid? id)
        //{
        //    ProductCategory productCategory = db.ProductCategories.Find(id);

        //    return productCategory;
        //}
        //[AllowAnonymous]
        //public List<ProductCategory> GetProductCategoryList(Guid? id)
        //{
        //    if (id != null)
        //        return db.ProductCategories.Where(current => current.ParentId == id && current.IsDeleted == false && current.IsActive == true).ToList();
        //    else
        //        return db.ProductCategories.Where(current =>current.ParentId==null&& current.IsDeleted == false && current.IsActive == true).ToList();

        //}
    }
}
