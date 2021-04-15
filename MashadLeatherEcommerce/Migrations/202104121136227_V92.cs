namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V92 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KiyanLogs", "FileUrl", c => c.String());
            AddColumn("dbo.KiyanLogs", "IsSuccess", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.KiyanLogs", "IsSuccess");
            DropColumn("dbo.KiyanLogs", "FileUrl");
        }
    }
}
