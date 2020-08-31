namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v006 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ItmId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ItmId");
        }
    }
}
