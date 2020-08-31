namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V63 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SiteSliders", "TitleEn", c => c.String());
            AddColumn("dbo.SiteSliders", "LinkTitleEn", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SiteSliders", "LinkTitleEn");
            DropColumn("dbo.SiteSliders", "TitleEn");
        }
    }
}
