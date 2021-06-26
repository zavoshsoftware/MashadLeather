namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V112 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carreers", "GenderTitle", c => c.String());
            AddColumn("dbo.Carreers", "MarriedStatus", c => c.String());
            DropColumn("dbo.Carreers", "IsLady");
            DropColumn("dbo.Carreers", "IsMarried");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carreers", "IsMarried", c => c.Boolean(nullable: false));
            AddColumn("dbo.Carreers", "IsLady", c => c.Boolean(nullable: false));
            DropColumn("dbo.Carreers", "MarriedStatus");
            DropColumn("dbo.Carreers", "GenderTitle");
        }
    }
}
