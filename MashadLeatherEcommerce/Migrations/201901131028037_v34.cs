namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v34 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductCategories", "SlideImageUrl", c => c.String());
            AddColumn("dbo.ProductCategories", "Size", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductCategories", "Size");
            DropColumn("dbo.ProductCategories", "SlideImageUrl");
        }
    }
}
