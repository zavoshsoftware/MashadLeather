using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Helper;
using MashadLeatherEcommerce.Services.Sms;
using ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace MashadLeatherEcommerce.Controllers
{
    public class AccountController : Controller
    {
        BaseViewModelHelper baseViewModelHelper = new BaseViewModelHelper();
        private DatabaseContext db = new DatabaseContext();

        [Route("login")]
        public ActionResult Login(string ReturnUrl = "")
        {

            ViewBag.Message = "";
            ViewBag.ReturnUrl = ReturnUrl;

            LoginPageViewModel login = new LoginPageViewModel()
            {
                MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups(),
                MenuItem = baseViewModelHelper.GetMenuItems(),
            };
            return View(login);

        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginPageViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User oUser = db.Users.Include(u => u.Role)
                    .Where(a => a.CellNum == model.Login.Username && a.Password == model.Login.Password)
                    .FirstOrDefault();

                if (oUser != null)
                {
                    var ident = new ClaimsIdentity(
                        new[]
                        {
                            // adding following 2 claim just for supporting default antiforgery provider
                            new Claim(ClaimTypes.NameIdentifier, oUser.CellNum),
                            new Claim(
                                "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                                "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),

                            new Claim(ClaimTypes.Name, oUser.Id.ToString()),

                            // optionally you could add roles if any
                            new Claim(ClaimTypes.Role, oUser.Role.Name),

                        },
                        DefaultAuthenticationTypes.ApplicationCookie);

                    HttpContext.GetOwinContext().Authentication.SignIn(
                        new AuthenticationProperties { IsPersistent = true }, ident);
                    return RedirectToLocal(returnUrl, oUser.Role.Name); // auth succeed 
                }
                else
                {
                    // invalid username or password
                    TempData["WrongPass"] = "نام کاربری و یا کلمه عبور وارد شده صحیح نمی باشد.";
                }
            }

            LoginPageViewModel login = new LoginPageViewModel()
            {
                MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups(),
                MenuItem = baseViewModelHelper.GetMenuItems(),
                Login = model.Login
            };

            return View(login);
        }

        private ActionResult RedirectToLocal(string returnUrl, string role)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Products");
            }
        }

        public ActionResult LogOff()
        {
            if (User.Identity.IsAuthenticated)
            {
                HttpContext.GetOwinContext().Authentication.SignOut();
            }
            return RedirectToAction("index", "Home");
        }


        [Route("RecoveryPassword")]
        [AllowAnonymous]
        public ActionResult RecoveryPassword()
        {
            RecoveryPasswordViewModel recoveryPass = new RecoveryPasswordViewModel()
            {
                MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups(),
                MenuItem = baseViewModelHelper.GetMenuItems()
            };
            return View(recoveryPass);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("RecoveryPassword")]
        public ActionResult RecoveryPassword(RecoveryPasswordViewModel recoveryPassword)
        {

            if (ModelState.IsValid)
            {
                string cellNumber = recoveryPassword.CellNumber.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2")
                    .Replace("۳", "3").Replace("۴", "4").Replace("۵", "5").Replace("۶", "6").Replace("v", "7")
                    .Replace("۸", "8").Replace("۹", "9");

                bool isValidMobile = Regex.IsMatch(cellNumber, @"(^(09|9)[0123456789][0123456789]\d{7}$)|(^(09|9)[0123456789][0123456789]\d{7}$)", RegexOptions.IgnoreCase);


                if (isValidMobile)
                {
                    Guid roleId = new Guid((System.Configuration.ConfigurationManager.AppSettings["customerRoleId"]));

                    User user = db.Users.Where(current => current.IsDeleted == false &&
                                                          current.CellNum == cellNumber)
                                                         .FirstOrDefault();

                    if (user != null)
                    {
                        string code = RandomCode();

                        SmsMessageHelper.SendSms(cellNumber, SmsMessageHelper.RecoveryPasswordCode(code));

                        UpdateUserPassword(user, code);

                        TempData["success"] = "کلمه عبور جدید برای شما از طریق پیامک ارسال گردید. اکنون می توانید از طریق صفحه ورود، وارد شود.";
                        recoveryPassword.MenuItem = baseViewModelHelper.GetMenuItems();
                        return View(recoveryPassword);
                    }
                    else
                        TempData["WrongCellNumber"] = "این شماره موبایل در سایت ثبت نام نکرده است. لطفا در وب سایت ثبت نام کنید.";
                }
                else
                    TempData["WrongCellNumber"] = "شماره موبایل وارد شده صحیح نمی باشد.";
            }

            RecoveryPasswordViewModel recoveryPass = new RecoveryPasswordViewModel()
            {
                MenuItem = baseViewModelHelper.GetMenuItems()
            };
            return View(recoveryPass);
        }

        [Route("Register")]
        [AllowAnonymous]
        public ActionResult Register()
        {
            RegisterViewModel register = new RegisterViewModel()
            {
                MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups(),
                MenuItem = baseViewModelHelper.GetMenuItems()
            };
            ViewBag.provinceId = new SelectList(db.Provinces.OrderBy(current => current.Title), "Id", "Title");
            ViewBag.cityId = ReturnCities(null);
            return View(register);
        }
        public SelectList ReturnCities(Guid? id)
        {
            SelectList cities;
            if (id == null)
            {
                cities = new SelectList(db.Cities.OrderBy(current => current.Title), "Id", "Title");
            }
            else
            {
                cities = new SelectList((db.Cities.Where(c => c.ProvinceId == id).OrderBy(current => current.Title)));
            }
            return cities;
        }
        public void UpdateUserPassword(User user, string pass)
        {
            user.Password = pass;
            db.SaveChanges();
        }
        public string RandomCode()
        {
            Random generator = new Random();
            String r = generator.Next(0, 100000).ToString("D5");
            return (r);
        }



        public ActionResult FinalizeRegister(string firstName, string lastName, string cellNumber, string password, string province, string city)
        {
            try
            {
                cellNumber = cellNumber.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4").Replace("۵", "5").Replace("۶", "6").Replace("v", "7").Replace("۸", "8").Replace("۹", "9");

                bool isValidMobile = Regex.IsMatch(cellNumber, @"(^(09|9)[0123456789][0123456789]\d{7}$)|(^(09|9)[0123456789][0123456789]\d{7}$)", RegexOptions.IgnoreCase);

                if (isValidMobile)
                {
                    User user = db.Users.FirstOrDefault(current => current.CellNum == cellNumber);


                    if (user == null)
                    {
                        Guid cityId = new Guid(city);
                        City cityItem = db.Cities.Find(cityId);
                        if (cityItem == null)
                            cityItem = db.Cities.FirstOrDefault();

                        Guid roleId = new Guid((System.Configuration.ConfigurationManager.AppSettings["customerRoleId"]));

                        User newUser = new User()
                        {
                            Id = Guid.NewGuid(),
                            FirstName = firstName,
                            LastName = lastName,
                            CellNum = cellNumber,
                            Password = password,
                            CityId = cityItem.Id,
                            CreationDate = DateTime.Now,
                            LastModifiedDate = DateTime.Now,
                            IsDeleted = false,
                            IsActive = true,
                            RoleId = roleId,
                            Code = GenerateUserCode()
                        };

                        db.Users.Add(newUser);
                        db.SaveChanges();
                        return Json("true", JsonRequestBehavior.AllowGet);

                    }
                    return Json("duplicateUser", JsonRequestBehavior.AllowGet);

                }

                else
                    return Json("invalidCellNumber", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }

        }

        public int GenerateUserCode()
        {
            return FindeLastUserCode() + 1;
        }

        public int FindeLastUserCode()
        {
            User user = db.Users.Where(current => current.IsDeleted == false).OrderByDescending(current => current.Code).FirstOrDefault();

            if (user != null)
                return user.Code;
            else
                return 999;
        }
        
    }
}