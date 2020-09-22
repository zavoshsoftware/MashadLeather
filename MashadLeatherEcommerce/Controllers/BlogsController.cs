using System;
using System.Collections.Generic;
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
    [Authorize(Roles = "Administrator,SuperAdministrator")]
    public class BlogsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            return View(db.Blogs.Where(a => a.IsDeleted == false).OrderByDescending(a => a.CreationDate).ToList());
        }

        [AllowAnonymous]
        [Route("blog/{groupUrlParam}/{urlParam}")]
        public ActionResult Details(string urlParam,string groupUrlParam)
        {
            Blog blog = db.Blogs.FirstOrDefault(a => a.UrlParam == urlParam && a.IsDeleted == false);

            BaseViewModelHelper baseViewModelHelper = new BaseViewModelHelper();

            BlogDetailViewModel blogDetail = new BlogDetailViewModel()
            {
                Blog = blog,
                MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups(),
                MenuItem = baseViewModelHelper.GetMenuItems(),
                RelatedBlogs = db.Blogs.Where(current => current.IsDeleted == false).Take(4).ToList()
            };
            ViewBag.Canonical = "https://www.mashadleather.com/blog/" + blog.BlogGroup.UrlParam +"/" +blog.UrlParam;
            return View(blogDetail);
        }

        public ActionResult Create()
        {
            ViewBag.BlogGroupId = new SelectList(db.BlogGroups.Where(c=>c.IsActive), "Id", "Title");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Blog blog, HttpPostedFileBase fileupload)
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

                    newFilenameUrl = "/Uploads/Blog/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    blog.ImageUrl = newFilenameUrl;
                }
                #endregion
                blog.IsDeleted = false;
                blog.CreationDate = DateTime.Now;

                blog.Id = Guid.NewGuid();
                db.Blogs.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BlogGroupId = new SelectList(db.BlogGroups.Where(c => c.IsActive), "Id", "Title");

            return View(blog);
        }

        // GET: Blogs/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }

            ViewBag.BlogGroupId = new SelectList(db.BlogGroups.Where(c => c.IsActive), "Id", "Title",blog.BlogGroupId);

            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Blog blog, HttpPostedFileBase fileupload)
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

                    newFilenameUrl = "/Uploads/Blog/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    blog.ImageUrl = newFilenameUrl;
                }
                #endregion

                blog.IsDeleted = false;
                blog.LastModifiedDate = DateTime.Now;
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BlogGroupId = new SelectList(db.BlogGroups.Where(c => c.IsActive), "Id", "Title",blog.BlogGroupId);
            return View(blog);
        }
         
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }
         
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Blog blog = db.Blogs.Find(id);
            blog.IsDeleted = true;
            blog.DeletionDate = DateTime.Now;

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

        [AllowAnonymous]
        [Route("blog/{urlParam}")]
        public ActionResult List(string urlParam)
        {
            BlogGroup blogGroup = db.BlogGroups.FirstOrDefault(c => c.UrlParam == urlParam);

            if (blogGroup == null)
                return RedirectPermanent("/");

            List<Blog> blogs = db.Blogs.Where(a => a.BlogGroupId == blogGroup.Id && a.IsDeleted == false && a.IsActive)
                .OrderByDescending(a => a.CreationDate).ToList();
            
            BaseViewModelHelper baseViewModelHelper = new BaseViewModelHelper();

            BlogListViewModel blogList = new BlogListViewModel()
            {
                BlogGroup = blogGroup,
                MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups(),
                Blogs = blogs,
                MenuItem = baseViewModelHelper.GetMenuItems()
            };
            return View(blogList);
        }


        [AllowAnonymous]
        [Route("blog")]
        public ActionResult PureList()
        {
            List<Blog> blogs = db.Blogs.Where(a => a.IsDeleted == false && a.IsActive).OrderByDescending(a => a.CreationDate)
                .ToList();

            BaseViewModelHelper baseViewModelHelper = new BaseViewModelHelper();

            BlogListViewModel blogList = new BlogListViewModel()
            {
                MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups(),
                Blogs = blogs,
                MenuItem = baseViewModelHelper.GetMenuItems()
            };
            return View(blogList);
        }

    }
}
