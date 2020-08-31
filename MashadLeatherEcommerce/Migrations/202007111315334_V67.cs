namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V67 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogGroups", "TitleEn", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BlogGroups", "TitleEn");
        }
    }
}
