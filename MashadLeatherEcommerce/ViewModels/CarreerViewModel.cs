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
        [Display(Name = "نام و نام خانوادگی*")]
        [Required(ErrorMessage = "فیلد {0} اجباری می باشد.")]
        public string FullName { get; set; }
        [Display(Name = "ایمیل*")]
        [Required(ErrorMessage = "فیلد {0} اجباری می باشد.")]
        public string Email { get; set; }
        [Display(Name = "شماره تلفن همراه*")]
        [Required(ErrorMessage = "فیلد {0} اجباری می باشد.")]
        public string CellNumber { get; set; }
        [Display(Name = "کدملی*")]
        [Required(ErrorMessage = "فیلد {0} اجباری می باشد.")]
        public string NationalCode { get; set; }
        
        [Display(Name = "تاریخ تولد*")]
        [Required(ErrorMessage = "فیلد {0} اجباری می باشد.")]
        public DateTime BirthdayDate { get; set; }
       
        [Display(Name = "محل تولد*")]
        [Required(ErrorMessage = "فیلد {0} اجباری می باشد.")]
        public string PlaceOfBirth { get; set; }
        [Display(Name = "جنسیت*")]
        [Required(ErrorMessage = "فیلد {0} اجباری می باشد.")]
        public bool IsLady { get; set; }
        [Display(Name = "متاهل*")]
        [Required(ErrorMessage = "فیلد {0} اجباری می باشد.")]
        public bool IsMarried { get; set; }

        [Display(Name = "تعداد فرزندان)")]
        public int ChidNumber { get; set; }
        [Display(Name = "تعداد افراد تحت تکلف*")]
        public int PeopleInChargeNumber { get; set; }
        [Display(Name = "ملیت*")]
        [Required(ErrorMessage = "فیلد {0} اجباری می باشد.")]
        public string Nationality { get; set; }
        [Display(Name = "آدرس محل سکونت*")]
        [Required(ErrorMessage = "فیلد {0} اجباری می باشد.")]
        public string Address { get; set; }
        [Display(Name = "وضعیت نظام وظیفه*")]
        public string MilitaryStatus { get; set; }
        [Display(Name = "وضعیت جسمانی*")]
        public string PhysicalCondition { get; set; }
        [Display(Name = "آیا دارای سابقه پرداخت حق بیمه هستید*")]
        [Required(ErrorMessage = "فیلد {0} اجباری می باشد.")]
        public bool IsInsurance { get; set; }
     
        [Display(Name = "مدت سابقه بیمه کار*")]
        [Required(ErrorMessage = "فیلد {0} اجباری می باشد.")]
        public int DurationInsuranceHistory { get; set; }
     
        [Display(Name = "مدرک تحصیلی*")]
        [Required(ErrorMessage = "فیلد {0} اجباری می باشد.")]
        public string Education { get; set; }
      
        [Display(Name = "رشته تحصیلی*")]
        [Required(ErrorMessage = "فیلد {0} اجباری می باشد.")]
        public string Major { get; set; }
    
        [Display(Name = "محل اخذ مدرک (دانشگاه آخرین مدرک تحصیلی)*")]
        public string LastUniversity { get; set; }
      
        [Display(Name = "تاریخ اخذ آخرین مدرک تحصیلی*")]
        [Required(ErrorMessage = "فیلد {0} اجباری می باشد.")]
        public DateTime LastCertificateDateTime { get; set; }
      
        [Display(Name = "نرم افزارهای office Word, Excel ,Power Point))*")]
        public string Software { get; set; }
        [Display(Name = "Windows*")]
        public string Windows { get; set; }
        [Display(Name = "سایر نرم افزارها*")]
        public string OtherSoftware { get; set; }
        [Display(Name = "Writing نوشتن*")]
        public string Writing { get; set; }
        [Display(Name = "Reading خواندن*")]
        public string Reading { get; set; }
        [Display(Name = "Speaking خواندن*")]
        public string Speaking { get; set; }
        [Display(Name = "Listening گوش کردن*")]
        public string Listening { get; set; }
        [Display(Name = "نام معرف خود")]
        public string IntroduceName { get; set; }
        [Display(Name = "سمت معرف")]
        public string IntroducePost { get; set; }
        [Display(Name = "تمایل دارید در چه سمت شغلی مشغول به کارشوید")]
        public string InterestedJob { get; set; }
        [Display(Name = "حقوق مورد انتظار")]
        public string IsExpectedSalary { get; set; }
        [Display(Name = "مبلغ مورد درخواست")]
        public string RequestedPrice { get; set; }
        [Display(Name = "نام مصاحبه کننده منابع انسانی")]
        public string HumanResourceinterviewerName { get; set; }
        [Display(Name = "سمت شغلی مصاحبه کننده منابع انسانی")]
        public string HumanResourceinterviewerJob { get; set; }
        [Display(Name = "توضیحات مصاحبه کننده تخصصی")]
        public string HumanResourceDescription { get; set; }
        [Display(Name = "نام مصاحبه کننده تخصصی")]
        public string SpecializedInterviewerName { get; set; }
        [Display(Name = "سمت شغلی مصاحبه کننده تخصصی")]
        public string SpecializedInterviewerPost { get; set; }
        [Display(Name = "توضیحات مصاحبه کننده تخصصی")]
        public string SpecializedInterviewerDescription { get; set; }
        [Display(Name = "استخدام نامبرد مورد تایید است")]
        public bool IsConfirmed { get; set; }
        [Display(Name = "نحوه آشنایی با شرکت چرم مشهد*")]
        public string Familiar { get; set; }
        [Display(Name = "آیا سابقه فعالیت های ورزشی و هنری داشته اید ؟ ذکر نمایید")]
        public string SportHistory { get; set; }
        [Display(Name = "توضیحات")]
        public string ConfirmedDescription { get; set; }
    }
}