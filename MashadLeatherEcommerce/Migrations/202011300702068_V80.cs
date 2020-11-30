namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V80 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DiscountCodes", "IsPublic", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DiscountCodes", "IsPublic");
        }
    }
}
