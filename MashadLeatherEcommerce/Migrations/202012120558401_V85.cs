namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V85 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "WalletAmount", c => c.Decimal(storeType: "money"));
            AddColumn("dbo.Orders", "PaymentAmount", c => c.Decimal(storeType: "money"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "PaymentAmount");
            DropColumn("dbo.Orders", "WalletAmount");
        }
    }
}
