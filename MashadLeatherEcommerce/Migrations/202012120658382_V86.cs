namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V86 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentFreeCodes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.Long(nullable: false),
                        OrderId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                        DescriptionEn = c.String(),
                        DescriptionAr = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaymentFreeCodes", "OrderId", "dbo.Orders");
            DropIndex("dbo.PaymentFreeCodes", new[] { "OrderId" });
            DropTable("dbo.PaymentFreeCodes");
        }
    }
}
