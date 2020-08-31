namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v20 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "SaleReferenceId", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "SaleReferenceId");
        }
    }
}
