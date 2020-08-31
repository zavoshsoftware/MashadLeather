namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v002 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ProductCategories", new[] { "ParentProductCategory_Id" });
            DropColumn("dbo.ProductCategories", "ParentId");
            RenameColumn(table: "dbo.ProductCategories", name: "ParentProductCategory_Id", newName: "ParentId");
            AlterColumn("dbo.ProductCategories", "ParentId", c => c.Guid(nullable: false));
            CreateIndex("dbo.ProductCategories", "ParentId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProductCategories", new[] { "ParentId" });
            AlterColumn("dbo.ProductCategories", "ParentId", c => c.Guid());
            RenameColumn(table: "dbo.ProductCategories", name: "ParentId", newName: "ParentProductCategory_Id");
            AddColumn("dbo.ProductCategories", "ParentId", c => c.Guid(nullable: false));
            CreateIndex("dbo.ProductCategories", "ParentProductCategory_Id");
        }
    }
}
