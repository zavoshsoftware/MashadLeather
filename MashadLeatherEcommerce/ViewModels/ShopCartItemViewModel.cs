using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class ShopCartList
    {
        public List<ShopCartItemViewModel> ShopCartItems { get; set; }
        public decimal Amount { get; set; }
        public decimal ShippmentPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPayment { get; set; }
    }
    public class ShopCartItemViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Price { get; set; }
        public string Amount { get; set; }
        public string Qty { get; set; }
        public string color { get; set; }
        public string colorTitle { get; set; }
        public string size { get; set; }
        public string SizeTitle { get; set; }

    }
}