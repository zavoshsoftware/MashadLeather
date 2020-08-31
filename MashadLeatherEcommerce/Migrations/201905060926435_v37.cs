namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v37 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StepDiscountDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StepDiscountId = c.Guid(nullable: false),
                        TargetValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountPercent = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                        DescriptionEn = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StepDiscounts", t => t.StepDiscountId, cascadeDelete: true)
                .Index(t => t.StepDiscountId);
            
            CreateTable(
                "dbo.StepDiscounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        ExpireDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                        DescriptionEn = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StepDiscountDetails", "StepDiscountId", "dbo.StepDiscounts");
            DropIndex("dbo.StepDiscountDetails", new[] { "StepDiscountId" });
            DropTable("dbo.StepDiscounts");
            DropTable("dbo.StepDiscountDetails");
        }
    }
}
