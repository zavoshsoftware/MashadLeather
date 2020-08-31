using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Helper;
using Models;
using ViewModels;

namespace MashadLeatherEcommerce.Controllers
{
    public class SiteBranchesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: SiteBranches
        public ActionResult Index()
        {
            var siteBranches = db.SiteBranches.Include(s => s.SiteBranchGroup).Where(s=>s.IsDeleted==false).OrderByDescending(s=>s.CreationDate);
            return View(siteBranches.ToList());
        }

        // GET: SiteBranches/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteBranch siteBranch = db.SiteBranches.Find(id);
            if (siteBranch == null)
            {
                return HttpNotFound();
            }
            return View(siteBranch);
        }

        // GET: SiteBranches/Create
        public ActionResult Create()
        {
            ViewBag.SiteBranchGroupId = new SelectList(db.SiteBranchGroups, "Id", "Title");
            return View();
        }

        // POST: SiteBranches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( SiteBranch siteBranch)
        {
            if (ModelState.IsValid)
            {
				siteBranch.IsDeleted=false;
				siteBranch.CreationDate= DateTime.Now; 
					
                siteBranch.Id = Guid.NewGuid();
                db.SiteBranches.Add(siteBranch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SiteBranchGroupId = new SelectList(db.SiteBranchGroups, "Id", "Title", siteBranch.SiteBranchGroupId);
            return View(siteBranch);
        }

        // GET: SiteBranches/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteBranch siteBranch = db.SiteBranches.Find(id);
            if (siteBranch == null)
            {
                return HttpNotFound();
            }
            ViewBag.SiteBranchGroupId = new SelectList(db.SiteBranchGroups, "Id", "Title", siteBranch.SiteBranchGroupId);
            return View(siteBranch);
        }

        // POST: SiteBranches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SiteBranch siteBranch)
        {
            if (ModelState.IsValid)
            {
				siteBranch.IsDeleted=false;
					siteBranch.LastModifiedDate=DateTime.Now;
                db.Entry(siteBranch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SiteBranchGroupId = new SelectList(db.SiteBranchGroups, "Id", "Title", siteBranch.SiteBranchGroupId);
            return View(siteBranch);
        }

        // GET: SiteBranches/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteBranch siteBranch = db.SiteBranches.Find(id);
            if (siteBranch == null)
            {
                return HttpNotFound();
            }
            return View(siteBranch);
        }

        // POST: SiteBranches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            SiteBranch siteBranch = db.SiteBranches.Find(id);
			siteBranch.IsDeleted=true;
			siteBranch.DeletionDate=DateTime.Now;
 
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

        [Route("branches")]
        public ActionResult List()
        {
            Helper.BaseViewModelHelper baseViewModelHelper = new BaseViewModelHelper();

            List<BranchItemViewModel> branches=new List<BranchItemViewModel>();

            List<SiteBranchGroup> siteBranchGroups = db.SiteBranchGroups.Where(s => s.IsDeleted == false).OrderBy(c=>c.Order).ToList();

            foreach (SiteBranchGroup siteBranchGroup in siteBranchGroups)
            {
                branches.Add(new BranchItemViewModel()
                {
                    BranchGroup = siteBranchGroup,
                    SiteBranches = db.SiteBranches.Where(c =>
                        c.SiteBranchGroupId == siteBranchGroup.Id && c.IsDeleted == false && c.IsActive).OrderBy(c => c.Order).ToList()
                });
            }

            BranchListViewMoel branchList =new BranchListViewMoel()
            {
                MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups(),
                MenuItem = baseViewModelHelper.GetMenuItems(),
                Branches = branches
            };



            return View(branchList);
        }

    }
}
