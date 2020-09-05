namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V73 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SiteSliders", "LandingPageEn", c => c.String());
            AddColumn("dbo.SiteSliders", "LandingPageAr", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SiteSliders", "LandingPageAr");
            DropColumn("dbo.SiteSliders", "LandingPageEn");
        }
    }
}
