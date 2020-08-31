namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V53 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SiteBranches", "Latitude", c => c.String());
            AddColumn("dbo.SiteBranches", "Longitude", c => c.String());
            DropColumn("dbo.SiteBranches", "Location");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SiteBranches", "Location", c => c.String());
            DropColumn("dbo.SiteBranches", "Longitude");
            DropColumn("dbo.SiteBranches", "Latitude");
        }
    }
}
