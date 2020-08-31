namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V45 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SiteSliders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Order = c.Int(nullable: false),
                        ImageUrl = c.String(),
                        Title = c.String(),
                        LinkTitle = c.String(),
                        LandingPage = c.String(),
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
            DropTable("dbo.SiteSliders");
        }
    }
}
