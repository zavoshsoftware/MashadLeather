namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "IsChanged", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "IsChanged");
        }
    }
}
