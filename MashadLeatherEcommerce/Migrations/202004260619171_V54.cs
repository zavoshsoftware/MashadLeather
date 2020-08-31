namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V54 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TempBranches",
                c => new
                    {
                        bId = c.Guid(nullable: false),
                        BranchName = c.String(),
                        BranchAddress = c.String(),
                        BranchCity = c.String(),
                        BranchLatitude = c.String(),
                        BranchLongitude = c.String(),
                        isEnabled = c.Boolean(nullable: false),
                        Priority = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.bId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TempBranches");
        }
    }
}
