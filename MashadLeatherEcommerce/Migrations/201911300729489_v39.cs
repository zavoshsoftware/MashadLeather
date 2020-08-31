namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v39 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "BankName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "BankName");
        }
    }
}
