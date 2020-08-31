namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v36 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "DecreaseAmount", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "DecreaseAmount");
        }
    }
}
