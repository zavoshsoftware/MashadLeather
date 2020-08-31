namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v005 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "ProductCategoryId", "dbo.ProductCategories");
            DropIndex("dbo.Products", new[] { "ProductCategoryId" });
            AddColumn("dbo.Products", "PosDepartmentId", c => c.Int());
            AddColumn("dbo.Products", "Taxable", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "UserPrice", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Products", "ItmBrcd", c => c.String());
            AddColumn("dbo.Products", "ItmCreateDate", c => c.String());
            AddColumn("dbo.Products", "ItmName", c => c.String());
            AddColumn("dbo.Products", "ItmPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "ItmQuantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "ItmRsGrpPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "ItmRsGrpTempPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "ItmTempPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Products", "Amount", c => c.Decimal(storeType: "money"));
            AlterColumn("dbo.Products", "ProductCategoryId", c => c.Guid());
            CreateIndex("dbo.Products", "ProductCategoryId");
            AddForeignKey("dbo.Products", "ProductCategoryId", "dbo.ProductCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ProductCategoryId", "dbo.ProductCategories");
            DropIndex("dbo.Products", new[] { "ProductCategoryId" });
            AlterColumn("dbo.Products", "ProductCategoryId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Products", "Amount", c => c.Decimal(nullable: false, storeType: "money"));
            DropColumn("dbo.Products", "ItmTempPrice");
            DropColumn("dbo.Products", "ItmRsGrpTempPrice");
            DropColumn("dbo.Products", "ItmRsGrpPrice");
            DropColumn("dbo.Products", "ItmQuantity");
            DropColumn("dbo.Products", "ItmPrice");
            DropColumn("dbo.Products", "ItmName");
            DropColumn("dbo.Products", "ItmCreateDate");
            DropColumn("dbo.Products", "ItmBrcd");
            DropColumn("dbo.Products", "UserPrice");
            DropColumn("dbo.Products", "Taxable");
            DropColumn("dbo.Products", "PosDepartmentId");
            CreateIndex("dbo.Products", "ProductCategoryId");
            AddForeignKey("dbo.Products", "ProductCategoryId", "dbo.ProductCategories", "Id", cascadeDelete: true);
        }
    }
}
