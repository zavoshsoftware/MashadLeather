using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class OrderDashboardViewModel
    {
        public int TotalOrderQty { get; set; }
        public decimal TotamOrderAmount { get; set; }
        public int TotalOnProgressOrderQty { get; set; }
        public int TotalFinalOrderQty { get; set; }
        public List<OrderByCityViewModel> OrderByProvince { get; set; }
    }

    public class OrderByCityViewModel
    {
        public Guid ProvinceId { get; set; }
        [Display(Name ="استان")]
        public string ProvinceTitle { get; set; }
        [Display(Name ="تعداد سفارشات ثبت شده")]
        public int TotalOrderQtyByProvince { get; set; }
        [Display(Name ="جمع ریالی سفارشات")]
        public decimal TotamOrderAmountByProvince { get; set; }
        [Display(Name ="جمع ریالی سفارشات")]
        public string TotamOrderAmountByProvinceStr { get { return TotamOrderAmountByProvince.ToString("n0"); } }
    }
}