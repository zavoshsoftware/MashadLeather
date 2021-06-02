namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V103 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CustomerClubGroup", c => c.String());
            AddColumn("dbo.Orders", "CustomerClubDiscountAmount", c => c.Decimal(storeType: "money"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "CustomerClubDiscountAmount");
            DropColumn("dbo.Orders", "CustomerClubGroup");
        }
    }
}
