namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v15 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentLogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrderId = c.Guid(nullable: false),
                        RefID = c.String(maxLength: 50),
                        SaleReferenceId = c.Long(nullable: false),
                        ResCode_Request = c.Int(nullable: false),
                        ResCode_Payment = c.Int(nullable: false),
                        ResCode_Verify = c.Int(nullable: false),
                        ResCode_Settle = c.Int(nullable: false),
                        ErrorMessage = c.String(),
                        IsSuccess = c.Boolean(nullable: false),
                        PaymentDate = c.DateTime(nullable: false),
                        PaymentIP = c.String(maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.PaymentUniqeCodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaymentUniqeCodes", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.PaymentLogs", "OrderId", "dbo.Orders");
            DropIndex("dbo.PaymentUniqeCodes", new[] { "OrderId" });
            DropIndex("dbo.PaymentLogs", new[] { "OrderId" });
            DropTable("dbo.PaymentUniqeCodes");
            DropTable("dbo.PaymentLogs");
        }
    }
}
