using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class ChangeAddressViewModel
    {
        public Guid Id { get; set; }
        [Display(Name="استان")]
        public Guid ProvinceId { get; set; }
        [Display(Name="شهر")]
        public Guid CityId { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name="آدرس")]
        public string Address { get; set; }
        [Display(Name="کد پستی")]
        public string PostalCode { get; set; }
    }
}