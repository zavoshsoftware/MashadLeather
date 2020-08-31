namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v014 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CityId", c => c.Guid());
            CreateIndex("dbo.Orders", "CityId");
            AddForeignKey("dbo.Orders", "CityId", "dbo.Cities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CityId", "dbo.Cities");
            DropIndex("dbo.Orders", new[] { "CityId" });
            DropColumn("dbo.Orders", "CityId");
        }
    }
}
