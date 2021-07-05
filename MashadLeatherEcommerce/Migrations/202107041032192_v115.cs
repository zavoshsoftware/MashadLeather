namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v115 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Visit", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Visit");
        }
    }
}
