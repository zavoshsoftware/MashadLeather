namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v110 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carreers", "SportHistory", c => c.String());
            AddColumn("dbo.Carreers", "ConfirmedDescription", c => c.String());
            //DropColumn("dbo.Carreers", "ResumeFile");
            DropColumn("dbo.Carreers", "MilitaryStatus");
            DropColumn("dbo.Carreers", "PhysicalCondition");
            DropColumn("dbo.Carreers", "Education");
            DropColumn("dbo.Carreers", "Familiar");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carreers", "Familiar", c => c.Int(nullable: false));
            AddColumn("dbo.Carreers", "Education", c => c.Int(nullable: false));
            AddColumn("dbo.Carreers", "PhysicalCondition", c => c.Int(nullable: false));
            AddColumn("dbo.Carreers", "MilitaryStatus", c => c.Int(nullable: false));
            //AddColumn("dbo.Carreers", "ResumeFile", c => c.String());
            DropColumn("dbo.Carreers", "ConfirmedDescription");
            DropColumn("dbo.Carreers", "SportHistory");
        }
    }
}
