using Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Models
{
    public class SiteSlider : BaseEntity
    {
        [Display(Name="اولویت")]
        public int Order { get; set; }

        [Display(Name="تصویر")]
        public string ImageUrl { get; set; }
        [Display(Name = "تصویر انگلیسی")]
        public string ImageUrlEn { get; set; }
        [Display(Name = "تصویر عربی")]
        public string ImageUrlAr { get; set; }

        [Display(Name="عنوان")]
        public string Title { get; set; }
        [Display(Name = "عنوان انگلیسی")]
        public string TitleEn { get; set; }

        [Display(Name = "عنوان عربی")]
        public string TitleAr { get; set; }

        [Display(Name="عنوان لینک")]
        public string LinkTitle { get; set; }
        [Display(Name = "عنوان لینک انگلیسی")]
        public string LinkTitleEn { get; set; }

        [Display(Name = "عنوان لینک عربی")]
        public string LinkTitleAr { get; set; }

        [Display(Name="صفحه فرود لینک")]
        public string LandingPage { get; set; }


        [Display(Name="صفحه فرود لینک انگلیسی")]
        public string LandingPageEn { get; set; }


        [Display(Name="صفحه فرود لینک عربی")]
        public string LandingPageAr { get; set; }



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
        [NotMapped]
        public string ImageUrlSrt
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.ImageUrlEn;
                    case "fa-ir":
                        return this.ImageUrl;
                    case "ar-ae":
                        return this.ImageUrlAr;
                    default:
                        return String.Empty;
                }
            }
        }
        [NotMapped]
        public string LinkTitleSrt
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.LinkTitleEn;
                    case "fa-ir":
                        return this.LinkTitle;
                    case "ar-ae":
                        return this.LinkTitleAr;
                    default:
                        return String.Empty;
                }
            }
        }

        [NotMapped]
        public string LandingPageSrt
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.LandingPageEn;
                    case "fa-ir":
                        return this.LandingPage;
                    case "ar-ae":
                        return this.LandingPageAr;
                    default:
                        return this.LandingPage;
                }
            }
        }
    }
}