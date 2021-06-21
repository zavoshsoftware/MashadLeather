using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class CarreerViewModel : _BaseViewModel
    {
        //public CarreerViewModel()
        //{
        //    Carreer = new Carreer();
        //}
        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "فیلد {0} اجباری می باشد.")]
        public string FullName { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "فیلد {0} اجباری می باشد.")]
        public string Email { get; set; }
        [Display(Name = "شماره تماس")]
        [Required(ErrorMessage = "فیلد {0} اجباری می باشد.")]
        public string CellNumber { get; set; }
        [Display(Name = "مسیر فایل رزومه")]
        public string ResumeFile { get; set; }
    }
}