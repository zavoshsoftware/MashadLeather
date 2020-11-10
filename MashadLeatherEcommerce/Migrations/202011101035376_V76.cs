namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V76 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "IsAvailable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "IsAvailable");
        }
    }
}
