namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V68 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "TagTitleEn", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "TagTitleEn");
        }
    }
}
