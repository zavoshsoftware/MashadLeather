namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v010 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sizes", "BarCodeProductGroup", c => c.String());
            DropColumn("dbo.Sizes", "CarCodeProductGroup");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sizes", "CarCodeProductGroup", c => c.String());
            DropColumn("dbo.Sizes", "BarCodeProductGroup");
        }
    }
}
