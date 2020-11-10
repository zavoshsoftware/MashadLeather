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
    public class Product : BaseEntity
    {

        public Product()
        {
            OrderDetails = new List<OrderDetail>();
            Products = new List<Product>();
            ProductImages = new List<ProductImage>();
            UserProductLike = new List<UserProductLike>();
        }

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

        [Display(Name = "Amount", ResourceType = typeof(Resources.Models.Product))]
        [Column(TypeName = "Money")]
        public decimal? Amount { get; set; }

        [Display(Name = "DiscountAmount", ResourceType = typeof(Resources.Models.Product))]
        [Column(TypeName = "Money")]
        public decimal? DiscountAmount { get; set; }
        [Display(Name = "Taxable", ResourceType = typeof(Resources.Models.Product))]
        public bool Taxable { get; set; }

        [Display(Name = "Barcode", ResourceType = typeof(Resources.Models.Product))]
        public string Barcode { get; set; }

        [Display(Name = "KiyanName", ResourceType = typeof(Resources.Models.Product))]
        public string KiyanName { get; set; }

        [Display(Name = "Quantity", ResourceType = typeof(Resources.Models.Product))]
        public decimal Quantity { get; set; }


        [Display(Name = "KiyanId", ResourceType = typeof(Resources.Models.Product))]
        public int KiyanId { get; set; }
        [Display(Name = "KiyanCreateDate", ResourceType = typeof(Resources.Models.Product))]
        public string KiyanCreateDate { get; set; }
        public string Serial { get; set; }
        public Guid? ParentId { get; set; }
        public bool? IsChanged { get; set; }
        public Guid? ProductCategoryId { get; set; }
        public Guid? SizeId { get; set; }
        public Guid? ColorId { get; set; }
        public Guid? SecondColorId { get; set; }
        [Display(Name = "پروموشن فعال")]
        public bool IsInPromotion { get; set; }
        [Display(Name = "KiyanCreateDate", ResourceType = typeof(Resources.Models.Product))]
        public decimal? DecreaseAmount { get; set; }
        [Display(Name = "Code", ResourceType = typeof(Resources.Models.Product))]
        public int Code { get; set; }
        public virtual SecondColor SecondColor { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual Product Parent { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual Color Color { get; set; }
        public virtual Size Size { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<UserProductLike> UserProductLike { get; set; }


        internal class Configuration : EntityTypeConfiguration<Product>
        {
            public Configuration()
            {
                HasOptional(p => p.ProductCategory)
                    .WithMany(j => j.Products)
                    .HasForeignKey(p => p.ProductCategoryId);

                HasOptional(p => p.Size)
                    .WithMany(j => j.Products)
                    .HasForeignKey(p => p.SizeId);

                HasOptional(p => p.Color)
                    .WithMany(j => j.Products)
                    .HasForeignKey(p => p.ColorId);

                HasOptional(p => p.SecondColor)
                   .WithMany(j => j.Products)
                   .HasForeignKey(p => p.SecondColorId);
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
        [Display(Name="در صفحه اصلی باشد؟")]
        public bool IsInHome { get; set; }

        [Display(Name="محصول پرفروش باشد؟")]
        public bool IsMostSale { get; set; }

        [Display(Name="تگ داشته باشد؟")]
        public bool HasTag { get; set; }

        [Display(Name="عنوان تگ")]
        public string TagTitle { get; set; }
        [Display(Name="عنوان تگ انگلیسی")]
        public string TagTitleEn { get; set; }
        [Display(Name = "عنوان تگ عربی")]
        public string TagTitleAr { get; set; }

        public bool IsAvailable { get; set; }

        [NotMapped]
        public string TagTitleSrt
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.TagTitleEn;
                    case "fa-ir":
                        return this.TagTitle;
                    case "ar-ae":
                        return this.TagTitleAr;
                    default:
                        return String.Empty;
                }
            }
        }
        GetCurrency oGetCurrency = new GetCurrency();
        [NotMapped]
        public decimal? AmountSrt
        {
            get
            {
                string currentCurrency = oGetCurrency.CurrentCurrency();
                
                switch (currentCurrency.ToLower())
                {
                    case "none":
                        return this.Amount;
                    case "toman":
                        return this.Amount;
                    case "euro":
                        {
                            return this.Amount / oGetCurrency.CurrentEuroPrice();
                        }
                    default:
                        return this.Amount;
                }
            }
        }

        [NotMapped]
        public decimal? DiscountAmountSrt
        {
            get
            {
                string currentCurrency = oGetCurrency.CurrentCurrency();

                switch (currentCurrency.ToLower())
                {
                    case "none":
                        return this.DiscountAmount;
                    case "toman":
                        return this.DiscountAmount;
                    case "euro":
                        {
                            return this.DiscountAmount / oGetCurrency.CurrentEuroPrice();
                        }
                    default:
                        return this.Amount;
                }
            }
        }
    }
}
