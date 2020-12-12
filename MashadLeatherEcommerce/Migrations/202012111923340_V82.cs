namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V82 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DiscountCodes", "MaxAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Users", "Amount", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.DiscountCodes", "IsPercent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DiscountCodes", "IsPercent", c => c.Boolean(nullable: false));
            DropColumn("dbo.Users", "Amount");
            DropColumn("dbo.DiscountCodes", "MaxAmount");
        }
    }
}
