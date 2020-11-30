namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V81 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "SubAmount", c => c.Decimal(storeType: "money"));
            AddColumn("dbo.Orders", "ShipmentAmount", c => c.Decimal(storeType: "money"));
            AddColumn("dbo.Orders", "DiscountAmount", c => c.Decimal(storeType: "money"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "DiscountAmount");
            DropColumn("dbo.Orders", "ShipmentAmount");
            DropColumn("dbo.Orders", "SubAmount");
        }
    }
}
