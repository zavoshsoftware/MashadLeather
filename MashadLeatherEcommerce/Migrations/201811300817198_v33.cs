namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v33 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KiyanLogs", "InventoryId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.KiyanLogs", "InventoryId");
        }
    }
}
