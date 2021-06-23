using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class CarreerFamilyInformation : BaseEntity
    {
        [Display(Name = "نام و نام خانوادگی")]
        public string FullName { get; set; }
        [Display(Name = "نسبت")]
        public string Relationship { get; set; }
        [Display(Name = "تاریخ تولد")]
        public DateTime BirthdayDate { get; set; }
        [Display(Name = "میزان تحصیلات")]
        public string EducationalLevel { get; set; }
        [Display(Name = "شغل")]
        public string Job { get; set; }
        [Display(Name = "شماره تلفن")]
        public string CellNumber { get; set; }
        [Display(Name = "نام کارجو")]
        public Guid CarreerId { get; set; }
        public virtual Carreer Carreer { get; set; }
    }
}