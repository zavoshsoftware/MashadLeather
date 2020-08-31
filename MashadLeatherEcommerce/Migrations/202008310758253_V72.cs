namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V72 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SiteSliders", "ImageUrlEn", c => c.String());
            AddColumn("dbo.SiteSliders", "ImageUrlAr", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SiteSliders", "ImageUrlAr");
            DropColumn("dbo.SiteSliders", "ImageUrlEn");
        }
    }
}
