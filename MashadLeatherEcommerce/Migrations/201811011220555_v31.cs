namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v31 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductCategoryMappers", "KiyanProductCategoryId", "dbo.KiyanProductCategories");
            DropForeignKey("dbo.ProductCategoryMappers", "ProductCategoryId", "dbo.ProductCategories");
            DropIndex("dbo.ProductCategoryMappers", new[] { "ProductCategoryId" });
            DropIndex("dbo.ProductCategoryMappers", new[] { "KiyanProductCategoryId" });
            AddColumn("dbo.SecondColors", "TitleEn", c => c.String());
            DropTable("dbo.ProductCategoryMappers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductCategoryMappers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductCategoryId = c.Guid(nullable: false),
                        KiyanProductCategoryId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                        DescriptionEn = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.SecondColors", "TitleEn");
            CreateIndex("dbo.ProductCategoryMappers", "KiyanProductCategoryId");
            CreateIndex("dbo.ProductCategoryMappers", "ProductCategoryId");
            AddForeignKey("dbo.ProductCategoryMappers", "ProductCategoryId", "dbo.ProductCategories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductCategoryMappers", "KiyanProductCategoryId", "dbo.KiyanProductCategories", "Id", cascadeDelete: true);
        }
    }
}
