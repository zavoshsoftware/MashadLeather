using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class HomeViewModel : _BaseViewModel
    {
        public List<SiteSlider> Sliders { get; set; }
        public List<ProductListItem> NewProducts { get; set; }
        public List<ProductListItem> MostSellProducts { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public string CurrentCurrency { get; set; }
    }
}