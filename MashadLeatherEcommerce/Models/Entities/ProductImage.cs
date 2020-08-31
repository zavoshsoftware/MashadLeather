using Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Models
{
    public class ProductImage : BaseEntity
    {
        [Display(Name = "Title", ResourceType = typeof(Resources.Models.Product))]
        public string Title { get; set; }
        [Display(Name = "TitleEn", ResourceType = typeof(Resources.Models.Product))]
        public string TitleEn { get; set; }

        [Display(Name = "عنوان عربی")]
        public string TitleAr { get; set; }

        [Display(Name = "Priority", ResourceType = typeof(Resources.Models.Product))]
        public int Priority { get; set; }

        [Display(Name = "ImageUrl", ResourceType = typeof(Resources.Models.Product))]
        [MaxLength(200)]
        public string ImageUrl { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        internal class Configuration : EntityTypeConfiguration<ProductImage>
        {
            public Configuration()
            {
                HasOptional(p => p.Product)
                    .WithMany(j => j.ProductImages)
                    .HasForeignKey(p => p.ProductId);
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
                    case "ar-ae":
                        return this.TitleAr;
                    default:
                        return String.Empty;
                }
            }
        }
    }
}