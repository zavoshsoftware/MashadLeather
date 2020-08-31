using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Helper;
using Models;
using ViewModels;

namespace MashadLeatherEcommerce.Controllers
{
    [Authorize(Roles = "Administrator,SuperAdministrator")]
    public class CommentsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Comments
        public ActionResult Index(Guid? id)
        {
            List<Comment> comments = new List<Comment>();
            if (id == null)
            {
                ViewBag.parent = "";
                comments = db.Comments.Include(c => c.Parent).Where(c => c.IsDeleted == false).OrderByDescending(c => c.CreationDate).Include(c => c.ProductCategory).Where(c => c.IsDeleted == false).OrderByDescending(c => c.CreationDate).ToList();
            }
            else
            {
                ViewBag.parent = id;
                comments = db.Comments.Include(c => c.Parent).Where(c => c.IsDeleted == false && c.Id == id).OrderByDescending(c => c.CreationDate).Include(c => c.ProductCategory).Where(c => c.IsDeleted == false).OrderByDescending(c => c.CreationDate).ToList();
            }

            GridviewBind(comments);
            return View(comments.ToList());
        }

        public ActionResult Confirm(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            else

            {
                comment.IsActive = true;
                db.SaveChanges();
                //if (comment.ParentId.ToString() != "")
                //    return RedirectToAction("Index", new { id = comment.ParentId });
                //else
                return RedirectToAction("Index");
            }
        }
        public ActionResult NoConfirm(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            else

            {
                comment.IsActive = false;
                db.SaveChanges();
                //if (comment.ParentId.ToString() != "")
                //    return RedirectToAction("Index");
                //else
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Comment comment = db.Comments.Find(id);
            comment.IsDeleted = true;
            comment.DeletionDate = DateTime.Now;

            db.SaveChanges();
            //if (comment.ParentId.ToString() != "")
            //    return RedirectToAction("Index");
            //else
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




        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Comment comment)
        {
            if (ModelState.IsValid)
            {


                comment.IsDeleted = false;
                comment.LastModifiedDate = DateTime.Now;
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comment);
        }




        public void GridviewBind(List<Comment> comments)
        {
            List<ExcelGridviewCommentsReportViewModel> gridList = new List<ExcelGridviewCommentsReportViewModel>();
            foreach (Comment cm in comments)
            {
                gridList.Add(new ExcelGridviewCommentsReportViewModel
                {
                    FullName = cm.Name,
                    Comment = cm.CommentBody,
                    Email = cm.Email,
                    ProductGroup = cm.ProductCategory.Title,
                    Response = cm.Response,
                    CreationDate = cm.CreationDate,
                });
            }

            GridView gv = new GridView();
            gv.DataSource = gridList;
            gv.DataBind();
            gv.HeaderRow.Cells[0].Text = "نام کاربر";
            gv.HeaderRow.Cells[1].Text = "ایمیل";
            gv.HeaderRow.Cells[2].Text = "صفحه نظر داده شده";
            gv.HeaderRow.Cells[3].Text = "متن نظر";
            gv.HeaderRow.Cells[4].Text = "پاسخ";
            gv.HeaderRow.Cells[5].Text = "تاریخ ثبت";

            Session["comments"] = gv;
        }


        public ActionResult DownloadReport()
        {
            if (Session["comments"] != null)
            {
                return new DownloadFileActionResult((GridView)Session["comments"], "comments.xls");
            }
            else
            {
                return null;
            }
        }
    }
}
