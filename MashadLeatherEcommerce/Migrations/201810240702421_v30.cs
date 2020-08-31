namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v30 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SecondColors",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Code = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                        DescriptionEn = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Products", "SecondColorId", c => c.Guid());
            CreateIndex("dbo.Products", "SecondColorId");
            AddForeignKey("dbo.Products", "SecondColorId", "dbo.SecondColors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "SecondColorId", "dbo.SecondColors");
            DropIndex("dbo.Products", new[] { "SecondColorId" });
            DropColumn("dbo.Products", "SecondColorId");
            DropTable("dbo.SecondColors");
        }
    }
}
