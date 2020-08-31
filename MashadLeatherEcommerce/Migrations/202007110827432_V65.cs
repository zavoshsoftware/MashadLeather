namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V65 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SiteBranches", "TitleEn", c => c.String());
            AddColumn("dbo.SiteBranches", "AddressEn", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SiteBranches", "AddressEn");
            DropColumn("dbo.SiteBranches", "TitleEn");
        }
    }
}
