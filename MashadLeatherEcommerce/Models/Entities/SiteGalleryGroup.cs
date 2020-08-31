using Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Models
{
    public class SiteGalleryGroup : BaseEntity
    {
        public SiteGalleryGroup()
        {
            SiteGalleries = new List<SiteGallery>();
        }

        [Display(Name = "عنوان گروه")]
        public string Title { get; set; }

        [Display(Name = "عنوان انگلیسی")]
        public string TitleEn { get; set; }

        [Display(Name = "عنوان عربی")]
        public string TitleAr { get; set; }

        [Display(Name = "پارامتر url گروه")]
        public string UrlParam { get; set; }

        [Display(Name = "متن")]
        [DataType(DataType.Html)]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        public string Body { get; set; }

        public virtual ICollection<SiteGallery> SiteGalleries { get; set; }

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