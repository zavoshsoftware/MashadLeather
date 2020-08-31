namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V49 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Texts", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Texts", "Name");
        }
    }
}
