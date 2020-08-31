using Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Models
{
    public class BlogGroup:BaseEntity
    {
        [Display(Name = "گروه وبلاگ")]
        public string Title { get; set; }
        [Display(Name = "عنوان انگلیسی")]
        public string TitleEn { get; set; }
        [Display(Name = "عنوان عربی")]
        public string TitleAr { get; set; }

        [Display(Name = "پارامتر url")]
        public string UrlParam { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
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