namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V104 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ClubLevelCode", c => c.Int());
            AddColumn("dbo.Users", "ClubLevelTitle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ClubLevelTitle");
            DropColumn("dbo.Users", "ClubLevelCode");
        }
    }
}
