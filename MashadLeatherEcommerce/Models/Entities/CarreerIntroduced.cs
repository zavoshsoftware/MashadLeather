using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class CarreerIntroduced : BaseEntity
    {
        [Display(Name = "نام و نام خانوادگی")]
        public string FullName { get; set; }
        [Display(Name = "نسبت")]
        public string Relationship { get; set; }
        [Display(Name = "شغل")]
        public string Job { get; set; }
        [Display(Name = "محل کار")]
        public string WorkPlace { get; set; }
        [Display(Name = "تلفن منزل")]
        public string HomePhone { get; set; }
        [Display(Name = "تلفن همراه")]
        public string CellNumber { get; set; }
        [Display(Name = "آدرس")]
        public string Address { get; set; }
        [Display(Name = "کارجو")]
        public Guid CarreerId { get; set; }
        public virtual Carreer Carreer { get; set; }
    }
}