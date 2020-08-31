namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V62 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SiteGalleryGroups", "TitleEn", c => c.String());
            DropColumn("dbo.SiteGalleries", "TitleEn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SiteGalleries", "TitleEn", c => c.String());
            DropColumn("dbo.SiteGalleryGroups", "TitleEn");
        }
    }
}
