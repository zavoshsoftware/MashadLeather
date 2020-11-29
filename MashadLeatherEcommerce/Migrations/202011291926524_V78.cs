namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V78 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "CityId", "dbo.Cities");
            DropIndex("dbo.Users", new[] { "CityId" });
            AlterColumn("dbo.Users", "FirstName", c => c.String(maxLength: 250));
            AlterColumn("dbo.Users", "LastName", c => c.String(maxLength: 250));
            AlterColumn("dbo.Users", "CityId", c => c.Guid());
            CreateIndex("dbo.Users", "CityId");
            AddForeignKey("dbo.Users", "CityId", "dbo.Cities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "CityId", "dbo.Cities");
            DropIndex("dbo.Users", new[] { "CityId" });
            AlterColumn("dbo.Users", "CityId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Users", "LastName", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Users", "FirstName", c => c.String(nullable: false, maxLength: 250));
            CreateIndex("dbo.Users", "CityId");
            AddForeignKey("dbo.Users", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
        }
    }
}
