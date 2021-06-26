using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class CarreerEducationalCourse : BaseEntity
    {
        [Display(Name = "نام دوره آموزشی")]
        public string CourseName { get; set; }
        [Display(Name = "نام موسسه / مرکز آموزشی")]
        public string InstitutionName { get; set; }
        [Display(Name = "مدت دوره")]
        public string CourseDuration { get; set; }
        [Display(Name = "مهارت کسب شده")]
        public string Skill { get; set; }
        [Display(Name = "نام کارجو")]
        public Guid CarreerId { get; set; }
        public virtual Carreer Carreer { get; set; }
    }
}