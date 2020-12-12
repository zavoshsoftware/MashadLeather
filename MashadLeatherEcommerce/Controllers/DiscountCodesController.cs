using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;
using ViewModels;

namespace MashadLeatherEcommerce.Controllers
{
    public class DiscountCodesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        [Authorize(Roles = "Administrator,SuperAdministrator,eshopadmin,operator")]

        public ActionResult IndexForOprator()
        {
            var discountCodes = db.DiscountCodes.Include(d => d.User).Where(d => d.IsDeleted == false).OrderByDescending(d => d.CreationDate);
            return View(discountCodes.ToList());
        }
        [Authorize(Roles = "Administrator,SuperAdministrator,eshopadmin,operator")]

        public ActionResult UseInStore(Guid id)
        {
            var discountCode = db.DiscountCodes.FirstOrDefault(d => d.Id == id && d.IsDeleted == false);

            if (discountCode != null)
            {
                var identity = (System.Security.Claims.ClaimsIdentity)User.Identity;
                string uid = identity.FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
                Guid userId = new Guid(uid);
                var user = db.Users.Find(userId);

                discountCode.IsUsed = true;
                discountCode.OperatorUsername = user.CellNum;
                discountCode.LastModifiedDate=DateTime.Now;

                db.SaveChanges();
            }
            return RedirectToAction("IndexForOprator");
        }

        [Authorize(Roles = "Administrator,SuperAdministrator,eshopadmin")]

        public ActionResult Index()
        {
            var discountCodes = db.DiscountCodes.Include(d => d.User).Where(d => d.IsDeleted == false).OrderByDescending(d => d.CreationDate);
            return View(discountCodes.ToList());
        }

        [Authorize(Roles = "Administrator,SuperAdministrator,eshopadmin")]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountCode discountCode = db.DiscountCodes.Find(id);
            if (discountCode == null)
            {
                return HttpNotFound();
            }
            return View(discountCode);
        }

        [Authorize(Roles = "Administrator,SuperAdministrator,eshopadmin")]
        public ActionResult Create()
        {
            //    ViewBag.UserId = new SelectList(db.Users, "Id", "Username");
            return View();
        }


        [Authorize(Roles = "Administrator,SuperAdministrator,eshopadmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DiscountCodeCreateViewModel discountCodeVm)
        {
            if (ModelState.IsValid)
            {

                if (db.DiscountCodes.Any(c => c.Code == discountCodeVm.Code && c.IsDeleted == false))
                {
                    ModelState.AddModelError("duplicateCode", "این کد قبلا استفاده شده است.");
                    return View(discountCodeVm);

                }
                DiscountCode discountCode = new DiscountCode();

                if (!discountCodeVm.IsPublic)
                {
                    var user = db.Users.Where(c => c.CellNum == discountCodeVm.UserCellNumber && c.IsDeleted == false)
                        .Select(c => new { c.Id }).FirstOrDefault();

                    if (user == null)
                    {
                        ModelState.AddModelError("invalidUser", "کاربری با این شماره موبایل وجود ندارد.");
                        return View(discountCodeVm);
                    }
                    discountCode.UserId = user.Id;
                }

                discountCode.IsDeleted = false;
                discountCode.CreationDate = DateTime.Now;
                discountCode.Id = Guid.NewGuid();
                discountCode.IsPublic = discountCodeVm.IsPublic;
                discountCode.ExpireDate = discountCodeVm.ExpireDate;
                discountCode.MaxAmount = discountCodeVm.MaxAmount;
                discountCode.IsActive = discountCodeVm.IsActive;
                discountCode.IsMultiUsing = discountCodeVm.IsMultiUsing;
                discountCode.Amount = discountCodeVm.Amount;
                discountCode.Code = discountCodeVm.Code;
                discountCode.IsUsed = discountCodeVm.IsUsed;
                discountCode.Description = discountCodeVm.Description;

                db.DiscountCodes.Add(discountCode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(discountCodeVm);
        }

        [Authorize(Roles = "Administrator,SuperAdministrator,eshopadmin")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountCode discountCode = db.DiscountCodes.Find(id);
            if (discountCode == null)
            {
                return HttpNotFound();
            }

            string userCellNumber = "";
            if (discountCode.UserId != null)
                userCellNumber = discountCode.User.CellNum;

            DiscountCodeCreateViewModel discountCodeVm = new DiscountCodeCreateViewModel()
            {
                Id = discountCode.Id,
                Code = discountCode.Code,
                IsPublic = discountCode.IsPublic,
                IsActive = discountCode.IsActive,
                UserCellNumber = userCellNumber,
                MaxAmount = discountCode.MaxAmount,
                Amount = discountCode.Amount,
                ExpireDate = discountCode.ExpireDate,
                IsMultiUsing = discountCode.IsMultiUsing,
                IsUsed = discountCode.IsUsed,
                Description = discountCode.Description
            };
            return View(discountCodeVm);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,SuperAdministrator,eshopadmin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DiscountCodeCreateViewModel discountCodeVm)
        {
            if (ModelState.IsValid)
            {
                DiscountCode discountCode = db.DiscountCodes.Find(discountCodeVm.Id);


                if (db.DiscountCodes.Any(c => c.Code == discountCodeVm.Code && c.IsDeleted == false && c.Id != discountCodeVm.Id))
                {
                    ModelState.AddModelError("duplicateCode", "این کد قبلا استفاده شده است.");
                    return View(discountCodeVm);

                }

                if (!discountCodeVm.IsPublic)
                {
                    var user = db.Users.Where(c => c.CellNum == discountCodeVm.UserCellNumber && c.IsDeleted == false)
                        .Select(c => new { c.Id }).FirstOrDefault();

                    if (user == null)
                    {
                        ModelState.AddModelError("invalidUser", "کاربری با این شماره موبایل وجود ندارد.");
                        return View(discountCodeVm);
                    }
                    discountCode.UserId = user.Id;
                }


                discountCode.IsDeleted = false;
                discountCode.LastModifiedDate = DateTime.Now;
                discountCode.IsPublic = discountCodeVm.IsPublic;
                discountCode.ExpireDate = discountCodeVm.ExpireDate;
                discountCode.MaxAmount = discountCodeVm.MaxAmount;
                discountCode.IsActive = discountCodeVm.IsActive;
                discountCode.IsMultiUsing = discountCodeVm.IsMultiUsing;
                discountCode.Amount = discountCodeVm.Amount;
                discountCode.Code = discountCodeVm.Code;
                discountCode.IsUsed = discountCodeVm.IsUsed;
                discountCode.Description = discountCodeVm.Description;

                db.Entry(discountCode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(discountCodeVm);
        }

        [Authorize(Roles = "Administrator,SuperAdministrator,eshopadmin")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountCode discountCode = db.DiscountCodes.Find(id);
            if (discountCode == null)
            {
                return HttpNotFound();
            }
            return View(discountCode);
        }

        // POST: DiscountCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator,SuperAdministrator,eshopadmin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            DiscountCode discountCode = db.DiscountCodes.Find(id);
            discountCode.IsDeleted = true;
            discountCode.DeletionDate = DateTime.Now;

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
