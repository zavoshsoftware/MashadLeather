using Helper;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace MashadLeatherEcommerce.Controllers
{
    public class HomeController : Controller
    {
        public static readonly string PgwSite = ConfigurationManager.AppSettings["PgwSite"];
        public static readonly string CallBackUrl = ConfigurationManager.AppSettings["CallBackUrl"];
        public static readonly string TerminalId = ConfigurationManager.AppSettings["TerminalId"];
        public static readonly string UserName = ConfigurationManager.AppSettings["UserName"];
        public static readonly string UserPassword = ConfigurationManager.AppSettings["UserPassword"];
        private DatabaseContext db = new DatabaseContext();
        GetCurrency oGetCurrency = new GetCurrency();
        // GET: Home
        Helper.BaseViewModelHelper baseViewModelHelper = new BaseViewModelHelper();
        public ActionResult Index()
        {


            HomeViewModel home = new HomeViewModel()
            {
                MenuItem = baseViewModelHelper.GetMenuItems(),
                MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups(),
                NewProducts = GetNewstProductList(true, false),
                MostSellProducts = GetNewstProductList(false, true),
                ProductCategories = db.ProductCategories.Where(c => c.IsDeleted == false && c.ParentId == null && c.IsActive).Take(4).ToList(),
                Sliders = db.SiteSliders.Where(c => c.IsDeleted == false && c.IsActive).OrderBy(c => c.Order).ToList(),
                CurrentCurrency = oGetCurrency.CurrentCurrency()
            };
            return View(home);

        }


        public List<ProductListItem> GetNewstProductList(bool isinHome, bool isMostSale)
        {
            List<Product> products = new List<Product>();

            if (isinHome)
                products = db.Products
                     .Where(current => current.IsDeleted == false && current.IsActive && current.ParentId == null &&
                                       current.IsInHome).Take(8).ToList();

            if (isMostSale)
                products = db.Products
                    .Where(current => current.IsDeleted == false && current.IsActive && current.ParentId == null &&
                                      current.IsMostSale).Take(8).ToList();

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
                    TagTitle = product.TagTitleSrt
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

        [Route("privacy")]
        public ActionResult Privacy()
        {
            Guid id = new Guid("9b7a3586-edf8-4803-8dc0-009203bda89d");
            TextViewModel textViewModel = new TextViewModel()
            {
                MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups(),
                MenuItem = baseViewModelHelper.GetMenuItems(),
                Text = db.Texts.Where(current => current.IsDeleted == false && current.Id == id).FirstOrDefault()
            };
            return View(textViewModel);
        }

        public ActionResult Saman()
        {
            //using (var wb = new WebClient())
            //{
            //    var data = new NameValueCollection();
            //    data["Amount"] = "1000";
            //    data["ResNum"] = "2";
            //    data["MID"] = "21284935";
            //    data["RedirectURL"] = "https://google.com";

            //    var response = wb.UploadValues("https://sep.shaparak.ir/payment.aspx", "POST", data);
            //    string responseInString = Encoding.UTF8.GetString(response);
            //}
            //var data = new NameValueCollection();
            //data["Amount"] = "1000";
            //data["ResNum"] = "2";
            //data["MID"] = "21284935";
            //data["RedirectURL"] = "https://google.com";

            //var url = "https://sep.shaparak.ir/payment.aspx?Amount=20000&ResNum=2&MID=21284935&RedirectURL=https://google.com";
            //var request = WebRequest.Create(url);
            //request.Method = "POST";
            //request.ContentLength = 0; //got an error without this line
            //var response = request.GetResponse();
            //var data = response.GetResponseStream();
            //string result;
            //using (var sr = new StreamReader(data))
            //{
            //    result = sr.ReadToEnd();
            //}
            //return Json(result);
            //var values = new Dictionary<string, string>
            //{
            //    { "Amount", "1000" },
            //    { "ResNum", "2" },
            //    { "MID", "21284935" },
            //    { "RedirectURL", "https://google.com" }
            //};

            //var content = new FormUrlEncodedContent(values);
            //HttpClient client = new HttpClient();

            //var response =  client.PostAsync("https://sep.shaparak.ir/payment.aspx", content);

            // var responseString = await response.Content.ReadAsStringAsync();

            return View();
        }
        //[AllowAnonymous]
        //public PartialViewResult GetMenu()
        //{
        //    //List<Master_Pages> Menu = db.Master_Pages.Where(w => w.Active && w.MasterPageID == null && w.MenuTop).OrderBy(o => o.PriorityView).ToList();
        //    return PartialView("~/Views/Shared/MashadLeather/MLMenu.cshtml", Menu);
        //}


        [Route("About")]
        public ActionResult About()
        {
            AboutViewModel about = new AboutViewModel();
            Guid aboutTextTypeId = new Guid("267bb0fc-f450-4f5c-bb99-4d0468fb567d");

            about.MenuItem = baseViewModelHelper.GetMenuItems();
            about.MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups();

            Guid headerId = new Guid("BDCA1DF5-7358-4EBB-815D-5D9EF22F3382");
            Text header = db.Texts.Find(headerId);
            if (header != null)
                about.HeaderImage = header.ImageUrl;

            Guid aboutId = new Guid("F6B76069-2B0C-46A4-B593-CC8453541DBB");
            Text aboutDesc = db.Texts.Find(aboutId);
            if (aboutDesc != null)
                about.MainText = aboutDesc.BodySrt;


            Guid partnerId = new Guid("8685fcb3-f117-46d5-9fcb-a0c334b5f440");
            Text aboutPartner = db.Texts.Find(partnerId);
            if (aboutPartner != null)
                about.Partners = aboutPartner.BodySrt;



            about.Numbers = db.Texts.Where(c => c.Name == "number" && c.TextTypeId == aboutTextTypeId).ToList();
            about.Certificates = db.Texts.Where(c => c.Name == "cer" && c.TextTypeId == aboutTextTypeId).ToList();


            about.SaleSystem = db.Texts.Where(c => c.Name == "sale" && c.TextTypeId == aboutTextTypeId).ToList();


            return View(about);

        }


        [Route("customerclub")]
        public ActionResult CustomerClub()
        {
            CustomerClubViewModel club = new CustomerClubViewModel()
            {
                MenuItem = baseViewModelHelper.GetMenuItems(),
                MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups(),
                CustomerClub = db.Texts.FirstOrDefault(current => current.Name == "club")
            };

            return View(club);

        }


        [Route("contact")]
        public ActionResult ContactUs()
        {




            ContactViewModel contact = new ContactViewModel();
            contact.MenuItem = baseViewModelHelper.GetMenuItems();
            contact.MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups();





            Guid id = new Guid("E6852A16-F844-44DC-A913-AC36A9574729");
            Text text = db.Texts.Find(id);
            if (text != null)
                contact.FactoryAdress = text.SummerySrt;

            id = new Guid("AA92C013-0BB5-4E92-9F23-6BE6FDA0E0DE");
            text = db.Texts.Find(id);
            if (text != null)
                contact.MashadAddres = text.SummerySrt;



            id = new Guid("CBEDFAF9-AF29-4507-9B2E-4895190698CF");
            text = db.Texts.Find(id);
            if (text != null)
                contact.MashadPhone = text.SummerySrt;




            id = new Guid("B6FC4412-1056-49E0-B7B4-42D9AC8C436E");
            text = db.Texts.Find(id);
            if (text != null)
                contact.TehranAdress = text.SummerySrt;





            id = new Guid("7F5D1D0C-E79A-4807-9593-B2B680C36B80");
            text = db.Texts.Find(id);
            if (text != null)
                contact.TehranPhone = text.SummerySrt;



            return View(contact);

        }




        [Route("صفحه/درباره چرم مشهد")]
        public ActionResult Redirect1()
        {
            return RedirectPermanent("/about");
        }


        [Route("گالری/گالری-ویدئو-ها")]
        public ActionResult Redirect2()
        {
            return RedirectPermanent("/gallery/video");
        }

        [Route("اخبار")]
        public ActionResult Redirect3()
        {
            return RedirectPermanent("/blog/news");
        }

        [Route("صفحه/قوانین باشگاه مشتریان")]
        public ActionResult Redirect4()
        {
            return RedirectPermanent("/customerclub");
        }

        [Route("شعب")]
        public ActionResult Redirect5()
        {
            return RedirectPermanent("/branches");
        }

        [Route("تماس-با-ما/چرم-مشهد")]
        public ActionResult Redirect6()
        {
            return RedirectPermanent("contact");
        }

        [Route("OrganizationalSales")]
        public ActionResult OrganizationalSales()
        {
            OrganizationalSaleViewModel organizationalSale = new OrganizationalSaleViewModel();
            Guid headerId = new Guid("05c0998d-b1a4-4fea-95af-fd41a114cfd8");
            Text header = db.Texts.Find(headerId);
            organizationalSale.MenuItem = baseViewModelHelper.GetMenuItems();
            organizationalSale.MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups();
            if (header != null)
            {
                organizationalSale.MainText = header.BodySrt;
                organizationalSale.HeaderImage = header.ImageUrl;
            }
            return View(organizationalSale);
        }
        [Route("Export")]
        public ActionResult Export()
        {
            OrganizationalSaleViewModel organizationalSale = new OrganizationalSaleViewModel();
            Text header = db.Texts.Where(current => current.Name == "export").FirstOrDefault();
            organizationalSale.MenuItem = baseViewModelHelper.GetMenuItems();
            organizationalSale.MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups();
            if (header != null)
            {
                organizationalSale.MainText = header.BodySrt;
                organizationalSale.HeaderImage = header.ImageUrl;
            }
            return View(organizationalSale);
        }

        [Route("career")]
        public ActionResult Cooperation()
        {
            CooperationViewModel cooperationViewModel = new CooperationViewModel();
            cooperationViewModel.MenuItem = baseViewModelHelper.GetMenuItems();
            cooperationViewModel.MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups();
            Text mainText = db.Texts.Where(current => current.Name == "cooperation").FirstOrDefault();
            if (mainText != null)
            {
                cooperationViewModel.MainText = mainText;
                cooperationViewModel.HeaderImage = mainText.ImageUrl;
            }
            TempData["alertText"] = "";
            //if (!string.IsNullOrEmpty(alertText))
            //{
            //    TempData["alertText"] = alertText;
            //}
            return View(cooperationViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("career")]
        public ActionResult Cooperation(CooperationViewModel model, HttpPostedFileBase fileUpload)
        {
            model.MenuItem = baseViewModelHelper.GetMenuItems();
            model.MenuGalleryGroups = baseViewModelHelper.GetMenuGalleryGroups();
            Text mainText = db.Texts.Where(current => current.Name == "cooperation").FirstOrDefault();
            if (mainText != null)
            {
                model.MainText = mainText;
                model.HeaderImage = mainText.ImageUrl;
            }


            try
            {


                string newFilenameUrl = string.Empty;


                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/Resume/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);
                    TempData["alertText"] = "رزومه شما با موفقیت ثبت گردید.";

                    ResumeFile resume = new ResumeFile()
                    {
                        Id = Guid.NewGuid(),
                        CreationDate = DateTime.Now,
                        IsDeleted = false,
                        IsActive = true,
                        FileUrl = newFilenameUrl
                    };
                    db.ResumeFiles.Add(resume);
                    db.SaveChanges();

                    return View(model);
                    //return RedirectToAction("Cooperation",new { alertText = message });


                }
                TempData["alertText"] = "خطا در ثبت رزومه!! مجددا تلاش نمایید";
                return View(model);
            }
            catch (Exception e)
            {
                TempData["alertText"] = "خطا در ثبت رزومه!! مجددا تلاش نمایید";
                return View(model);
            }

            //return RedirectToAction("Cooperation", new { alertText = message });
        }

        public ActionResult ChangeCurrency()
        {
            return View(db.Configurations.FirstOrDefault());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeCurrency(Models.Configuration configuration)
        {
            if (ModelState.IsValid)
            {
                configuration.IsDeleted = true;
                configuration.IsDeleted = false;
                configuration.LastModifiedDate = DateTime.Now;
                db.Entry(configuration).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View(configuration);
        }

    }
}