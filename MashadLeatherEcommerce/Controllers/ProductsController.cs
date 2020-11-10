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
using Helper;
using MashadLeatherEcommerce.KiyanService;
using ViewModels;
using System.Text.RegularExpressions;

namespace MashadLeatherEcommerce.Controllers
{
    public class ProductsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        GetCurrency oGetCurrency = new GetCurrency();

        // GET: Products
        [Authorize(Roles = "Administrator,SuperAdministrator,eshopadmin")]
        public ActionResult Index(Guid? id)
        {
            List<Product> products = new List<Product>();
            if (!id.HasValue)
            {
                UpdateProductsCode();

                products = db.Products.Include(p => p.ProductCategory).Where(p => p.IsDeleted == false).OrderBy(p => p.Priority).ToList();
            }
            else
            {
                products = db.Products.Include(p => p.ProductCategory).Where(p => p.IsDeleted == false && p.ParentId == id).OrderBy(p => p.Priority).ToList();
                ViewBag.Id = id;
                ViewBag.ParentId = id.Value;
            }

            return View(products.ToList());
        }

        //internal class ListViewModel<T>
        //{
        //    public Guid Id { get; set; }
        //    public ICollection<T> Collection { get; set; }
        //}

        // GET: Products/Details/5
        [Route("product-detail/{id:Guid?}")]
        public ActionResult Details(Guid? id)
        {
            Helper.BaseViewModelHelper baseViewModelHelper = new BaseViewModelHelper();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }


