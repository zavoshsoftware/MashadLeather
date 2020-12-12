namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V84 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DiscountCodes", "OperatorUsername", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DiscountCodes", "OperatorUsername");
        }
    }
}
