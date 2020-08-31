using Helper;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace MashadLeatherEcommerce.Controllers
{
    public class UserProductLikeController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        // GET: UserProductLike
        public ActionResult Index()
        {
            return View();
        }
        [Route("wishList")]
        public ActionResult List()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Helper.BaseViewModelHelper baseViewModelHelper = new BaseViewModelHelper();
                Guid userId = new Guid(HttpContext.User.Identity.Name);
              List<UserProductLike> userProductLikes = db.UserProductLike.Where(current => current.UserId == userId && current.IsDeleted == false).ToList();

                ViewBag.total = userProductLikes.Count();

                ProductListViewModel productList = new ProductListViewModel
                {
                    MenuItem = baseViewModelHelper.GetMenuItems(),
                    Products = GetProductList(userProductLikes),
                    
                };
                ViewBag.Title = "لیست محصولات مورد علاقه";
                return View(productList);
            }
            else
                return Redirect("/login");
        }

        public List<ProductListItem> GetProductList(List<UserProductLike> userProductLike)
        {
            List<ProductListItem> productItems = new List<ProductListItem>();

            foreach (UserProductLike like in userProductLike)
            {
                Product product = db.Products.Find(like.ProductId);
                productItems.Add(new ProductListItem()
                {
                    Id = like.ProductId,
                    ImageUrl = product.ImageUrl,
                    Amount = string.Format("{0:#,#}", product.Amount),
                    Title = product.Title,
                    ProductCategoryTitle = product.ProductCategory.Title,
                    LikeClass = ReturnUserLike(like.ProductId)
                });
            }

            return productItems;
        }
       
        
        public string ReturnUserLike(Guid id)
        {
            string likeClass = string.Empty;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Guid userId = new Guid(HttpContext.User.Identity.Name);
                UserProductLike userProductLike = db.UserProductLike.Where(current => current.UserId == userId && current.ProductId == id && current.IsDeleted == false).FirstOrDefault();
                if (userProductLike != null)
                    likeClass = "likeList";
            }

            return likeClass;
        }
    }
}