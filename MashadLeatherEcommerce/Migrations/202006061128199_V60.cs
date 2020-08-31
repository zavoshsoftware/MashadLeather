namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V60 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Code", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Code");
        }
    }
}
