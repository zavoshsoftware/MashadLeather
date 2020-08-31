namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V66 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SiteBranchGroups", "TitleEn", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SiteBranchGroups", "TitleEn");
        }
    }
}
