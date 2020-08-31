namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v24 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Email = c.String(),
                        CommentBody = c.String(),
                        ParentId = c.Guid(),
                        ProductCategoryId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.ParentId)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategoryId, cascadeDelete: true)
                .Index(t => t.ParentId)
                .Index(t => t.ProductCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "ProductCategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.Comments", "ParentId", "dbo.Comments");
            DropIndex("dbo.Comments", new[] { "ProductCategoryId" });
            DropIndex("dbo.Comments", new[] { "ParentId" });
            DropTable("dbo.Comments");
        }
    }
}
