namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v44 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductCategories", "UrlParam", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductCategories", "UrlParam");
        }
    }
}
