namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v29 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cities", "DescriptionEn", c => c.String());
            AddColumn("dbo.Orders", "DescriptionEn", c => c.String());
            AddColumn("dbo.OrderDetails", "DescriptionEn", c => c.String());
            AddColumn("dbo.Products", "TitleEn", c => c.String());
            AddColumn("dbo.Products", "DescriptionEn", c => c.String());
            AddColumn("dbo.Colors", "TitleEn", c => c.String());
            AddColumn("dbo.Colors", "DescriptionEn", c => c.String());
            AddColumn("dbo.ProductCategories", "TitleEn", c => c.String());
            AddColumn("dbo.ProductCategories", "DescriptionEn", c => c.String());
            AddColumn("dbo.Comments", "DescriptionEn", c => c.String());
            AddColumn("dbo.ProductCategoryMappers", "DescriptionEn", c => c.String());
            AddColumn("dbo.KiyanProductCategories", "DescriptionEn", c => c.String());
            AddColumn("dbo.ProductImages", "TitleEn", c => c.String());
            AddColumn("dbo.ProductImages", "DescriptionEn", c => c.String());
            AddColumn("dbo.Sizes", "DescriptionEn", c => c.String());
            AddColumn("dbo.UserProductLikes", "DescriptionEn", c => c.String());
            AddColumn("dbo.Users", "DescriptionEn", c => c.String());
            AddColumn("dbo.Roles", "DescriptionEn", c => c.String());
            AddColumn("dbo.OrderStatus", "DescriptionEn", c => c.String());
            AddColumn("dbo.Payments", "DescriptionEn", c => c.String());
            AddColumn("dbo.Provinces", "DescriptionEn", c => c.String());
            AddColumn("dbo.Texts", "TitleEn", c => c.String());
            AddColumn("dbo.Texts", "BodyEn", c => c.String(storeType: "ntext"));
            AddColumn("dbo.Texts", "DescriptionEn", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Texts", "DescriptionEn");
            DropColumn("dbo.Texts", "BodyEn");
            DropColumn("dbo.Texts", "TitleEn");
            DropColumn("dbo.Provinces", "DescriptionEn");
            DropColumn("dbo.Payments", "DescriptionEn");
            DropColumn("dbo.OrderStatus", "DescriptionEn");
            DropColumn("dbo.Roles", "DescriptionEn");
            DropColumn("dbo.Users", "DescriptionEn");
            DropColumn("dbo.UserProductLikes", "DescriptionEn");
            DropColumn("dbo.Sizes", "DescriptionEn");
            DropColumn("dbo.ProductImages", "DescriptionEn");
            DropColumn("dbo.ProductImages", "TitleEn");
            DropColumn("dbo.KiyanProductCategories", "DescriptionEn");
            DropColumn("dbo.ProductCategoryMappers", "DescriptionEn");
            DropColumn("dbo.Comments", "DescriptionEn");
            DropColumn("dbo.ProductCategories", "DescriptionEn");
            DropColumn("dbo.ProductCategories", "TitleEn");
            DropColumn("dbo.Colors", "DescriptionEn");
            DropColumn("dbo.Colors", "TitleEn");
            DropColumn("dbo.Products", "DescriptionEn");
            DropColumn("dbo.Products", "TitleEn");
            DropColumn("dbo.OrderDetails", "DescriptionEn");
            DropColumn("dbo.Orders", "DescriptionEn");
            DropColumn("dbo.Cities", "DescriptionEn");
        }
    }
}
