namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V50 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SiteGalleries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        ImageUrl = c.String(),
                        FileUrl = c.String(),
                        FileAddress = c.String(),
                        ImageUrlThumb = c.String(),
                        SiteGalleryGroupId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                        DescriptionEn = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SiteGalleryGroups", t => t.SiteGalleryGroupId, cascadeDelete: true)
                .Index(t => t.SiteGalleryGroupId);
            
            CreateTable(
                "dbo.SiteGalleryGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        UrlParam = c.String(),
                        Body = c.String(storeType: "ntext"),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                        DescriptionEn = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SiteGalleries", "SiteGalleryGroupId", "dbo.SiteGalleryGroups");
            DropIndex("dbo.SiteGalleries", new[] { "SiteGalleryGroupId" });
            DropTable("dbo.SiteGalleryGroups");
            DropTable("dbo.SiteGalleries");
        }
    }
}
