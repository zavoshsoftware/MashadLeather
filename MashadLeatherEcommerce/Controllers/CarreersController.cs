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
        public ActionResult Create(Carreer carreer)
        {
            if (ModelState.IsValid)
            {
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
        public ActionResult Edit(Carreer carreer)
        {
            if (ModelState.IsValid)
            {
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
        public ActionResult CreateByUser(CarreerViewModel carreer)
        {
            Carreer modelCarreer = new Carreer();
            Helper.BaseViewModelHelper baseViewModelHelper = new BaseViewModelHelper();


            carreer.MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups();
            carreer.MenuItem = baseViewModelHelper.GetMenuItems();


            if (ModelState.IsValid)
            {

                //#region Upload and resize image if needed
                //string newFilenameUrl = string.Empty;
                //if (fileupload != null)
                //{
                //    string filename = Path.GetFileName(fileupload.FileName);
                //    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                //                         + Path.GetExtension(filename);

                //    newFilenameUrl = "/Uploads/Carreer/" + newFilename;
                //    string physicalFilename = Server.MapPath(newFilenameUrl);
                //    fileupload.SaveAs(physicalFilename);
                //    carreer.ResumeFile = newFilenameUrl;
                //}


                //#endregion

                modelCarreer.FullName = carreer.FullName;
                modelCarreer.Email = carreer.Email;
                modelCarreer.Id = Guid.NewGuid();
                //modelCarreer.ResumeFile = carreer.ResumeFile;
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
      
        
        [HttpPost]
        public ActionResult AjaxPost(CareerPostViewModel input)
        {
            try
            {

            
            Carreer carreer = new Carreer()
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                IsDeleted = false,
                IsActive = true,
                FullName = input.FullName,
                Email = input.Email,
                CellNumber = input.CellNumber,
                NationalCode = input.NationalCode,
                GenderTitle = input.GenderTitle,
                MarriedStatus = input.marriedRadio,
                PeopleInChargeNumber =Convert.ToInt32(input.PeopleInChargeNumber),
                ChidNumber = Convert.ToInt32(input.ChidNumber),
                Nationality = input.Nationality,
                PlaceOfBirth = input.PlaceOfBirth,
                Address=input.Address,
                BirthdayDate = Convert.ToDateTime(input.BirthdayDate),
                MilitaryStatus = input.MilitaryStatus,
                PhysicalCondition = input.PhysicalCondition,
                IsInsurance =Convert.ToBoolean(input.insuranceRadio),
                DurationInsuranceHistory = Convert.ToInt32(input.insuranceYear),
                Education = input.educationRadio,
                Major = input.Major,
                LastUniversity = input.LastUniversity,
                LastCertificateDateTime = Convert.ToDateTime(input.lastCertificateDate),
                Writing = input.writingRadio,
                Reading = input.readingRadio,
                Listening = input.listeningRadio,
                Speaking = input.speakingRadio,
                Software = input.Software,
                Windows = input.Windows,
                OtherSoftware = input.OtherSoftware,
                SportHistory = input.SportHistory,
                Familiar = input.familierWithCompanyRadio,
                IntroduceName = input.IntroduceName,
                IntroducePost = input.IntroducePost,
                ExpectedSalary = input.expectedSalaryRadio,
                InterestedJob=input.InterestedJob,
                RequestedPrice=input.RequestedPrice
            };
            db.Carreers.Add(carreer);
            db.SaveChanges();
            if (!string.IsNullOrEmpty(input.introduceName1))
                InsertCarierIntroduce(carreer.Id, input.introduceName1, input.introduceRelative1,
                    input.introduceJob1, input.introduceWorkPlace1, input.introduceHomePhone1,
                    input.introduceCellNumber1, input.introduceAddress1);


            if (!string.IsNullOrEmpty(input.introduceName2))
                InsertCarierIntroduce(carreer.Id, input.introduceName2, input.introduceRelative2,
                    input.introduceJob2, input.introduceWorkPlace2, input.introduceHomePhone2,
                    input.introduceCellNumber2, input.introduceAddress2);


            if (!string.IsNullOrEmpty(input.introduceName3))
                InsertCarierIntroduce(carreer.Id, input.introduceName3, input.introduceRelative3,
                    input.introduceJob3, input.introduceWorkPlace3, input.introduceHomePhone3,
                    input.introduceCellNumber3, input.introduceAddress3);


            if (!string.IsNullOrEmpty(input.companyName1))
                InsertPrevExperience(carreer.Id, input.companyName1, input.post1,
                    input.workStartDate1, input.workEndDate1, input.recievedPrice1,
                    input.leaveWorkReason1, input.Phone1,input.Address1);


            if (!string.IsNullOrEmpty(input.companyName2))
                InsertPrevExperience(carreer.Id, input.companyName2, input.post2,
                    input.workStartDate2, input.workEndDate2, input.recievedPrice2,
                    input.leaveWorkReason2, input.Phone2,input.Address2);


            if (!string.IsNullOrEmpty(input.companyName3))
                InsertPrevExperience(carreer.Id, input.companyName3, input.post3,
                    input.workStartDate3, input.workEndDate3, input.recievedPrice3,
                    input.leaveWorkReason3, input.Phone3,input.Address3);


            if (!string.IsNullOrEmpty(input.courseName1))
                InsertCourse(carreer.Id, input.courseName1, input.educationalCenterName1,
                    input.skill1, input.durationofCourse1);



            if (!string.IsNullOrEmpty(input.courseName2))
                InsertCourse(carreer.Id, input.courseName2, input.educationalCenterName2,
                    input.skill2, input.durationofCourse2);



            if (!string.IsNullOrEmpty(input.courseName3))
                InsertCourse(carreer.Id, input.courseName3, input.educationalCenterName3,
                    input.skill3, input.durationofCourse3);


            if (!string.IsNullOrEmpty(input.familyName1))
                InsertCarierFamily(carreer.Id, input.familyName1, input.familyRelative1,
                    input.introduceJob1, input.birthdayDate1,input.introduceCellNumber1,
                    input.educationalLevel1);

            db.SaveChanges();


            return Json("true", JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json("false", JsonRequestBehavior.AllowGet);

            }
        }

        public void InsertCarierIntroduce(Guid carrerId,string fullName, string relative, 
            string job, string place, string phone, string cellNumber, string address)
        {
            CarreerIntroduced carreerIntroduced = new CarreerIntroduced()
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                IsDeleted = false,
                IsActive = true,
                CarreerId = carrerId,
                FullName =fullName,
                Relationship = relative,
                Job = job,
                HomePhone = phone,
                CellNumber = cellNumber,
                Address = address,
                WorkPlace = place,
            };

            db.CarreerIntroduceds.Add(carreerIntroduced);
        }
      
        public void InsertPrevExperience(Guid carrerId,string companyName, string post, 
            string startDate, string endDate, string sallary, string reason, string phone, string address)
        {
            CarreerPreviousExperience carreerPreviousExperience = new CarreerPreviousExperience()
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                IsDeleted = false,
                IsActive = true,
                CarreerId = carrerId,
                CompanyName = companyName,
                Post = post,
                StartDatetime =  (startDate),
                EndDatetime = endDate,
                ReceivedMoney = sallary,
                Address = address,
                LeavingWorkReason = reason,
                Phone = phone,
            };

            db.CarreerPreviousExperiences.Add(carreerPreviousExperience);
        }
     
        public void InsertCourse(Guid carrerId,string courseName, string center, 
            string skill, string duration)
        {
            CarreerEducationalCourse carreerEducationalCourse = new CarreerEducationalCourse()
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                IsDeleted = false,
                IsActive = true,
                CarreerId = carrerId,
                CourseName = courseName,
                CourseDuration = duration,
                Skill = skill,
                InstitutionName = center,
               
            };

            db.CarreerEducationalCourses.Add(carreerEducationalCourse);
        }

        public void InsertCarierFamily(Guid carrerId, string fullName, string relative,
            string job, string birthDate, string cellNumber, string education)
        {
            CarreerFamilyInformation carreerFamilyInformation = new CarreerFamilyInformation()
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                IsDeleted = false,
                IsActive = true,
                CarreerId = carrerId,
                FullName = fullName,
                Relationship = relative,
                Job = job,
                CellNumber = cellNumber,
                BirthdayDate = Convert.ToDateTime(birthDate),
                EducationalLevel = education,
            };

            db.CarreerFamilyInformations.Add(carreerFamilyInformation);
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
