namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v007 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Code", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Code", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
