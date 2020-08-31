using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper;

namespace Models
{
    public class ProductCategory : BaseEntity
    {
        public ProductCategory()
        {
            Products = new List<Product>();
            ProductCategories = new List<ProductCategory>();
            Comments = new List<Comment>();
        }

        [Display(Name = "Title", ResourceType = typeof(Resources.Models.ProductCategory))]
        public string Title { get; set; }
        [Display(Name = "TitleEn", ResourceType = typeof(Resources.Models.ProductCategory))]
        public string TitleEn { get; set; }

        [Display(Name = "عنوان عربی")]
        public string TitleAr { get; set; }

        [Display(Name = "Priority", ResourceType = typeof(Resources.Models.ProductCategory))]
        public int Priority { get; set; }

        [Display(Name = "ImageUrl", ResourceType = typeof(Resources.Models.ProductCategory))]
        public string ImageUrl { get; set; }

        [Display(Name = "SlideImageUrl", ResourceType = typeof(Resources.Models.ProductCategory))]
        public string SlideImageUrl { get; set; }

        [Display(Name = "Size", ResourceType = typeof(Resources.Models.ProductCategory))]
        public int? Size { get; set; }

        public Guid? ParentId { get; set; }

        public virtual ProductCategory Parent { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        [Display(Name = "پارامتر url")]
        public string UrlParam { get; set; }

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
