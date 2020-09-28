using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class ProductListViewModel : _BaseViewModel
    {
        public List<ProductListItem> Products { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public List<Comment> Commnets { get; set; }
        public List<BreadcrumpItemViewModel> BreadcrumpItems { get; set; }
        public string CurrentCurrency { get; set; }
    }

    public class ProductListItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Amount { get; set; }
        public string DiscountAmount { get; set; }
        public string ImageUrl { get; set; }
        public string ProductCategoryTitle { get; set; }
        public string LikeClass { get; set; }
        public bool IsInPromotion { get; set; }
        public bool HasTag { get; set; }
        public string TagTitle { get; set; }
        public bool IsActive { get; set; }
    }
   
}