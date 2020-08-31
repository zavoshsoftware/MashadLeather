using Helper;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Models
{
    public class SiteGallery:BaseEntity
    {

        [Display(Name="عنوان")]
        public string Title { get; set; }
        [Display(Name = "عنوان انگلیسی")]
        public string TitleEn { get; set; }

        [Display(Name = "عنوان عربی")]
        public string TitleAr { get; set; }

        [Display(Name="تصویر")]
        public string ImageUrl { get; set; }
        [Display(Name = "پارامتر url")]
        public string UrlParam { get; set; }

        [Display(Name="بارگزاری فایل")]
        public string FileUrl { get; set; }

        [Display(Name="آدرس آپارات ویدیو")]
        [DataType(DataType.MultilineText)]
        public string FileAddress { get; set; }

        [Display(Name="تصویر")]
        public string ImageUrlThumb { get; set; }
        [Display(Name = "اولویت")]
        public int? Order { get; set; }
        [Display(Name = "گروه گالری")]
        public Guid SiteGalleryGroupId { get; set; }
        public virtual SiteGalleryGroup SiteGalleryGroup { get; set; }

        internal class Configuration : EntityTypeConfiguration<SiteGallery>
        {
            public Configuration()
            {
                HasRequired(p => p.SiteGalleryGroup)
                    .WithMany(j => j.SiteGalleries)
                    .HasForeignKey(p => p.SiteGalleryGroupId);
            }
        }
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
                    //case "ar-ae":
                        return this.TitleAr;
                    default:
                        return String.Empty;
                }
            }
        }
    }
}