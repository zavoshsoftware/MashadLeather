namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v16 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PaymentLogs", "SaleReferenceId", c => c.Long());
            AlterColumn("dbo.PaymentLogs", "ResCode_Request", c => c.Int());
            AlterColumn("dbo.PaymentLogs", "ResCode_Payment", c => c.Int());
            AlterColumn("dbo.PaymentLogs", "ResCode_Verify", c => c.Int());
            AlterColumn("dbo.PaymentLogs", "ResCode_Settle", c => c.Int());
            DropColumn("dbo.PaymentLogs", "PaymentDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PaymentLogs", "PaymentDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PaymentLogs", "ResCode_Settle", c => c.Int(nullable: false));
            AlterColumn("dbo.PaymentLogs", "ResCode_Verify", c => c.Int(nullable: false));
            AlterColumn("dbo.PaymentLogs", "ResCode_Payment", c => c.Int(nullable: false));
            AlterColumn("dbo.PaymentLogs", "ResCode_Request", c => c.Int(nullable: false));
            AlterColumn("dbo.PaymentLogs", "SaleReferenceId", c => c.Long(nullable: false));
        }
    }
}
