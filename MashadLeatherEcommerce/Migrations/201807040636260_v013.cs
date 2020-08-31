namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v013 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "DiscountAmount", c => c.Decimal(storeType: "money"));
            AddColumn("dbo.Products", "Barcode", c => c.String());
            AddColumn("dbo.Products", "KiyanName", c => c.String());
            AddColumn("dbo.Products", "Quantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "KiyanId", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "KiyanCreateDate", c => c.String());
            DropColumn("dbo.Products", "PosDepartmentId");
            DropColumn("dbo.Products", "UserPrice");
            DropColumn("dbo.Products", "ItmBrcd");
            DropColumn("dbo.Products", "ItmCreateDate");
            DropColumn("dbo.Products", "ItmName");
            DropColumn("dbo.Products", "ItmPrice");
            DropColumn("dbo.Products", "ItmQuantity");
            DropColumn("dbo.Products", "ItmRsGrpPrice");
            DropColumn("dbo.Products", "ItmRsGrpTempPrice");
            DropColumn("dbo.Products", "ItmTempPrice");
            DropColumn("dbo.Products", "ItmId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ItmId", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "ItmTempPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "ItmRsGrpTempPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "ItmRsGrpPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "ItmQuantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "ItmPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "ItmName", c => c.String());
            AddColumn("dbo.Products", "ItmCreateDate", c => c.String());
            AddColumn("dbo.Products", "ItmBrcd", c => c.String());
            AddColumn("dbo.Products", "UserPrice", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Products", "PosDepartmentId", c => c.Int());
            DropColumn("dbo.Products", "KiyanCreateDate");
            DropColumn("dbo.Products", "KiyanId");
            DropColumn("dbo.Products", "Quantity");
            DropColumn("dbo.Products", "KiyanName");
            DropColumn("dbo.Products", "Barcode");
            DropColumn("dbo.Products", "DiscountAmount");
        }
    }
}
