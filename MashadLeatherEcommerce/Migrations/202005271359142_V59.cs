namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V59 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SiteGalleries", "Order", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SiteGalleries", "Order");
        }
    }
}
