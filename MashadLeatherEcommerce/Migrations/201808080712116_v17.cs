namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v17 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentLogs", "ResCodeRequest", c => c.Int());
            AddColumn("dbo.PaymentLogs", "ResCodePayment", c => c.Int());
            AddColumn("dbo.PaymentLogs", "ResCodeVerify", c => c.Int());
            AddColumn("dbo.PaymentLogs", "ResCodeSettle", c => c.Int());
            DropColumn("dbo.PaymentLogs", "ResCode_Request");
            DropColumn("dbo.PaymentLogs", "ResCode_Payment");
            DropColumn("dbo.PaymentLogs", "ResCode_Verify");
            DropColumn("dbo.PaymentLogs", "ResCode_Settle");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PaymentLogs", "ResCode_Settle", c => c.Int());
            AddColumn("dbo.PaymentLogs", "ResCode_Verify", c => c.Int());
            AddColumn("dbo.PaymentLogs", "ResCode_Payment", c => c.Int());
            AddColumn("dbo.PaymentLogs", "ResCode_Request", c => c.Int());
            DropColumn("dbo.PaymentLogs", "ResCodeSettle");
            DropColumn("dbo.PaymentLogs", "ResCodeVerify");
            DropColumn("dbo.PaymentLogs", "ResCodePayment");
            DropColumn("dbo.PaymentLogs", "ResCodeRequest");
        }
    }
}
