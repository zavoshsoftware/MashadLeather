namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V102 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "SentDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "SentDate");
        }
    }
}
