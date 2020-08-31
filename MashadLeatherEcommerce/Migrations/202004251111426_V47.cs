namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V47 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Texts", "Summery", c => c.String());
            AddColumn("dbo.Texts", "SummeryEn", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Texts", "SummeryEn");
            DropColumn("dbo.Texts", "Summery");
        }
    }
}
