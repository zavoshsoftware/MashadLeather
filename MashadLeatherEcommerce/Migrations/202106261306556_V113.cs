namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V113 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carreers", "ExpectedSalary", c => c.String());
            DropColumn("dbo.Carreers", "IsExpectedSalary");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carreers", "IsExpectedSalary", c => c.String());
            DropColumn("dbo.Carreers", "ExpectedSalary");
        }
    }
}
