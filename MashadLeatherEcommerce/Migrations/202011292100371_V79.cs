namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V79 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DiscountCodes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(nullable: false, maxLength: 10),
                        ExpireDate = c.DateTime(nullable: false),
                        IsPercent = c.Boolean(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsMultiUsing = c.Boolean(nullable: false),
                        UserId = c.Guid(),
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
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Orders", "DiscountCodeId", c => c.Guid());
            CreateIndex("dbo.Orders", "DiscountCodeId");
            AddForeignKey("dbo.Orders", "DiscountCodeId", "dbo.DiscountCodes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DiscountCodes", "UserId", "dbo.Users");
            DropForeignKey("dbo.Orders", "DiscountCodeId", "dbo.DiscountCodes");
            DropIndex("dbo.DiscountCodes", new[] { "UserId" });
            DropIndex("dbo.Orders", new[] { "DiscountCodeId" });
            DropColumn("dbo.Orders", "DiscountCodeId");
            DropTable("dbo.DiscountCodes");
        }
    }
}
