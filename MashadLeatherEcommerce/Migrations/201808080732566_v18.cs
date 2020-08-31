namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v18 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PaymentLogs", "OrderId", "dbo.Orders");
            DropIndex("dbo.PaymentLogs", new[] { "OrderId" });
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ReferenceNumber = c.String(),
                        SaleReferenceId = c.Long(nullable: false),
                        PaymentStatus = c.String(),
                        IsSuccess = c.Boolean(nullable: false),
                        Amount = c.String(),
                        OrderId = c.Guid(nullable: false),
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
            
            DropTable("dbo.PaymentLogs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PaymentLogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrderId = c.Guid(nullable: false),
                        RefId = c.String(maxLength: 50),
                        SaleReferenceId = c.Long(),
                        ResCodeRequest = c.Int(),
                        ResCodePayment = c.Int(),
                        ResCodeVerify = c.Int(),
                        ResCodeSettle = c.Int(),
                        ErrorMessage = c.String(),
                        IsSuccess = c.Boolean(nullable: false),
                        PaymentIp = c.String(maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Payments", "OrderId", "dbo.Orders");
            DropIndex("dbo.Payments", new[] { "OrderId" });
            DropTable("dbo.Payments");
            CreateIndex("dbo.PaymentLogs", "OrderId");
            AddForeignKey("dbo.PaymentLogs", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
        }
    }
}
