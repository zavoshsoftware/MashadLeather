namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v35 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductCategories", "Size", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductCategories", "Size", c => c.String());
        }
    }
}
