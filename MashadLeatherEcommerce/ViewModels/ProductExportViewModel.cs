using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class ProductExportViewModel
    {
        public Guid Id { get; set; }
        [Display(Name="بارکد")]
        public string Barcode { get; set; }
        [Display(Name = "رنگ")]
        public string Color { get; set; }
        [Display(Name="سایز")]
        public string Size { get; set; }
        [Display(Name="قیمت")]
        public string Amount { get; set; }
        [Display(Name="قیمت بعد از تخفیف")]
        public string DiscountAmount { get; set; }
        [Display(Name="عنوان محصول")]
        public string Title { get; set; }
        [Display(Name="موجودی")]
        public string Quantity { get; set; }
        [Display(Name="کد محصول")]
        public string Code { get; set; }
    }

    public class ProductExportForExcelViewModel
    {
        [Display(Name = "عنوان محصول")]
        public string Title { get; set; }
        [Display(Name = "کد محصول")]
        public string Code { get; set; }
        [Display(Name = "بارکد")]
        public string Barcode { get; set; }
        [Display(Name = "رنگ")]
        public string Color { get; set; }
        [Display(Name = "سایز")]
        public string Size { get; set; }
        [Display(Name = "قیمت")]
        public string Amount { get; set; }
        [Display(Name = "قیمت بعد از تخفیف")]
        public string DiscountAmount { get; set; }

        [Display(Name = "موجودی")]
        public string Quantity { get; set; }
 
    }
}