namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V52 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        UrlParam = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                        DescriptionEn = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SiteBranches",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Location = c.String(),
                        SiteBranchGroupId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                        DescriptionEn = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SiteBranchGroups", t => t.SiteBranchGroupId, cascadeDelete: true)
                .Index(t => t.SiteBranchGroupId);
            
            CreateTable(
                "dbo.SiteBranchGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                        DescriptionEn = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Blogs", "BlogGroupId", c => c.Guid());
            CreateIndex("dbo.Blogs", "BlogGroupId");
            AddForeignKey("dbo.Blogs", "BlogGroupId", "dbo.BlogGroups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SiteBranches", "SiteBranchGroupId", "dbo.SiteBranchGroups");
            DropForeignKey("dbo.Blogs", "BlogGroupId", "dbo.BlogGroups");
            DropIndex("dbo.SiteBranches", new[] { "SiteBranchGroupId" });
            DropIndex("dbo.Blogs", new[] { "BlogGroupId" });
            DropColumn("dbo.Blogs", "BlogGroupId");
            DropTable("dbo.SiteBranchGroups");
            DropTable("dbo.SiteBranches");
            DropTable("dbo.BlogGroups");
        }
    }
}
