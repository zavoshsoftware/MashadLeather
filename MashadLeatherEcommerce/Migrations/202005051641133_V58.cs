namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V58 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "HasTag", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "TagTitle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "TagTitle");
            DropColumn("dbo.Products", "HasTag");
        }
    }
}
