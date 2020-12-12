namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V87 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DiscountCodes", "Code", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DiscountCodes", "Code", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
