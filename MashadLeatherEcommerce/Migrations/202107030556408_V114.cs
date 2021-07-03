namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V114 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CareerTypes",
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
                        DescriptionAr = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Carreers", "CareerTypeId", c => c.Guid());
            CreateIndex("dbo.Carreers", "CareerTypeId");
            AddForeignKey("dbo.Carreers", "CareerTypeId", "dbo.CareerTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carreers", "CareerTypeId", "dbo.CareerTypes");
            DropIndex("dbo.Carreers", new[] { "CareerTypeId" });
            DropColumn("dbo.Carreers", "CareerTypeId");
            DropTable("dbo.CareerTypes");
        }
    }
}
