namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V101 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "ShipmentType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "ShipmentType");
        }
    }
}
