namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v108 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Carreers", "ResumeFile", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Carreers", "ResumeFile", c => c.String(nullable: false));
        }
    }
}
