namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v42 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "UrlParam", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blogs", "UrlParam");
        }
    }
}
