namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v38 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.StepDiscounts", "ExpireDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StepDiscounts", "ExpireDate", c => c.DateTime(nullable: false));
        }
    }
}
