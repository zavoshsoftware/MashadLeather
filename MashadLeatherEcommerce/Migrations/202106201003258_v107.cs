namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v107 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Carreers", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Carreers", "Email", c => c.Int(nullable: false));
        }
    }
}
