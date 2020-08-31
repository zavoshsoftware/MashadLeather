namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v19 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KiyanLogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LogDate = c.DateTime(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.KiyanLogs");
        }
    }
}
