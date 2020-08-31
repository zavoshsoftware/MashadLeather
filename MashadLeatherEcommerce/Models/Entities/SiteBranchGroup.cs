using Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Models
{
    public class SiteBranchGroup : BaseEntity
    {
        [Display(Name="شهر")]
        public string Title { get; set; }
        [Display(Name = "شهر انگلیسی")]
        public string TitleEn { get; set; }

        [Display(Name = "شهر عربی")]
        public string TitleAr { get; set; }

        [Display(Name="اولویت نمایش")]
        public int? Order { get; set; }
        public virtual ICollection<SiteBranch> SiteBranches { get; set; }
        GetCulture oGetCulture = new GetCulture();

        [NotMapped]
        public string TitleSrt
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.TitleEn;
                    case "fa-ir":
                        return this.Title;
                         case "ar-ae":
                        return this.TitleAr;
                    default:
                        return String.Empty;
                }
            }
        }
    }
}