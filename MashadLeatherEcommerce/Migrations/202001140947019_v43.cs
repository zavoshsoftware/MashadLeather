namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v43 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Response", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "Response");
        }
    }
}
