using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MashadLeatherEcommerce.Enum
{
    public static class EnumData
    {
        public enum MilitaryStatus
        {
            [Display(Name = "معافیت: تحصیلی")]
            EducationalExemption,
            [Display(Name = "پزشکی")]
            MedicalExemption,
            [Display(Name = "مشمول")]
            Included,
            [Display(Name = "پایان خدمت")]
            EndMilitary
        }
    }
}