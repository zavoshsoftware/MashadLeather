namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V100 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "SendFactor", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "SendFactor");
        }
    }
}
