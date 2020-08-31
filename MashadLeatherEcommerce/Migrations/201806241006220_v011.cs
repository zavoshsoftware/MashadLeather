namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v011 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductColors", "ColorId", "dbo.Colors");
            DropForeignKey("dbo.ProductColors", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductSizes", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductSizes", "SizeId", "dbo.Sizes");
            DropIndex("dbo.ProductColors", new[] { "ProductId" });
            DropIndex("dbo.ProductColors", new[] { "ColorId" });
            DropIndex("dbo.ProductSizes", new[] { "ProductId" });
            DropIndex("dbo.ProductSizes", new[] { "SizeId" });
            AddColumn("dbo.Products", "ParentId", c => c.Guid());
            AddColumn("dbo.Products", "SizeId", c => c.Guid());
            AddColumn("dbo.Products", "ColorId", c => c.Guid());
            CreateIndex("dbo.Products", "ParentId");
            CreateIndex("dbo.Products", "SizeId");
            CreateIndex("dbo.Products", "ColorId");
            AddForeignKey("dbo.Products", "ColorId", "dbo.Colors", "Id");
            AddForeignKey("dbo.Products", "ParentId", "dbo.Products", "Id");
            AddForeignKey("dbo.Products", "SizeId", "dbo.Sizes", "Id");
            DropColumn("dbo.Products", "Size");
            DropTable("dbo.ProductColors");
            DropTable("dbo.ProductSizes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductSizes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        SizeId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductColors",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        ColorId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Products", "Size", c => c.String(maxLength: 50));
            DropForeignKey("dbo.Products", "SizeId", "dbo.Sizes");
            DropForeignKey("dbo.Products", "ParentId", "dbo.Products");
            DropForeignKey("dbo.Products", "ColorId", "dbo.Colors");
            DropIndex("dbo.Products", new[] { "ColorId" });
            DropIndex("dbo.Products", new[] { "SizeId" });
            DropIndex("dbo.Products", new[] { "ParentId" });
            DropColumn("dbo.Products", "ColorId");
            DropColumn("dbo.Products", "SizeId");
            DropColumn("dbo.Products", "ParentId");
            CreateIndex("dbo.ProductSizes", "SizeId");
            CreateIndex("dbo.ProductSizes", "ProductId");
            CreateIndex("dbo.ProductColors", "ColorId");
            CreateIndex("dbo.ProductColors", "ProductId");
            AddForeignKey("dbo.ProductSizes", "SizeId", "dbo.Sizes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductSizes", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductColors", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductColors", "ColorId", "dbo.Colors", "Id", cascadeDelete: true);
        }
    }
}
