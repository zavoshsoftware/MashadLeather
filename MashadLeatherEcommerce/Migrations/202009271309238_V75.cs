namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V75 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Configurations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Key = c.String(),
                        Value = c.String(),
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
            DropTable("dbo.Configurations");
        }
    }
}
