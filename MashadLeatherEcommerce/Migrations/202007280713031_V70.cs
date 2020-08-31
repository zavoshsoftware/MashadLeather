namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V70 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogGroups", "DescriptionAr", c => c.String());
            AddColumn("dbo.Blogs", "DescriptionAr", c => c.String());
            AddColumn("dbo.Cities", "DescriptionAr", c => c.String());
            AddColumn("dbo.Orders", "DescriptionAr", c => c.String());
            AddColumn("dbo.OrderDetails", "DescriptionAr", c => c.String());
            AddColumn("dbo.Products", "DescriptionAr", c => c.String());
            AddColumn("dbo.Colors", "DescriptionAr", c => c.String());
            AddColumn("dbo.ProductCategories", "DescriptionAr", c => c.String());
            AddColumn("dbo.Comments", "DescriptionAr", c => c.String());
            AddColumn("dbo.ProductImages", "DescriptionAr", c => c.String());
            AddColumn("dbo.SecondColors", "DescriptionAr", c => c.String());
            AddColumn("dbo.Sizes", "DescriptionAr", c => c.String());
            AddColumn("dbo.UserProductLikes", "DescriptionAr", c => c.String());
            AddColumn("dbo.Users", "DescriptionAr", c => c.String());
            AddColumn("dbo.Roles", "DescriptionAr", c => c.String());
            AddColumn("dbo.OrderStatus", "DescriptionAr", c => c.String());
            AddColumn("dbo.Payments", "DescriptionAr", c => c.String());
            AddColumn("dbo.Provinces", "DescriptionAr", c => c.String());
            AddColumn("dbo.KiyanProductCategories", "DescriptionAr", c => c.String());
            AddColumn("dbo.SiteBranches", "DescriptionAr", c => c.String());
            AddColumn("dbo.SiteBranchGroups", "DescriptionAr", c => c.String());
            AddColumn("dbo.SiteGalleries", "DescriptionAr", c => c.String());
            AddColumn("dbo.SiteGalleryGroups", "DescriptionAr", c => c.String());
            AddColumn("dbo.SiteSliders", "DescriptionAr", c => c.String());
            AddColumn("dbo.StepDiscountDetails", "DescriptionAr", c => c.String());
            AddColumn("dbo.StepDiscounts", "DescriptionAr", c => c.String());
            AddColumn("dbo.Texts", "DescriptionAr", c => c.String());
            AddColumn("dbo.TextTypes", "DescriptionAr", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TextTypes", "DescriptionAr");
            DropColumn("dbo.Texts", "DescriptionAr");
            DropColumn("dbo.StepDiscounts", "DescriptionAr");
            DropColumn("dbo.StepDiscountDetails", "DescriptionAr");
            DropColumn("dbo.SiteSliders", "DescriptionAr");
            DropColumn("dbo.SiteGalleryGroups", "DescriptionAr");
            DropColumn("dbo.SiteGalleries", "DescriptionAr");
            DropColumn("dbo.SiteBranchGroups", "DescriptionAr");
            DropColumn("dbo.SiteBranches", "DescriptionAr");
            DropColumn("dbo.KiyanProductCategories", "DescriptionAr");
            DropColumn("dbo.Provinces", "DescriptionAr");
            DropColumn("dbo.Payments", "DescriptionAr");
            DropColumn("dbo.OrderStatus", "DescriptionAr");
            DropColumn("dbo.Roles", "DescriptionAr");
            DropColumn("dbo.Users", "DescriptionAr");
            DropColumn("dbo.UserProductLikes", "DescriptionAr");
            DropColumn("dbo.Sizes", "DescriptionAr");
            DropColumn("dbo.SecondColors", "DescriptionAr");
            DropColumn("dbo.ProductImages", "DescriptionAr");
            DropColumn("dbo.Comments", "DescriptionAr");
            DropColumn("dbo.ProductCategories", "DescriptionAr");
            DropColumn("dbo.Colors", "DescriptionAr");
            DropColumn("dbo.Products", "DescriptionAr");
            DropColumn("dbo.OrderDetails", "DescriptionAr");
            DropColumn("dbo.Orders", "DescriptionAr");
            DropColumn("dbo.Cities", "DescriptionAr");
            DropColumn("dbo.Blogs", "DescriptionAr");
            DropColumn("dbo.BlogGroups", "DescriptionAr");
        }
    }
}
