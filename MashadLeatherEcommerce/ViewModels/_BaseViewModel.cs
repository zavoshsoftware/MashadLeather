using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class _BaseViewModel
    {
        public MenuItem MenuItem { get; set; }
        public List<SiteGalleryGroup> MenuGalleryGroups { get; set; }
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