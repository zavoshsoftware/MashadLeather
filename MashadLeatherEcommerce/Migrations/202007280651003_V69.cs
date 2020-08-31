namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V69 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogGroups", "TitleAr", c => c.String());
            AddColumn("dbo.Blogs", "TitleAr", c => c.String());
            AddColumn("dbo.Blogs", "SummeryAr", c => c.String());
            AddColumn("dbo.Blogs", "BodyAr", c => c.String(storeType: "ntext"));
            AddColumn("dbo.Products", "TitleAr", c => c.String());
            AddColumn("dbo.Products", "TagTitleAr", c => c.String());
            AddColumn("dbo.Colors", "TitleAr", c => c.String());
            AddColumn("dbo.ProductCategories", "TitleAr", c => c.String());
            AddColumn("dbo.ProductImages", "TitleAr", c => c.String());
            AddColumn("dbo.SecondColors", "TitleAr", c => c.String());
            AddColumn("dbo.SiteBranches", "TitleAr", c => c.String());
            AddColumn("dbo.SiteBranches", "AddressAr", c => c.String());
            AddColumn("dbo.SiteBranchGroups", "TitleAr", c => c.String());
            AddColumn("dbo.SiteGalleries", "TitleAr", c => c.String());
            AddColumn("dbo.SiteGalleryGroups", "TitleAr", c => c.String());
            AddColumn("dbo.Texts", "TitleAr", c => c.String());
            AddColumn("dbo.Texts", "BodyAr", c => c.String(storeType: "ntext"));
            AddColumn("dbo.Texts", "SummeryAr", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Texts", "SummeryAr");
            DropColumn("dbo.Texts", "BodyAr");
            DropColumn("dbo.Texts", "TitleAr");
            DropColumn("dbo.SiteGalleryGroups", "TitleAr");
            DropColumn("dbo.SiteGalleries", "TitleAr");
            DropColumn("dbo.SiteBranchGroups", "TitleAr");
            DropColumn("dbo.SiteBranches", "AddressAr");
            DropColumn("dbo.SiteBranches", "TitleAr");
            DropColumn("dbo.SecondColors", "TitleAr");
            DropColumn("dbo.ProductImages", "TitleAr");
            DropColumn("dbo.ProductCategories", "TitleAr");
            DropColumn("dbo.Colors", "TitleAr");
            DropColumn("dbo.Products", "TagTitleAr");
            DropColumn("dbo.Products", "TitleAr");
            DropColumn("dbo.Blogs", "BodyAr");
            DropColumn("dbo.Blogs", "SummeryAr");
            DropColumn("dbo.Blogs", "TitleAr");
            DropColumn("dbo.BlogGroups", "TitleAr");
        }
    }
}
