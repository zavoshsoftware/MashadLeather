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
    }

    public class ProductCategoryListItem
    {
        public ProductCategory ProductCategory { get; set; }
        public bool IsParent { get; set; }
    }
}