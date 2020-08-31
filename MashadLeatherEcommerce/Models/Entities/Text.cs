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
    public class Text:BaseEntity
    {
        [Display(Name ="Title",ResourceType =typeof(Resources.Models.Text))]
        public string Title { get; set; }
        [Display(Name = "TitleEn", ResourceType = typeof(Resources.Models.Text))]
        public string TitleEn { get; set; }

        [Display(Name = "عنوان عربی")]
        public string TitleAr { get; set; }

        [Display(Name = "Body", ResourceType = typeof(Resources.Models.Text))]
        [AllowHtml]
        [UIHint("RichText")]
        [Column(TypeName = "ntext")]
        public string Body { get; set; }
        [Display(Name = "BodyEn", ResourceType = typeof(Resources.Models.Text))]
        [AllowHtml]
        [UIHint("RichText")]
        [Column(TypeName = "ntext")]
        public string BodyEn { get; set; }

        [Display(Name = "متن عربی")]
        [AllowHtml]
        [UIHint("RichText")]
        [Column(TypeName = "ntext")]
        public string BodyAr { get; set; }


        [Display(Name="متن کوتاه")]
        [DataType(DataType.MultilineText)]
        public string Summery { get; set; }

        [Display(Name="متن کوتاه انگلیسی")]
        [DataType(DataType.MultilineText)]
        public string SummeryEn { get; set; }

        [Display(Name = "متن کوتاه عربی")]
        [DataType(DataType.MultilineText)]
        public string SummeryAr { get; set; }

        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public Guid? TextTypeId { get; set; }
        public virtual TextType TextType { get; set; }

        GetCulture oGetCulture = new GetCulture();

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