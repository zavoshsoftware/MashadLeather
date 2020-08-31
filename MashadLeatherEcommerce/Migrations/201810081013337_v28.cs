namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v28 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "CellNum", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "CellNum", c => c.String(maxLength: 20));
        }
    }
}
