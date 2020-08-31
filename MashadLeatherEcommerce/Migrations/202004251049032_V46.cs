namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V46 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TextTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                        DescriptionEn = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Texts", "TextTypeId", c => c.Guid());
            CreateIndex("dbo.Texts", "TextTypeId");
            AddForeignKey("dbo.Texts", "TextTypeId", "dbo.TextTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Texts", "TextTypeId", "dbo.TextTypes");
            DropIndex("dbo.Texts", new[] { "TextTypeId" });
            DropColumn("dbo.Texts", "TextTypeId");
            DropTable("dbo.TextTypes");
        }
    }
}
