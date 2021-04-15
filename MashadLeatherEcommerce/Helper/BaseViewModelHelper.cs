using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using ViewModels;

namespace Helper
{
    public class BaseViewModelHelper
    {
        private DatabaseContext db = new DatabaseContext();
        public MenuItem GetMenuItems()
        {
            MenuItem menuItems = new MenuItem();

            menuItems.MenuProductCategories = GetMenuProductCategory();
            return menuItems;
        }
        public List<SiteGalleryGroup> GetMenuGalleryGroups()
        {

            return db.SiteGalleryGroups.Where(c => c.IsDeleted == false && c.IsActive).ToList();
        }
        public List<MenuProductCategory> GetMenuProductCategory()
        {
            List<ProductCategory> ProductCategories = db.ProductCategories.Where(current => current.IsDeleted == false
            && current.UrlParam.ToLower() != "gifts" && current.UrlParam.ToLower() != "leather-care"
            ).OrderBy(current => current.Priority).ToList();

            List<MenuProductCategory> menuProductCategories = new List<MenuProductCategory>();

            foreach (ProductCategory productCategory in ProductCategories)
            {
                bool hasChild = db.ProductCategories.Any(current => current.IsDeleted == false && current.ParentId == productCategory.Id);

                string menuClass = null;
                if (hasChild)
                    menuClass = "dropdown-submenu";

                string link = "#";
                if (!hasChild)
                    link = "/product/" + productCategory.Id;

                menuProductCategories.Add(new MenuProductCategory()
                {
                    Id = productCategory.Id,
                    Title = productCategory.TitleSrt,
                    HasChild = hasChild,
                    ParentId = productCategory.ParentId,
                    MenuClass = menuClass,
                    UrlParam = productCategory.UrlParam
                });
            }


            return menuProductCategories;
        }

        public List<ProductCategory> GetMenuJoinProductCategory()
        {
            return db.ProductCategories.Where(current => current.IsDeleted == false
            && current.UrlParam.ToLower() == "leather-care"
            ).OrderBy(current => current.Priority).ToList();

        }


        public string GetMenExtraMenuCategory()
        {
            ProductCategory productCategory = db.ProductCategories.FirstOrDefault(current => current.IsDeleted == false
             && current.UrlParam.ToLower() == "gifts");


            if (productCategory != null)
                return productCategory.TitleSrt;

            return string.Empty;
        }

        public User GetLoginUser()
        {
            try
            {

                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    var identity = (System.Security.Claims.ClaimsIdentity)HttpContext.Current.User.Identity;
                    string name = identity.FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
                    Guid userId = new Guid(name);

                    User user = db.Users.FirstOrDefault(c => c.Id == userId && c.IsDeleted == false);

                    if (user != null)
                        return user;

                    return null;
                }

                return null;

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}