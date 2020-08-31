namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v004 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ProductCategories", new[] { "ParentId" });
            AlterColumn("dbo.ProductCategories", "ParentId", c => c.Guid());
            CreateIndex("dbo.ProductCategories", "ParentId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProductCategories", new[] { "ParentId" });
            AlterColumn("dbo.ProductCategories", "ParentId", c => c.Guid(nullable: false));
            CreateIndex("dbo.ProductCategories", "ParentId");
        }
    }
}
