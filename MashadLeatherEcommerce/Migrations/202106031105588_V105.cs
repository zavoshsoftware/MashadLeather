namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V105 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlackListUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CellNumber = c.String(),
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
            DropTable("dbo.BlackListUsers");
        }
    }
}
