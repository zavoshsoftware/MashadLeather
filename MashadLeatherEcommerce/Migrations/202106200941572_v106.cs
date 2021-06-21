namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v106 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carreers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FullName = c.String(nullable: false),
                        Email = c.Int(nullable: false),
                        CellNumber = c.String(nullable: false),
                        ResumeFile = c.String(nullable: false),
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Carreers");
        }
    }
}
