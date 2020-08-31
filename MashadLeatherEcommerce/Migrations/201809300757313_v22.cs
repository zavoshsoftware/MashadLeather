namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v22 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProductLikes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProductLikes", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserProductLikes", "ProductId", "dbo.Products");
            DropIndex("dbo.UserProductLikes", new[] { "ProductId" });
            DropIndex("dbo.UserProductLikes", new[] { "UserId" });
            DropTable("dbo.UserProductLikes");
        }
    }
}
