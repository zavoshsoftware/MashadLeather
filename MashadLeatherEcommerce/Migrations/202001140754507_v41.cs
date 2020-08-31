namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v41 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Blogs", "Body", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.Blogs", "BodyEn", c => c.String(storeType: "ntext"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Blogs", "BodyEn", c => c.String());
            AlterColumn("dbo.Blogs", "Body", c => c.String());
        }
    }
}
