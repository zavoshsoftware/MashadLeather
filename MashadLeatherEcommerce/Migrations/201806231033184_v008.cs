namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v008 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Serial", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Serial");
        }
    }
}