            return RedirectPermanent("/product-detail/" + product.Code);
        }


        [Route("product-detail/{code:int}")]
        public ActionResult Details(int code)
        {
            Helper.BaseViewModelHelper baseViewModelHelper = new BaseViewModelHelper();


            Product product = db.Products.FirstOrDefault(current => current.Code == code);
            if (product == null)
            {
                return HttpNotFound();
            }
            ProductQuickViewModel quickProduct = new ProductQuickViewModel();

            quickProduct.Id = product.Id;
            quickProduct.Title = product.Title;
            quickProduct.Sizes = GetProductSize(product);
            quickProduct.Colors = GetProductColor(product.Id);
            quickProduct.Price = string.Format("{0:#,#}", product.AmountSrt);
            quickProduct.ProductImages = GetProductImages(product.Id);
            quickProduct.Description = product.Description;
            if (product.ProductCategoryId != null)
                quickProduct.ProductCategoryTitle = product.ProductCategory.Title;
            quickProduct.MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups();
            quickProduct.MenuItem = baseViewModelHelper.GetMenuItems();
            quickProduct.FacebookShareLink =
                "https://www.facebook.com/sharer/sharer.php?u=http%3A//mashadleather.com/product/" + product.Id;
            quickProduct.TwitterShareLink =
                "https://twitter.com/home?status=http%3A//mashadleather.com/product/" + product.Id;
            quickProduct.GooglePlusShareLink =
                "https://plus.google.com/share?url=https%3A//twitter.com/home?status=http%253A//mashadleather.com/product/" +
                product.Id;
            quickProduct.TelegramShareLink =
                "https://t.me/share/url?url=http%3A//mashadleather.com/product/" + product.Id;
            quickProduct.SecondColor = ReturnSecondColor(product);
            //ProductDetailViewModel productDetail = new ProductDetailViewModel()
            //{
            //    MenuItem = baseViewModelHelper.GetMenuItems(),
            //    Product = product,
            //    ProductImages = db.ProductImages.Where(current => current.IsDeleted == false && current.ProductId == id)
            //        .OrderBy(current => current.Priority).ToList()
            //};
            quickProduct.SecondColor = ReturnSecondColor(product);
            quickProduct.IsInPromotion = product.IsInPromotion;
            quickProduct.DiscountAmount = string.Format("{0:#,#}", product.DiscountAmountSrt);
            quickProduct.IsAvailable = product.IsAvailable;

            quickProduct.CurrentCurrency = oGetCurrency.CurrentCurrency();
            ViewBag.Title = product.Title + " | چرم مشهد";
            ViewBag.Canonical = "https://www.mashadleather.com/product-detail/" + product.Code;
            ViewBag.Description = "بررسی ابعاد، رنگ و مشخصات " + product.Title + " با امکان خرید اینترنتی از وب‌سایت رسمی چرم مشهد.";

            quickProduct.BreadcrumpItems = GetProductBreadcrump(product);
            return View(quickProduct);
        }

        // GET: Products/Create
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public ActionResult Create()
        {
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories.Where(current => current.IsDeleted == false), "Id", "Title");
            ViewBag.SizeId = new SelectList(db.Sizes.Where(current => current.IsDeleted == false), "Id", "Title");
            ViewBag.ColorId = new SelectList(db.Colors.Where(current => current.IsDeleted == false), "Id", "Title");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public ActionResult Create(Product product, HttpPostedFileBase fileupload)
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

                    newFilenameUrl = "/Uploads/Product/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    product.ImageUrl = newFilenameUrl;
                }
                #endregion
                product.IsDeleted = false;
                product.CreationDate = DateTime.Now;

                product.Id = Guid.NewGuid();
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "Id", "Title", product.ProductCategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Administrator,SuperAdministrator,eshopadmin")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            if (product.ProductCategoryId != null)
            {
                //   ViewBag.ParentProductCategoryId = new SelectList(db.ProductCategories.Where(current => current.IsDeleted == false && current.ParentId == null), "Id", "Title", product.ProductCategory.ParentId);
                ViewBag.ProductCategoryId = new SelectList(db.ProductCategories.Where(current => current.IsDeleted == false), "Id", "Title", product.ProductCategoryId);

            }
            else
            {
                ViewBag.ProductCategoryId = new SelectList(db.ProductCategories.Where(current => current.IsDeleted == false), "Id", "Title");
            }

            ViewBag.SizeId = new SelectList(db.Sizes.Where(current => current.IsDeleted == false), "Id", "Title", product.SizeId);
            ViewBag.ColorId = new SelectList(db.Colors.Where(current => current.IsDeleted == false), "Id", "Title", product.ColorId);
            ViewBag.Id = product.ParentId;
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,SuperAdministrator,eshopadmin")]
        public ActionResult Edit(Product product, HttpPostedFileBase fileupload)
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

                    newFilenameUrl = "/Uploads/Product/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    product.ImageUrl = newFilenameUrl;
                }
                #endregion

                product.IsDeleted = false;
                product.LastModifiedDate = DateTime.Now;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = product.ParentId });
            }
            //ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "Id", "Title", product.ProductCategoryId);


            ViewBag.ParentProductCategoryId = new SelectList(db.ProductCategories.Where(current => current.IsDeleted == false && current.ParentId == null), "Id", "Title", product.ProductCategory.ParentId);
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories.Where(current => current.IsDeleted == false && current.ParentId == product.ProductCategory.ParentId), "Id", "Title", product.ProductCategoryId);

            return View(product);
        }

        //[AllowAnonymous]
        //public ActionResult FillProductCategories(string id)
        //{
        //    Guid parentId = new Guid(id);
        //    //ViewBag.cityId = ReturnProductCategories(parentId);
        //    return Json(ReturnProductCategories(parentId));
        //}
        //[AllowAnonymous]
        //public SelectList ReturnProductCategories(Guid? id)
        //{

        //    SelectList productCategories;
        //    if (id == null)
        //    {
        //        productCategories = new SelectList(db.ProductCategories.OrderBy(current => current.Priority).Where(current => current.IsActive == true && current.IsDeleted == false), "Id", "Title");

        //    }
        //    else
        //    {
        //        productCategories = new SelectList(db.ProductCategories.Where(current => current.ParentId == id && current.IsDeleted == false && current.IsActive == true).OrderBy(current => current.Priority), "Id", "Title");
        //    }
        //    return productCategories;
        //}


        // GET: Products/Delete/5
        [Authorize(Roles = "Administrator,SuperAdministrator,eshopadmin")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,SuperAdministrator,eshopadmin")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Product product = db.Products.Find(id);
            product.IsDeleted = true;
            product.DeletionDate = DateTime.Now;

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

        [HttpPost]
        public ActionResult Migration()
        {
            try
            {
                TransferDataToDatabase();
                CheckConflict();
                CheckConflictDeletedParent();
                SetSecondColor();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return RedirectToAction("Index");
        }


        public Guid? GetSizeByBarCode(string itmBrcd)
        {
          
            string title = itmBrcd.Substring(10, 2);

            string barCodeProductGroup = itmBrcd.Substring(5, 1);

            Size size = db.Sizes.FirstOrDefault(current => current.Title == title && current.BarCodeProductGroup == barCodeProductGroup);

            return size?.Id;
        }
        public Guid? GetColorByBarCode(string itmBrcd)
        {
            //G0422D0241011504Z014
            string barCodeColor = itmBrcd.Substring(17, 3);

            Color color = db.Colors.FirstOrDefault(current => current.BarCodeId == barCodeColor);

            return color?.Id;
        }

        public decimal? CalculateAmount(decimal price)
        {
            decimal vat = Convert.ToDecimal(0.09);
            decimal? amount = price + price * vat;

            decimal? remain = amount % 1000;

            if (remain < 500)
                amount = amount - remain;

            else
                amount = amount + (1000 - remain);

            return amount;
        }
        public void TransferDataToDatabase()
        {
            KiyanHelper kiyan = new KiyanHelper();

            KyanOnlineSaleServiceSoapClient ks = new KyanOnlineSaleServiceSoapClient();

            //var dsds = ks.GetInventoriesList(header);
            //var pro = ks.GetPromotions(header, 616);

            ks.Endpoint.Binding.SendTimeout = new TimeSpan(0, 4, 0);
            ks.Endpoint.Binding.ReceiveTimeout = new TimeSpan(0, 4, 0);


            ValidationSoapHeader header = kiyan.ConnectToService();


            //تمدن
            List<KiyanProductItem> productList616 = GetProductFromInventory(616, ks, header);

            //فردوسی
            List<KiyanProductItem> productList988 = GetProductFromInventory(988, ks, header);

            ////کریمخان
            // List<KiyanProductItem> productList698 = GetProductFromInventory(698, ks, header);

            // //ولیعصر
            // List<KiyanProductItem> productList290 = GetProductFromInventory(290, ks, header);
            ChangeChangeStatus();

            TransferProducts(productList616, true);
            TransferProducts(productList988, false);
            db.SaveChanges();

            DeleteNotChangedProducts();
        }

        public void ResolveDuplicateProduct()
        {

            List<Product> products = db.Products
                .Where(current => current.IsDeleted == false && current.ParentId == null && current.IsInPromotion).ToList();

            foreach (Product product in products)
            {
                List<Product> childProducts = db.Products
                    .Where(current => current.ParentId == product.Id && current.IsDeleted == false && current.IsInPromotion).ToList();

                foreach (Product childProduct in childProducts)
                {
                    List<Product> duplicated = childProducts.Where(current =>
                        current.Barcode == childProduct.Barcode && current.Id != childProduct.Id && current.DiscountAmount != childProduct.DiscountAmount).ToList();

                    if (duplicated.Any())
                    {
                        string barcode = childProduct.Barcode;
                    }
                }

            }
        }

        public void TransferProducts(List<KiyanProductItem> products, bool isFirstInventory)
        {
            foreach (var item in products)
            {
                decimal? amount = CalculateAmount(item.itmPrice);
                if (item.itmBrcd.Length == 20)
                {
                    List<Product> currentProducts = db.Products
                         .Where(current => current.IsDeleted == false && current.Barcode == item.itmBrcd).ToList();

                    //Product exaclly is in database with same color and same size
                    if (currentProducts.Count > 0)
                    {
                        foreach (Product currentProduct in currentProducts)
                        {
                            currentProduct.Taxable = item.Taxable;
                            currentProduct.Barcode = item.itmBrcd;
                            currentProduct.KiyanCreateDate = item.itmCreateDate;
                            currentProduct.KiyanName = item.itmName;
                            currentProduct.Amount = amount;
                            if (isFirstInventory)
                                currentProduct.Quantity = item.itmQuantity;
                            else
                                currentProduct.Quantity = currentProduct.Quantity + item.itmQuantity;
                            currentProduct.DiscountAmount = item.itmTempPrice;
                            currentProduct.KiyanId = item.itmID;
                            currentProduct.Title = ReturnTitle(item.itmBrcd.Substring(5, 5));
                            currentProduct.LastModifiedDate = DateTime.Now;
                            currentProduct.IsChanged = true;
                            currentProduct.IsAvailable = true;

                            if (currentProduct.ParentId != null)
                            {
                                currentProduct.Parent.IsChanged = true;
                                currentProduct.Parent.Amount = amount;
                                currentProduct.Parent.IsAvailable = true;

                            }

                            //اگر قبلا رنگ تعریف نشده بود و کد رنک نال ثبت شده باشد
                            if (currentProduct.ColorId == null)
                            {
                                Guid? colorId = GetColorByBarCode(item.itmBrcd);
                                if (colorId != null)
                                    currentProduct.ColorId = colorId;
                            }

                            //شرایطی پیش آمد که پدر در جدول موجود بود اما رکوردی برای بچه با همان سایز و رنگ موجود نبود
                            else if (currentProduct.ParentId == null)
                            {
                                Guid? colorId = GetColorByBarCode(currentProduct.Barcode);
                                string code = currentProduct.Barcode.Substring(5, 5);
                                Guid? sizeId = null;
                                if (IsProductSizable(code))
                                {
                                    sizeId = GetSizeByBarCode(currentProduct.Barcode);
                                }

                                if (!currentProducts.Any(current =>
                                    current.ColorId == colorId && current.SizeId == sizeId))
                                {
                                    Product product = new Product();

                                    if (sizeId != null)
                                        product.SizeId = sizeId;

                                    if (colorId != null)
                                        product.ColorId = colorId;

                                    product.Id = Guid.NewGuid();
                                    product.ParentId = currentProduct.Id;
                                    product.IsDeleted = false;
                                    product.Priority = 100;
                                    product.IsActive = false;
                                    product.CreationDate = DateTime.Now;
                                    product.LastModifiedDate = DateTime.Now;
                                    product.Taxable = item.Taxable;
                                    product.Barcode = item.itmBrcd;
                                    product.KiyanCreateDate = item.itmCreateDate;
                                    product.KiyanName = item.itmName;
                                    product.Amount = amount;
                                    if (isFirstInventory)
                                        currentProduct.Quantity = item.itmQuantity;
                                    else
                                        currentProduct.Quantity = currentProduct.Quantity + item.itmQuantity;
                                    product.DiscountAmount = item.itmTempPrice;
                                    product.KiyanId = item.itmID;
                                    product.Title = ReturnTitle(code);
                                    product.IsChanged = true;
                                    product.IsAvailable = true;
                                    db.Products.Add(product);
                                }
                            }
                        }
                    }

                    else
                    {
                        string code = item.itmBrcd.Substring(5, 5);

                        Product currentProduct = db.Products
                            .FirstOrDefault(current =>
                                current.IsDeleted == false &&
                                current.Barcode.Substring(5, 5) == code &&
                                current.ParentId == null);

                        //parent product exist in database
                        if (currentProduct != null)
                        {
                            Guid? colorId = GetColorByBarCode(item.itmBrcd);

                            if (IsProductSizable(code))
                            {
                                Product product = new Product();

                                product.Id = Guid.NewGuid();

                                Guid? sizeId = GetSizeByBarCode(item.itmBrcd);

                                if (sizeId != null)
                                    product.SizeId = sizeId;

                                if (colorId != null)
                                    product.ColorId = colorId;

                                product.ParentId = currentProduct.Id;
                                product.IsDeleted = false;
                                product.Priority = 100;
                                product.IsActive = false;
                                product.CreationDate = DateTime.Now;
                                product.LastModifiedDate = DateTime.Now;
                                product.Taxable = item.Taxable;
                                product.Barcode = item.itmBrcd;
                                product.KiyanCreateDate = item.itmCreateDate;
                                product.KiyanName = item.itmName;
                                product.Amount = amount;
                                product.Quantity = item.itmQuantity;
                                product.DiscountAmount = item.itmTempPrice;
                                product.KiyanId = item.itmID;
                                product.Title = ReturnTitle(code);
                                product.IsChanged = true;
                                product.IsAvailable = true;
                                //if (product.ParentId != null)
                                //    product.Parent.Amount = amount;

                                if (currentProduct.ParentId != null)
                                    currentProduct.Parent.IsChanged = true;

                                db.Products.Add(product);
                            }

                            //product not sizable
                            else
                            {
                                Product product = new Product();

                                product.Id = Guid.NewGuid();
                                if (colorId != null)
                                {
                                    product.ColorId = colorId;
                                }

                                product.ParentId = currentProduct.Id;
                                product.IsDeleted = false;
                                product.Priority = 100;
                                product.IsActive = false;
                                product.CreationDate = DateTime.Now;
                                product.LastModifiedDate = DateTime.Now;
                                product.Taxable = item.Taxable;
                                product.Barcode = item.itmBrcd;
                                product.KiyanCreateDate = item.itmCreateDate;
                                product.KiyanName = item.itmName;
                                product.Amount = amount;
                                product.Quantity = item.itmQuantity;
                                product.DiscountAmount = item.itmTempPrice;
                                product.KiyanId = item.itmID;
                                product.Title = ReturnTitle(code);
                                product.IsChanged = true;
                                product.IsAvailable = true;

                                if (currentProduct.ParentId != null)
                                    currentProduct.Parent.IsChanged = true;


                                //if (product.ParentId != null)
                                //    product.Parent.Amount = amount;


                                db.Products.Add(product);
                            }
                        }

                        //parent is not in database
                        else
                        {

                            List<Product> deletedProducts = db.Products.Where(current =>
                                current.Barcode == item.itmBrcd && current.IsDeleted == true).ToList();

                            //is on deleted product
                            if (deletedProducts.Count() > 0)
                            {
                                foreach (Product deleteProduct in deletedProducts)
                                {
                                    deleteProduct.IsDeleted = false;
                                    deleteProduct.IsActive = false;
                                    deleteProduct.DeletionDate = null;
                                    deleteProduct.IsChanged = true;
                                    deleteProduct.LastModifiedDate = DateTime.Now;
                                    deleteProduct.IsAvailable = true;

                                    //if (deleteProduct.ParentId != null)
                                    //{
                                    //    deleteProduct.Parent.IsChanged = true;
                                    //    deleteProduct.Parent.IsDeleted = false;
                                    //    deleteProduct.Parent.IsActive = false;
                                    //    deleteProduct.Parent.DeletionDate = null;
                                    //    deleteProduct.Parent.LastModifiedDate = DateTime.Now;
                                    //}
                                }
                            }
                            else
                            {

                                //panet product not exist in database

                                //Loocking for parent product on Deleted products
                                Product parent = IsParentProductOnDeleted(code);

                                if (parent == null)
                                {

                                    Product newParent = new Product()
                                    {
                                        Id = Guid.NewGuid(),
                                        IsDeleted = false,
                                        Priority = 100,
                                        IsActive = false,
                                        CreationDate = DateTime.Now,
                                        LastModifiedDate = DateTime.Now,
                                        Taxable = item.Taxable,
                                        Barcode = item.itmBrcd,
                                        KiyanCreateDate = item.itmCreateDate,
                                        KiyanName = item.itmName,
                                        Amount = amount,
                                        Quantity = item.itmQuantity,
                                        DiscountAmount = item.itmTempPrice,
                                        KiyanId = item.itmID,
                                        Title = ReturnTitle(code),
                                        IsChanged = true,
                                        IsAvailable = true
                                    };
                                    db.Products.Add(newParent);
                                    parent = newParent;
                                    //     db.SaveChanges();
                                }

                                Guid? colorId = GetColorByBarCode(item.itmBrcd);

                                Product product = new Product();

                                if (IsProductSizable(code))
                                {
                                    Guid? sizeId = GetSizeByBarCode(item.itmBrcd);

                                    if (sizeId != null)
                                        product.SizeId = sizeId;
                                }

                                if (colorId != null)
                                    product.ColorId = colorId;

                                product.Id = Guid.NewGuid();
                                product.ParentId = parent.Id;
                                product.IsDeleted = false;
                                product.Priority = 100;
                                product.IsActive = false;
                                product.CreationDate = DateTime.Now;
                                product.LastModifiedDate = DateTime.Now;
                                product.Taxable = item.Taxable;
                                product.Barcode = item.itmBrcd;
                                product.KiyanCreateDate = item.itmCreateDate;
                                product.KiyanName = item.itmName;
                                product.Amount = amount;
                                product.Quantity = item.itmQuantity;
                                product.DiscountAmount = item.itmTempPrice;
                                product.KiyanId = item.itmID;
                                product.Title = ReturnTitle(code);
                                product.IsChanged = true;
                                product.IsAvailable = true;
                                db.Products.Add(product);
                                //     db.SaveChanges();
                            }
                        }
                    }
                }

                else if (item.itmBrcd.Length == 13)
                {
                    Product currentProduct = db.Products
                        .Where(current => current.IsDeleted == false && current.Barcode == item.itmBrcd)
                        .FirstOrDefault();

                    if (currentProduct != null)
                    {
                        currentProduct.Taxable = item.Taxable;
                        currentProduct.Barcode = item.itmBrcd;
                        currentProduct.KiyanCreateDate = item.itmCreateDate;
                        currentProduct.KiyanName = item.itmName;
                        currentProduct.Amount = amount;
                        if (isFirstInventory)
                            currentProduct.Quantity = item.itmQuantity;
                        else
                            currentProduct.Quantity = currentProduct.Quantity + item.itmQuantity;
                        currentProduct.DiscountAmount = item.itmTempPrice;
                        currentProduct.KiyanId = item.itmID;
                        currentProduct.LastModifiedDate = DateTime.Now;
                        currentProduct.IsChanged = true;
                        currentProduct.ParentId = null;
                        currentProduct.ColorId = null;
                        currentProduct.SizeId = null;
                        currentProduct.IsAvailable = true;
                    }
                    else
                    {
                        Product deleteProduct = db.Products.FirstOrDefault(current => current.Barcode == item.itmBrcd && current.IsDeleted == true);

                        if (deleteProduct != null)
                        {
                            deleteProduct.IsDeleted = false;
                            deleteProduct.IsActive = false;
                            deleteProduct.DeletionDate = null;
                            deleteProduct.IsChanged = true;
                            deleteProduct.IsAvailable = true;
                        }

                        else
                        {
                            Product oProduct = new Product()
                            {
                                Id = Guid.NewGuid(),
                                IsDeleted = false,
                                Priority = 100,
                                IsActive = false,
                                CreationDate = DateTime.Now,
                                LastModifiedDate = DateTime.Now,
                                Taxable = item.Taxable,
                                Barcode = item.itmBrcd,
                                KiyanCreateDate = item.itmCreateDate,
                                KiyanName = item.itmName,
                                Amount = amount,
                                Quantity = item.itmQuantity,
                                DiscountAmount = item.itmTempPrice,
                                KiyanId = item.itmID,
                                Title = item.itmBrcd,
                                IsChanged = true,
                                ColorId = null,
                                SizeId = null,
                                IsAvailable = true
                            };
                            db.Products.Add(oProduct);
                        }
                    }
                }
            }
        }

        public List<KiyanProductItem> GetProductFromInventory(int inventoryId, KyanOnlineSaleServiceSoapClient ks, ValidationSoapHeader header)
        {
            List<KiyanProductItem> productList = new List<KiyanProductItem>();
            header.Token = "Charm@#$568";
            var list = ks.GetItemListWithinventoryID(header, inventoryId);


            InsertIntoKiyanLog(list.ResponseResult.Length, inventoryId);

            foreach (var item in list.ResponseResult)
            {
                productList.Add(new KiyanProductItem()
                {
                    itmBrcd = item.itmBrcd,
                    itmID = item.itmID,
                    itmQuantity = item.itmQuantity,
                    itmPrice = item.itmPrice,
                    itmName = item.itmName,
                    Taxable = item.Taxable,
                    itmCreateDate = item.itmCreateDate,
                    itmTempPrice = item.itmTempPrice,
                });
            }

            return productList;
        }
        public Product IsParentProductOnDeleted(string code)
        {
            Product product = db.Products.FirstOrDefault(current =>
                current.IsDeleted == true && current.ParentId == null && current.Barcode.Substring(5, 5) == code);

            if (product != null)
            {
                product.IsDeleted = true;
                product.DeletionDate = null;
                product.IsChanged = true;
                product.LastModifiedDate = DateTime.Now;
                product.IsActive = false;
            }
            return product;
        }

        public void DeleteNotChangedProducts()
        {
            List<Product> products = db.Products.Where(current =>
                current.IsDeleted == false && current.IsChanged == false).ToList();

            foreach (Product product in products)
            {
                product.IsActive = false;
                product.IsDeleted = true;
                product.DeletionDate = DateTime.Now;
                product.IsAvailable = false;
            }

            if (products.Any())
                db.SaveChanges();
        }

        public void ChangeChangeStatus()
        {
            List<Product> products = db.Products.Where(current => current.IsDeleted == false).ToList();

            foreach (Product product in products)
            {
                product.IsChanged = false;

                //disablePromotion
                product.IsInPromotion = false;
                product.DiscountAmount = 0;
            }

            db.SaveChanges();
        }
        public void InsertIntoKiyanLog(int length, int? inventoryId)
        {
            KiyanLog kiyanlog = new KiyanLog()
            {
                LogDate = DateTime.Now,
                Count = length,
                Id = Guid.NewGuid(),
                InventoryId = inventoryId

            };

            db.KiyanLogs.Add(kiyanlog);
            db.SaveChanges();
        }
        public string ReturnTitle(string code)
        {
            string codeChar = code.Substring(0, 1);
            string title = string.Empty;
            switch (codeChar)
            {
                case "A":
                    {
                        title = "کیف اداری " + code;
                        break;
                    }
                case "B":
                    {
                        title = "مانتو " + code;
                        break;
                    }
                case "C":
                    {
                        title = "کت " + code;
                        break;
                    }
                case "D":
                    {
                        title = "کیف پول " + code;
                        break;
                    }
                case "G":
                    {
                        title = "جاکلیدی و اکسسوری " + code;
                        break;
                    }
                case "H":
                    {
                        title = "کلاه " + code;
                        break;
                    }
                case "I":
                    {
                        title = "جاموبایلی " + code;
                        break;
                    }
                case "J":
                    {
                        title = "کفش " + code;
                        break;
                    }
                case "K":
                    {
                        title = "کاپشن " + code;
                        break;
                    }
                case "L":
                    {
                        title = "کیف آرایشی " + code;
                        break;
                    }
                case "M":
                    {
                        title = "کت بارانی " + code;
                        break;
                    }
                case "R":
                    {
                        title = "دستکش " + code;
                        break;
                    }
                case "S":
                    {
                        title = "کیف دوشی " + code;
                        break;
                    }
                case "X":
                    {
                        title = "کیف حمایل " + code;
                        break;
                    }
                case "Z":
                    {
                        title = "دستبند " + code;
                        break;
                    }
                case "P":
                    {
                        title = "کیف پاسپورتی " + code;
                        break;
                    }
                case "N":
                    {
                        title = "کمربند " + code;
                        break;
                    }
                case "U":
                    {
                        title = "ست " + code;
                        break;
                    }
            }
            return title;
        }
        [AllowAnonymous]
        [Route("product/list/{productGroupId:Guid}")]
        public ActionResult List(Guid productGroupId)
        {
            Helper.BaseViewModelHelper baseViewModelHelper = new BaseViewModelHelper();


            ProductCategory productCategory =
                db.ProductCategories.Find(productGroupId);


            if (productCategory == null)
                return RedirectPermanent("/category");

            return RedirectPermanent("/product/" + productCategory.UrlParam);


        }

        [AllowAnonymous]
        [Route("product/{urlParam}")]
        public ActionResult List(string urlParam)
        {
            Helper.BaseViewModelHelper baseViewModelHelper = new BaseViewModelHelper();

            ProductCategory productCategory =
                db.ProductCategories.FirstOrDefault(current => current.UrlParam == urlParam);


            if (productCategory == null)
                return RedirectPermanent("/category");

            List<Product> products = db.Products
                .Where(current => current.ImageUrl != null && current.IsDeleted == false &&
                                  current.ParentId == null && current.ProductCategoryId == productCategory.Id).ToList();

            ViewBag.total = products.Count();

            ProductListViewModel productList = new ProductListViewModel
            {
                MenuItem = baseViewModelHelper.GetMenuItems(),
                Products = GetProductList(products).OrderByDescending(c => c.IsAvailable).ToList(),
                MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups(),
                ProductCategory = GetProductCategory(productCategory),
                Commnets = db.Comments.Where(c => c.ProductCategoryId == productCategory.Id && c.IsActive && c.IsDeleted == false && c.ParentId == null).ToList()
                ,
                BreadcrumpItems = GetProductCategoryBreadcrump(productCategory),
                CurrentCurrency = oGetCurrency.CurrentCurrency()
            };

            return View(productList);
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
            return list.OrderBy(c => c.Order).ToList();
        }
        public List<BreadcrumpItemViewModel> GetProductBreadcrump(Product currentProduct)
        {
            List<BreadcrumpItemViewModel> list = new List<BreadcrumpItemViewModel>();


            list.Add(new BreadcrumpItemViewModel()
            {
                Order = 10,
                Title = currentProduct.TitleSrt,
                UrlParam = currentProduct.Code.ToString(),
            });

            if (currentProduct.ProductCategoryId != null)
            {
                list.Add(new BreadcrumpItemViewModel()
                {
                    Order = 9,
                    Title = currentProduct.ProductCategory.TitleSrt,
                    UrlParam = currentProduct.ProductCategory.UrlParam,
                });

                if (currentProduct.ProductCategory.ParentId != null)
                {
                    list.Add(new BreadcrumpItemViewModel()
                    {
                        Order = 8,
                        Title = currentProduct.ProductCategory.Parent.TitleSrt,
                        UrlParam = currentProduct.ProductCategory.Parent.UrlParam,
                    });
                    if (currentProduct.ProductCategory.Parent.ParentId != null)
                    {
                        list.Add(new BreadcrumpItemViewModel()
                        {
                            Order = 7,
                            Title = currentProduct.ProductCategory.Parent.Parent.TitleSrt,
                            UrlParam = currentProduct.ProductCategory.Parent.Parent.UrlParam,
                        });
                    }
                }
            }
            return list.OrderBy(c => c.Order).ToList();
        }

        public List<ProductListItem> GetProductList(List<Product> products)
        {
            List<ProductListItem> productItems = new List<ProductListItem>();

            foreach (Product product in products)
            {
                productItems.Add(new ProductListItem()
                {
                    Id = product.Id,
                    ImageUrl = product.ImageUrl,
                    Amount = string.Format("{0:#,#}", product.AmountSrt),
                    Title = product.TitleSrt,
                    ProductCategoryTitle = product.ProductCategory.TitleSrt,
                    LikeClass = ReturnUserLike(product.Id),
                    DiscountAmount = string.Format("{0:#,#}", product.DiscountAmountSrt),
                    IsInPromotion = product.IsInPromotion,
                    HasTag = product.HasTag,
                    TagTitle = product.TagTitleSrt,
                    IsAvailable = product.IsAvailable
                });
            }

            return productItems;
        }
        public string ReturnUserLike(Guid id)
        {
            string likeClass = string.Empty;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                string userIdString = HttpContext.User.Identity.Name;
                if (!string.IsNullOrEmpty(userIdString))
                {
                    User user = db.Users.FirstOrDefault(current => current.CellNum == userIdString);

                    if (user != null)
                    {
                        Guid userId = user.Id;
                        UserProductLike userProductLike = db.UserProductLike.Where(current =>
                                current.UserId == userId && current.ProductId == id && current.IsDeleted == false)
                            .FirstOrDefault();
                        if (userProductLike != null)
                            likeClass = "likeList";
                    }
                }
            }
            return likeClass;
        }
        [AllowAnonymous]
        public ProductCategory GetProductCategory(ProductCategory productCategory)
        {
            ViewBag.FontSize = "50px;";

            ViewBag.Title = productCategory.TitleSrt + " - " + Resources.Label.MashadLeather;
            ViewBag.Canonical = "https://www.mashadleather.com/product/" + productCategory.UrlParam.ToLower();
            ViewBag.Description = "خرید اینترنتی محصولات " + productCategory.Title +
                                  " از فروشگاه اینترنتی چرم مشهد. بهره مندی از تخفیفات و امتیازات باشگاه مشتریان چرم مشهد با خرید آنلاین محصولات چرم مشهد";

            if (productCategory.ParentId != null)
            {
                if (productCategory.Parent.ParentId != null)
                {
                    if (productCategory.Parent.Parent.SlideImageUrl != null)
                        ViewBag.SlideImage = productCategory.Parent.Parent.SlideImageUrl;
                    else
                        ViewBag.SlideImage = "/images/mashad-faq-img.jpg";
                    if (productCategory.Parent.Parent.Size != null)
                        ViewBag.FontSize = productCategory.Parent.Parent.Size.ToString() + "px;";
                }
                else
                {
                    if (productCategory.Parent.SlideImageUrl != null)
                        ViewBag.SlideImage = productCategory.Parent.SlideImageUrl;
                    else
                        ViewBag.SlideImage = "/images/mashad-faq-img.jpg";
                    if (productCategory.Parent.Size != null)
                        ViewBag.FontSize = productCategory.Parent.Size.ToString() + "px;";
                }

            }
            else
            {
                if (productCategory.SlideImageUrl != null)
                    ViewBag.SlideImage = productCategory.SlideImageUrl;
                else
                    ViewBag.SlideImage = "/images/mashad-faq-img.jpg";
                if (productCategory.Size != null)
                    ViewBag.FontSize = productCategory.Size.ToString() + "px;";
            }

            return productCategory;
        }

        [AllowAnonymous]
        [Route("productQuiockView/{id:Guid}")]
        public ActionResult ProductQuiockView(Guid id)
        {
            Product product = db.Products.Find(id);

            ProductQuickViewModel quickProduct = new ProductQuickViewModel
            {
                Id = product.Id,
                Title = product.TitleSrt,
                Sizes = GetProductSize(product),
                Colors = GetProductColor(id),
                Price = string.Format("{0:#,#}", product.AmountSrt),
                ProductImages = GetProductImages(id),
                Description = product.DescriptionSrt,
                FacebookShareLink =
                    "https://www.facebook.com/sharer/sharer.php?u=http%3A//mashadleather.com/product/" + id,
                TwitterShareLink = "https://twitter.com/home?status=http%3A//mashadleather.com/product/" + id,
                GooglePlusShareLink = "https://plus.google.com/share?url=https%3A//twitter.com/home?status=http%253A//mashadleather.com/product/" + id,
                TelegramShareLink = "https://t.me/share/url?url=http%3A//mashadleather.com/product/" + id,
                SecondColor = ReturnSecondColor(product),
                IsInPromotion = product.IsInPromotion,
                DiscountAmount = string.Format("{0:#,#}", product.DiscountAmountSrt),
                IsAvailable = product.IsAvailable,
                CurrentCurrency = oGetCurrency.CurrentCurrency()
            };


            return View(quickProduct);
        }
        [AllowAnonymous]
        public List<ProductImage> GetProductImages(Guid productId)
        {
            List<ProductImage> productImages = db.ProductImages.Where(current => current.ProductId == productId && current.IsActive == true && current.IsDeleted == false).OrderBy(current => current.Priority).ToList();

            return productImages;
        }
        [AllowAnonymous]
        public List<Size> GetProductSize(Product product)
        {
            string code = product.Barcode.Substring(5, 5);


            List<Size> sizes = new List<Size>();
            if (IsProductSizable(code))
            {
                List<Product> products = db.Products.Where(current => current.ParentId == product.Id && current.IsDeleted == false).OrderBy(current => current.Size.Title).ToList();

                foreach (Product productItem in products)
                {
                    Size size = db.Sizes.Find(productItem.SizeId);

                    if (!sizes.Contains(size))
                        sizes.Add(size);
                }

                return sizes;
            }
            else
                return sizes;
        }


        [AllowAnonymous]
        public List<ProductColor> GetProductColor(Guid productId)
        {
            List<ProductColor> colors = new List<ProductColor>();

            List<Product> products = db.Products.Where(current => current.ParentId == productId && current.IsDeleted == false).ToList();

            foreach (Product product in products)
            {
                Color color = db.Colors.Find(product.ColorId);

                if (!colors.Any(current => current.Id == color.Id))
                    colors.Add(new ProductColor()
                    {
                        Id = color.Id,
                        TitleSrt = color.TitleSrt,
                        DecreaseAmount = string.Format("{0:#,#}", product.DecreaseAmount),
                        HexCode = color.HexCode
                    });
            }

            return colors;
        }
        [AllowAnonymous]
        public ActionResult AddToBasket(string qty, string proId)
        {
            return Json("true", JsonRequestBehavior.AllowGet);
        }


        [AllowAnonymous]
        public bool IsProductSizable(string productCode)
        {
            string productGroup = productCode.Substring(0, 1).ToLower();

            if (productGroup == "c" || productGroup == "b" || productGroup == "k" || productGroup == "j" ||
                productGroup == "m" || productGroup == "r")
                return true;

            return false;
        }
        [AllowAnonymous]
        [Route("purchaseGuide")]
        public ActionResult PurchaseGuide()
        {
            Guid id = new Guid("32f431cb-756b-42b5-b0d2-374fd2f0f9ab");
            Helper.BaseViewModelHelper baseViewModelHelper = new BaseViewModelHelper();
            TextViewModel textViewModel = new TextViewModel()
            {
                MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups(),
                MenuItem = baseViewModelHelper.GetMenuItems(),
                Text = db.Texts.Where(current => current.IsDeleted == false && current.Id == id).FirstOrDefault()
            };
            return View(textViewModel);

        }

        [AllowAnonymous]
        public ActionResult SaveUserProductLike(string id)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    Guid productId = new Guid(id);
                    Guid userId = new Guid(HttpContext.User.Identity.Name);
                    UserProductLike userProductLike = db.UserProductLike.Where(current => current.UserId == userId && current.ProductId == productId).FirstOrDefault();
                    if (userProductLike != null)
                    {
                        if (userProductLike.IsDeleted == false)
                        {
                            userProductLike.IsDeleted = true;
                            db.SaveChanges();
                            return Json("false", JsonRequestBehavior.AllowGet);
                        }

                        else if (userProductLike.IsDeleted == true)
                        {
                            userProductLike.IsDeleted = false;
                            db.SaveChanges();
                            return Json("true", JsonRequestBehavior.AllowGet);
                        }
                        else
                            return Json("false", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        UserProductLike oUserProductLike = new UserProductLike()
                        {
                            CreationDate = DateTime.Now,
                            IsActive = true,
                            IsDeleted = false,
                            ProductId = productId,
                            UserId = userId,
                        };
                        db.UserProductLike.Add(oUserProductLike);
                        db.SaveChanges();
                        return Json("true", JsonRequestBehavior.AllowGet);
                    }

                }
                else
                    return Json("notAuthenticated", JsonRequestBehavior.AllowGet);
            }
            else
                return Json("false", JsonRequestBehavior.AllowGet);


        }
        [AllowAnonymous]
        public ActionResult SubmitComment(string name, string email, string body, string productGroupName)
        {
            bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

            if (!isEmail)
                return Json("InvalidEmail", JsonRequestBehavior.AllowGet);
            else
            {

                ProductCategory productCategory =
                    db.ProductCategories.FirstOrDefault(c => c.UrlParam == productGroupName);

                if (productCategory != null)
                {
                    Comment comment = new Comment();

                    comment.Name = name;
                    comment.Email = email;
                    comment.CommentBody = body;
                    comment.CreationDate = DateTime.Now;
                    comment.IsDeleted = false;
                    comment.Id = Guid.NewGuid();
                    comment.ProductCategoryId = productCategory.Id;
                    comment.IsActive = false;

                    db.Comments.Add(comment);
                    db.SaveChanges();
                    return Json("true", JsonRequestBehavior.AllowGet);
                }

                return Json("false", JsonRequestBehavior.AllowGet);

            }
        }

        [AllowAnonymous]
        public string ReturnSecondColor(Product product)
        {
            if (product.SecondColorId != null)
            {
                SecondColor secondColor = db.SecondColors.FirstOrDefault(current => current.Id == product.SecondColorId);

                return Resources.Label.SecondColor + " : " + secondColor?.TitleSrt;
            }
            else
                return String.Empty;

        }
        public void CheckConflict()
        {
            string ReturnValue = "";

            List<Product> products = db.Products.Where(current =>
                current.ParentId == null && current.ImageUrl != null && current.IsDeleted == false && current.Barcode.Length == 20).ToList();

            foreach (Product product in products)
            {
                string code = product.Barcode.Substring(5, 5);

                List<Product> childProducts = db.Products
                    .Where(current =>
                        current.IsDeleted == false &&
                        current.Barcode.Substring(5, 5) == code &&
                        current.ParentId == null && current.ImageUrl == null).ToList();

                foreach (Product childProduct in childProducts)
                {
                    ReturnValue = ReturnValue + "|" + product.Id.ToString();

                    childProduct.ParentId = product.Id;

                    ChangeChildParents(childProduct.Id, product.Id);

                    if (IsProductSizable(code))
                    {
                        Guid? sizeId = GetSizeByBarCode(childProduct.Barcode);

                        if (sizeId != null)
                            childProduct.SizeId = sizeId;
                    }
                    Guid? colorId = GetColorByBarCode(childProduct.Barcode);

                    if (colorId != null)
                        childProduct.ColorId = colorId;

                    childProduct.LastModifiedDate = DateTime.Now;
                }
            }

            db.SaveChanges();

            //  return ReturnValue;
        }

        public void CheckConflictDeletedParent()
        {
            List<Product> products = db.Products.Where(current =>
                current.ParentId == null && current.IsDeleted == true).ToList();

            foreach (Product product in products)
            {
                string code = product.Barcode.Substring(5, 5);

                List<Product> childProducts = db.Products
                    .Where(current =>
                        current.IsDeleted == false &&
                        current.Barcode.Substring(5, 5) == code &&
                        current.ParentId == product.Id).ToList();

                if (childProducts.Count() > 0)
                {
                    product.IsDeleted = false;
                    product.DeletionDate = null;
                    product.LastModifiedDate = DateTime.Now;
                }
            }

            db.SaveChanges();

            //  return ReturnValue;
        }

        public void ChangeChildParents(Guid parentId, Guid newParentId)
        {
            List<Product> childProducts = db.Products
                .Where(current => current.ParentId == parentId && current.IsDeleted == false).ToList();

            foreach (Product childProduct in childProducts)
            {
                childProduct.ParentId = newParentId;
            }
        }

        [AllowAnonymous]
        public ActionResult CheckSizeAndColor(string productId, string colorId, string sizeId)
        {
            //return Json("true", JsonRequestBehavior.AllowGet);
            if (sizeId == null)
                return Json("true", JsonRequestBehavior.AllowGet);
            else
            {
                Guid parentId = new Guid(productId);

                Guid colorIdGuid = new Guid(colorId);

                Guid sizeIdGuid = new Guid(sizeId);

                Product product = db.Products.FirstOrDefault(current => current.ParentId == parentId && current.IsDeleted == false && current.ColorId == colorIdGuid &&
                    current.SizeId == sizeIdGuid);

                if (product != null)
                    return Json("true", JsonRequestBehavior.AllowGet);
                else
                    return Json("false", JsonRequestBehavior.AllowGet);
            }
        }
        [AllowAnonymous]
        public void SetSecondColor()
        {
            List<Product> products = db.Products.Where(current => current.IsDeleted == false && current.Barcode.Length == 20).ToList();
            foreach (Product product in products)
            {
                if (product.Barcode.Length == 20)
                {
                    string code = product.Barcode.Substring(13, 1).ToUpper();
                    if (HasSecondColor(code))
                    {
                        SecondColor secondColor =
                            db.SecondColors.FirstOrDefault(
                                current => current.IsDeleted == false && current.Code == code);

                        if (secondColor != null)
                        {
                            product.SecondColorId = secondColor.Id;
                            product.LastModifiedDate = DateTime.Now;
                        }
                    }
                }


            }
            db.SaveChanges();

        }

        public bool HasSecondColor(string code)
        {
            if (code == "A" || code == "B" || code == "C" || code == "D" || code == "E" || code == "F" || code == "G" || code == "H" || code == "I" || code == "J" || code == "K" || code == "L" || code == "M")
                return true;
            else
                return false;
        }


        [HttpPost]
        public ActionResult Promotion15()
        {
            try
            {
                AddPromotion(15);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Promotion20()
        {
            try
            {
                AddPromotion(20);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Promotion10()
        {
            try
            {
                AddPromotion(10);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return RedirectToAction("Index");
        }

        public void AddPromotion(int percent)
        {
            List<Product> products = db.Products.Where(current => current.IsDeleted == false).ToList();

            foreach (Product product in products)
            {
                product.IsInPromotion = true;
                product.DiscountAmount = product.Amount - product.Amount * percent / 100;
                product.DecreaseAmount = percent;
            }

            db.SaveChanges();
        }

        public ActionResult ConfigurePromotion()
        {
            return View();
        }


        public ActionResult KiyanPromotion(string containCharachter)
        {
            try
            {
                AddKiyanPromotion(containCharachter);
                return Json("true", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        public void AddKiyanPromotion(string containCharachter)
        {

            KiyanHelper kiyan = new KiyanHelper();

            KyanOnlineSaleServiceSoapClient ks = new KyanOnlineSaleServiceSoapClient();

            ValidationSoapHeader header = kiyan.ConnectToService();

            //ورودی مربوط به ای دی فروشگاه مهم نیست چه عددی باشد
            var pro = ks.GetPromotions(header, 616);

            int promotionIndex = 0;

            foreach (var promotionHeaderModel in pro.ResponseResult)
            {
                if (promotionHeaderModel.PromotionName.Contains(containCharachter))
                    break;

                else
                    promotionIndex++;
            }

            foreach (var promotion in pro.ResponseResult[promotionIndex].PromotionLineItem)
            {
                string barcode = promotion.BarCode;

                List<Product> products = db.Products
                    .Where(current => current.Barcode == barcode && current.IsDeleted == false).ToList();


                //Product product = db.Products
                //    .FirstOrDefault(current => current.Barcode == barcode && current.IsDeleted == false);
                foreach (Product product in products)
                {
                    if (product != null)
                    {
                        decimal discountAmount =
                            Convert.ToDecimal(product.Amount - product.Amount * (promotion.DecreaseAmount / 100));
                        product.IsInPromotion = true;
                        product.DiscountAmount = discountAmount;
                        product.DecreaseAmount = promotion.DecreaseAmount;
                    }
                }

            }

            CalculateParentPrices();

            //SetOtherProductDiscount();

            db.SaveChanges();
        }

        public decimal GetMaxDiscountAmount(string productName)
        {
            List<Product> products =
                db.Products.Where(current => current.Barcode.Substring(5, 5) == productName).ToList();

            decimal max = 0;

            foreach (Product product in products)
            {
                if (product.IsInPromotion && product.DiscountAmount > max)
                    max = (decimal)product.DiscountAmount;
            }

            return max;
        }

        public void CalculateParentPrices()
        {
            List<Product> products =
                db.Products.Where(current => current.ParentId == null && current.IsDeleted == false).ToList();

            foreach (Product product in products)
            {
                if (product.Barcode.Length == 20)
                {
                    decimal maxDiscountAmount = GetMaxDiscountAmount(product.Barcode.Substring(5, 5));

                    if (product.DiscountAmount < maxDiscountAmount)
                    {
                        product.DiscountAmount = maxDiscountAmount;
                    }

                    if (maxDiscountAmount != 0)
                        product.IsInPromotion = true;
                }
            }
        }

        [AllowAnonymous]
        public ActionResult ChangePriceByColor(string id, string colorId)
        {
            try
            {
                Guid productId = new Guid(id);
                Guid colorIdGuid = new Guid(colorId);

                Product parentProduct = db.Products.Find(productId);

                Product childProduct = db.Products.FirstOrDefault(current =>
                    current.ParentId == parentProduct.Id && current.ColorId == colorIdGuid);

                if (childProduct != null)
                    return Json(string.Format("{0:#,#}", childProduct.DiscountAmount), JsonRequestBehavior.AllowGet);
                else
                    return Json("error", JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json("errorCatch", JsonRequestBehavior.AllowGet);
            }
        }


        [AllowAnonymous]
        public string GetUsersCellNumber()
        {
            Guid statusId = new Guid("3d3a706e-2deb-4913-bb15-2a31ede340d6");
            List<Order> orders = db.Orders.Where(current => current.OrderStatusId == statusId).ToList();
            string res = "";
            foreach (Order order in orders)
            {
                res = res + order.User.CellNum + "<br/>";
            }

            return res;
        }

        public void SetOtherProductDiscount()
        {
            List<Product> products = db.Products
                .Where(current => current.IsInPromotion != true && current.IsDeleted == false).ToList();

            foreach (Product product in products)
            {
                decimal disVal = ((decimal)product.Amount * (decimal)(0.2));
                decimal discountAmount = Convert.ToDecimal(product.Amount - disVal);
                product.IsInPromotion = true;
                product.DiscountAmount = discountAmount;
                product.DecreaseAmount = 20;
            }
        }


        //public void testInventory()
        //{
        //    KiyanHelper kiyan = new KiyanHelper();

        //    KyanOnlineSaleServiceSoapClient ks = new KyanOnlineSaleServiceSoapClient();
        //    ValidationSoapHeader header = kiyan.ConnectToService();

        //    var dsds = ks.GetInventoriesList(header);
        //    //var pro = ks.GetPromotions(header, 616);

        //    ks.Endpoint.Binding.SendTimeout = new TimeSpan(0, 4, 0);
        //    ks.Endpoint.Binding.ReceiveTimeout = new TimeSpan(0, 4, 0);


        //}


        [AllowAnonymous]
        [Route("product/redirect/collonil")]
        public ActionResult RedirectCollonil()
        {

            return RedirectPermanent("/product/list/db34d7ee-6505-44bc-b6ec-d98957b8c4dd");
        }
        //public void SetProductCode()
        //{
        //    List<Product> products = db.Products.ToList();
        //    int code = 999;
        //    foreach (Product product in products)
        //    {
        //        code = code + 1;
        //        product.Code = code;
        //    }
        //    db.SaveChanges();
        //}


        public void UpdateProductsCode()
        {
            List<Product> products = db.Products.Where(c => c.Code == 0).ToList();

            Product lastProduct = db.Products.OrderByDescending(c => c.Code).FirstOrDefault();

            int code = lastProduct.Code;
            foreach (Product product in products)
            {
                code = code + 1;
                product.Code = code;
            }

            db.SaveChanges();
        }


        public string updateAvailabality()
        {
            List<Product> products = db.Products.Where(c => c.IsDeleted == false).ToList();

            foreach (Product product in products)
            {
                product.IsAvailable = true;
            }

            db.SaveChanges();
            return string.Empty;
        }
    }
}



