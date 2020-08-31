namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V51 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SiteGalleries", "UrlParam", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SiteGalleries", "UrlParam");
        }
    }
}
