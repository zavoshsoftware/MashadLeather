namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V56 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SiteBranches", "Order", c => c.Int());
            AddColumn("dbo.SiteBranchGroups", "Order", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SiteBranchGroups", "Order");
            DropColumn("dbo.SiteBranches", "Order");
        }
    }
}
