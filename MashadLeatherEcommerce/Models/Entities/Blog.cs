using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helper;

namespace Models
{
    public class Blog: BaseEntity
    {
        [Display(Name="عنوان")]
        public string Title { get; set; }

        [Display(Name="عنوان انگلیسی")]
        public string TitleEn { get; set; }
        [Display(Name = "عنوان عربی")]
        public string TitleAr { get; set; }

        [Display(Name = "پارامتر url")]
        public string UrlParam { get; set; }

        [Display(Name="خلاصه")]
        public string Summery { get; set; }

        [Display(Name= "خلاصه انگلیسی")]
        public string SummeryEn { get; set; }
        [Display(Name = "خلاصه عربی")]
        public string SummeryAr { get; set; }

        [Display(Name="تصویر")]
        public string ImageUrl { get; set; }

        [Display(Name="متن")]
        [DataType(DataType.Html)]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        public string Body { get; set; }

        [Display(Name = "متن انگلیسی")]
        [DataType(DataType.Html)]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        public string BodyEn { get; set; }

        [Display(Name = "متن عربی")]
        [DataType(DataType.Html)]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        public string BodyAr { get; set; }

        [Display(Name = "گروه وبلاگ")]
        public Guid? BlogGroupId { get; set; }
        public virtual BlogGroup BlogGroup { get; set; }

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
        public string SummerySrt
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.SummeryEn;
                    case "fa-ir":
                        return this.Summery;
                    case "ar-ae":
                        return this.SummeryAr;
                    default:
                        return String.Empty;
                }
            }
        }

        [NotMapped]
        public string BodySrt
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.BodyEn;
                    case "fa-ir":
                        return this.Body;
                    case "ar-ae":
                        return this.BodyAr;
                    default:
                        return String.Empty;
                }
            }
        }
    }
}