using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Helper;
using Models;

namespace ViewModels
{
    public class _BaseViewModel
    { 
        BaseViewModelHelper helper=new BaseViewModelHelper();

        public MenuItem MenuItem { get; set; }
        public List<SiteGalleryGroup> MenuGalleryGroups { get; set; }

        public List<ProductCategory> MenuJointProductGroups
        {
            get { return helper.GetMenuJoinProductCategory(); }
        }

        public string MenuExtraMenuCategory
        {
            get { return helper.GetMenExtraMenuCategory(); }
        }
        public User UserInfo { get { return helper.GetLoginUser(); } }

        public string LoginUserFullName
        {
            get
            {
                if (UserInfo != null)
                    return UserInfo.FirstName + " " + UserInfo.LastName;

                return null;
            }
        }

        public string LoginUserCellNumber { get
            {
                if (UserInfo != null)
                    return UserInfo.CellNum ;

                return null;
            }
        }

    }

    public class MenuItem
    {
        public List<MenuProductCategory> MenuProductCategories { get; set; }
    }

    public class MenuProductCategory
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool HasChild { get; set; }
        public Guid? ParentId { get; set; }
        public string MenuClass { get; set; }
        public string UrlParam { get; set; }
    }
}