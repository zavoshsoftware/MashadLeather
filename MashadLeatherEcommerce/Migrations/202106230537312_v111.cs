namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v111 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carreers", "MilitaryStatus", c => c.String());
            AddColumn("dbo.Carreers", "PhysicalCondition", c => c.String());
            AddColumn("dbo.Carreers", "Education", c => c.String(nullable: false));
            AddColumn("dbo.Carreers", "Familiar", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Carreers", "Familiar");
            DropColumn("dbo.Carreers", "Education");
            DropColumn("dbo.Carreers", "PhysicalCondition");
            DropColumn("dbo.Carreers", "MilitaryStatus");
        }
    }
}
