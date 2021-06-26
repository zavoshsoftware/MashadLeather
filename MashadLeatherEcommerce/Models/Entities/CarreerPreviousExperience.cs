using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class CarreerPreviousExperience : BaseEntity
    {
        [Display(Name = "نام شرکت/سازمان")]
        public string CompanyName { get; set; }
        [Display(Name = "سمت")]
        public string Post { get; set; }
        [Display(Name = "تاریخ شروع کار")]
        public string StartDatetime { get; set; }
        [Display(Name = "تاریخ پایان کار")]
        public string EndDatetime { get; set; }
        [Display(Name = "تلفن")]
        public string Phone { get; set; }
        [Display(Name = "علت ترک کار")]
        public string LeavingWorkReason { get; set; }
        [Display(Name = "مبلغ دریافتی")]
        public string ReceivedMoney { get; set; }
        [Display(Name = "آدرس")]
        public string Address { get; set; }
        [Display(Name = "نام کارجو")]
        public Guid CarreerId { get; set; }
        public virtual Carreer Carreer { get; set; }
    }
}