using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class ProductCategoryViewModel : _BaseViewModel
    {
        //public ProductCategory ParentProductCategory { get; set; }
        public List<ProductCategoryListItem> ProductCategories { get; set; }

        public List<BreadcrumpItemViewModel> BreadcrumpItems { get; set; }
    }

    public class ProductCategoryListItem
    {
        public ProductCategory ProductCategory { get; set; }
        public bool IsParent { get; set; }
    }

    public class BreadcrumpItemViewModel
    {
        public string Title { get; set; }
        public string UrlParam { get; set; }
        public int Order { get; set; }
    }
}