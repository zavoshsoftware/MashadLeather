namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V83 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DiscountCodes", "IsUsed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DiscountCodes", "IsUsed");
        }
    }
}
