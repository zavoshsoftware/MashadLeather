namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V71 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SiteSliders", "TitleAr", c => c.String());
            AddColumn("dbo.SiteSliders", "LinkTitleAr", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SiteSliders", "LinkTitleAr");
            DropColumn("dbo.SiteSliders", "TitleAr");
        }
    }
}
