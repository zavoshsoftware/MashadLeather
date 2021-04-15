namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V89 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DiscountCodes", "AvailableInPromotion", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DiscountCodes", "AvailableInPromotion");
        }
    }
}
