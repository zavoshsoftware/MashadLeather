namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v003 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Cities", "CreateUserId");
            DropColumn("dbo.Cities", "DeleteUserId");
            DropColumn("dbo.Provinces", "CreateUserId");
            DropColumn("dbo.Provinces", "DeleteUserId");
            DropColumn("dbo.Users", "CreateUserId");
            DropColumn("dbo.Users", "DeleteUserId");
            DropColumn("dbo.Orders", "CreateUserId");
            DropColumn("dbo.Orders", "DeleteUserId");
            DropColumn("dbo.OrderDetails", "CreateUserId");
            DropColumn("dbo.OrderDetails", "DeleteUserId");
            DropColumn("dbo.Products", "CreateUserId");
            DropColumn("dbo.Products", "DeleteUserId");
            DropColumn("dbo.ProductCategories", "CreateUserId");
            DropColumn("dbo.ProductCategories", "DeleteUserId");
            DropColumn("dbo.ProductCategoryMappers", "CreateUserId");
            DropColumn("dbo.ProductCategoryMappers", "DeleteUserId");
            DropColumn("dbo.KiyanProductCategories", "CreateUserId");
            DropColumn("dbo.KiyanProductCategories", "DeleteUserId");
            DropColumn("dbo.OrderStatus", "CreateUserId");
            DropColumn("dbo.OrderStatus", "DeleteUserId");
            DropColumn("dbo.Roles", "CreateUserId");
            DropColumn("dbo.Roles", "DeleteUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Roles", "DeleteUserId", c => c.Guid());
            AddColumn("dbo.Roles", "CreateUserId", c => c.Guid());
            AddColumn("dbo.OrderStatus", "DeleteUserId", c => c.Guid());
            AddColumn("dbo.OrderStatus", "CreateUserId", c => c.Guid());
            AddColumn("dbo.KiyanProductCategories", "DeleteUserId", c => c.Guid());
            AddColumn("dbo.KiyanProductCategories", "CreateUserId", c => c.Guid());
            AddColumn("dbo.ProductCategoryMappers", "DeleteUserId", c => c.Guid());
            AddColumn("dbo.ProductCategoryMappers", "CreateUserId", c => c.Guid());
            AddColumn("dbo.ProductCategories", "DeleteUserId", c => c.Guid());
            AddColumn("dbo.ProductCategories", "CreateUserId", c => c.Guid());
            AddColumn("dbo.Products", "DeleteUserId", c => c.Guid());
            AddColumn("dbo.Products", "CreateUserId", c => c.Guid());
            AddColumn("dbo.OrderDetails", "DeleteUserId", c => c.Guid());
            AddColumn("dbo.OrderDetails", "CreateUserId", c => c.Guid());
            AddColumn("dbo.Orders", "DeleteUserId", c => c.Guid());
            AddColumn("dbo.Orders", "CreateUserId", c => c.Guid());
            AddColumn("dbo.Users", "DeleteUserId", c => c.Guid());
            AddColumn("dbo.Users", "CreateUserId", c => c.Guid());
            AddColumn("dbo.Provinces", "DeleteUserId", c => c.Guid());
            AddColumn("dbo.Provinces", "CreateUserId", c => c.Guid());
            AddColumn("dbo.Cities", "DeleteUserId", c => c.Guid());
            AddColumn("dbo.Cities", "CreateUserId", c => c.Guid());
        }
    }
}
