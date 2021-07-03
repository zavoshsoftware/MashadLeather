using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Models
{
    public class DatabaseContext : DbContext
    {
        static DatabaseContext()
        {
           System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Migrations.Configuration>());
        }

        public DbSet<Role> Roles { get; set; }

        public DbSet<City> Cities { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<KiyanProductCategory> KiyanProductCategories { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Color> Colors { get; set; }

        public System.Data.Entity.DbSet<Models.ProductImage> ProductImages { get; set; }

        public System.Data.Entity.DbSet<Models.PaymentUniqeCodes> PaymentUniqeCodes { get; set; }

        public System.Data.Entity.DbSet<Models.Payment> Payments { get; set; }
        public DbSet<KiyanLog> KiyanLogs { get; set; }
        public DbSet<UserProductLike> UserProductLike { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Text> Texts { get; set; }
        public DbSet<SecondColor> SecondColors { get; set; }
        public DbSet<StepDiscount> StepDiscounts { get; set; }
        public DbSet<StepDiscountDetail> StepDiscountDetails { get; set; }

        public System.Data.Entity.DbSet<Models.Blog> Blogs { get; set; }

        public System.Data.Entity.DbSet<Models.SiteSlider> SiteSliders { get; set; }

        public System.Data.Entity.DbSet<Models.TextType> TextTypes { get; set; }

        public System.Data.Entity.DbSet<Models.SiteGalleryGroup> SiteGalleryGroups { get; set; }

        public System.Data.Entity.DbSet<Models.SiteGallery> SiteGalleries { get; set; }
        public System.Data.Entity.DbSet<Models.BlogGroup> BlogGroups { get; set; }
        public System.Data.Entity.DbSet<Models.SiteBranchGroup> SiteBranchGroups { get; set; }
        public System.Data.Entity.DbSet<Models.SiteBranch> SiteBranches { get; set; }
        public DbSet<ResumeFile> ResumeFiles { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<DiscountCode> DiscountCodes { get; set; }
        public DbSet<PaymentFreeCode> PaymentFreeCodes { get; set; }
        public DbSet<UserInformation> UserInformations { get; set; }

        public DbSet<BlackListUser> BlackListUsers { get; set; }

        public System.Data.Entity.DbSet<Models.Carreer> Carreers { get; set; }

        public System.Data.Entity.DbSet<Models.CarreerEducationalCourse> CarreerEducationalCourses { get; set; }

        public System.Data.Entity.DbSet<Models.CarreerFamilyInformation> CarreerFamilyInformations { get; set; }

        public System.Data.Entity.DbSet<Models.CarreerIntroduced> CarreerIntroduceds { get; set; }

        public System.Data.Entity.DbSet<Models.CarreerPreviousExperience> CarreerPreviousExperiences { get; set; }

        public System.Data.Entity.DbSet<Models.CareerType> CareerTypes { get; set; }
    }

}
