using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class ProductQuickViewModel:_BaseViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
         public List<ProductColor> Colors { get; set; }
        public List<Models.Size> Sizes { get; set; }
        public List<Models.ProductImage> ProductImages { get; set; }
        public string Description { get; set; }
        public string ProductCategoryTitle { get; set; }
        public string FacebookShareLink { get; set; }
        public string TwitterShareLink { get; set; }
        public string GooglePlusShareLink { get; set; }
        public string TelegramShareLink { get; set; }
        public string SecondColor { get; set; }
        public string DiscountAmount { get; set; }
        public bool IsInPromotion { get; set; }
        public bool IsActive  { get; set; }
    }

    public class ProductColor
    {
        public Guid Id { get; set; }
        public string TitleSrt { get; set; }
        public string DecreaseAmount { get; set; }
        public string HexCode { get; set; }
    }
}