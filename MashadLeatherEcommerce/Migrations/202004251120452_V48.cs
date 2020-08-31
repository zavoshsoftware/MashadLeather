namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V48 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Texts", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Texts", "ImageUrl");
        }
    }
}
