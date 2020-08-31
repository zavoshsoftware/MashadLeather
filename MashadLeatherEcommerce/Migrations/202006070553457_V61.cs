namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V61 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SiteGalleries", "TitleEn", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SiteGalleries", "TitleEn");
        }
    }
}
