namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v109 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarreerEducationalCourses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CourseName = c.String(),
                        InstitutionName = c.String(),
                        CourseDuration = c.String(),
                        Skill = c.String(),
                        CarreerId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                        DescriptionEn = c.String(),
                        DescriptionAr = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carreers", t => t.CarreerId, cascadeDelete: true)
                .Index(t => t.CarreerId);
            
            CreateTable(
                "dbo.CarreerFamilyInformations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FullName = c.String(),
                        Relationship = c.String(),
                        BirthdayDate = c.DateTime(nullable: false),
                        EducationalLevel = c.String(),
                        Job = c.String(),
                        CellNumber = c.String(),
                        CarreerId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                        DescriptionEn = c.String(),
                        DescriptionAr = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carreers", t => t.CarreerId, cascadeDelete: true)
                .Index(t => t.CarreerId);
            
            CreateTable(
                "dbo.CarreerIntroduceds",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FullName = c.String(),
                        Relationship = c.String(),
                        Job = c.String(),
                        WorkPlace = c.String(),
                        HomePhone = c.String(),
                        CellNumber = c.String(),
                        Address = c.String(),
                        CarreerId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                        DescriptionEn = c.String(),
                        DescriptionAr = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carreers", t => t.CarreerId, cascadeDelete: true)
                .Index(t => t.CarreerId);
            
            CreateTable(
                "dbo.CarreerPreviousExperiences",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CompanyName = c.String(),
                        Post = c.String(),
                        StartDatetime = c.String(),
                        EndDatetime = c.String(),
                        Phone = c.String(),
                        LeavingWorkReason = c.String(),
                        ReceivedMoney = c.String(),
                        Address = c.String(),
                        CarreerId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                        DescriptionEn = c.String(),
                        DescriptionAr = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carreers", t => t.CarreerId, cascadeDelete: true)
                .Index(t => t.CarreerId);
            
            AddColumn("dbo.Carreers", "NationalCode", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.Carreers", "BirthdayDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Carreers", "PlaceOfBirth", c => c.String(nullable: false));
            AddColumn("dbo.Carreers", "IsLady", c => c.Boolean(nullable: false));
            AddColumn("dbo.Carreers", "IsMarried", c => c.Boolean(nullable: false));
            AddColumn("dbo.Carreers", "ChidNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Carreers", "PeopleInChargeNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Carreers", "Nationality", c => c.String(nullable: false));
            AddColumn("dbo.Carreers", "Address", c => c.String(nullable: false));
            AddColumn("dbo.Carreers", "MilitaryStatus", c => c.Int(nullable: false));
            AddColumn("dbo.Carreers", "PhysicalCondition", c => c.Int(nullable: false));
            AddColumn("dbo.Carreers", "IsInsurance", c => c.Boolean(nullable: false));
            AddColumn("dbo.Carreers", "DurationInsuranceHistory", c => c.Int(nullable: false));
            AddColumn("dbo.Carreers", "Education", c => c.Int(nullable: false));
            AddColumn("dbo.Carreers", "Major", c => c.String(nullable: false));
            AddColumn("dbo.Carreers", "LastUniversity", c => c.String());
            AddColumn("dbo.Carreers", "LastCertificateDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Carreers", "Software", c => c.String());
            AddColumn("dbo.Carreers", "Windows", c => c.String());
            AddColumn("dbo.Carreers", "OtherSoftware", c => c.String());
            AddColumn("dbo.Carreers", "Writing", c => c.String());
            AddColumn("dbo.Carreers", "Reading", c => c.String());
            AddColumn("dbo.Carreers", "Speaking", c => c.String());
            AddColumn("dbo.Carreers", "Listening", c => c.String());
            AddColumn("dbo.Carreers", "IntroduceName", c => c.String());
            AddColumn("dbo.Carreers", "IntroducePost", c => c.String());
            AddColumn("dbo.Carreers", "InterestedJob", c => c.String());
            AddColumn("dbo.Carreers", "IsExpectedSalary", c => c.String());
            AddColumn("dbo.Carreers", "RequestedPrice", c => c.String());
            AddColumn("dbo.Carreers", "HumanResourceinterviewerName", c => c.String());
            AddColumn("dbo.Carreers", "HumanResourceinterviewerJob", c => c.String());
            AddColumn("dbo.Carreers", "HumanResourceDescription", c => c.String());
            AddColumn("dbo.Carreers", "SpecializedInterviewerName", c => c.String());
            AddColumn("dbo.Carreers", "SpecializedInterviewerPost", c => c.String());
            AddColumn("dbo.Carreers", "SpecializedInterviewerDescription", c => c.String());
            AddColumn("dbo.Carreers", "IsConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Carreers", "Familiar", c => c.Int(nullable: false));
            AlterColumn("dbo.Carreers", "CellNumber", c => c.String(nullable: false, maxLength: 11));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarreerPreviousExperiences", "CarreerId", "dbo.Carreers");
            DropForeignKey("dbo.CarreerIntroduceds", "CarreerId", "dbo.Carreers");
            DropForeignKey("dbo.CarreerFamilyInformations", "CarreerId", "dbo.Carreers");
            DropForeignKey("dbo.CarreerEducationalCourses", "CarreerId", "dbo.Carreers");
            DropIndex("dbo.CarreerPreviousExperiences", new[] { "CarreerId" });
            DropIndex("dbo.CarreerIntroduceds", new[] { "CarreerId" });
            DropIndex("dbo.CarreerFamilyInformations", new[] { "CarreerId" });
            DropIndex("dbo.CarreerEducationalCourses", new[] { "CarreerId" });
            AlterColumn("dbo.Carreers", "CellNumber", c => c.String(nullable: false));
            DropColumn("dbo.Carreers", "Familiar");
            DropColumn("dbo.Carreers", "IsConfirmed");
            DropColumn("dbo.Carreers", "SpecializedInterviewerDescription");
            DropColumn("dbo.Carreers", "SpecializedInterviewerPost");
            DropColumn("dbo.Carreers", "SpecializedInterviewerName");
            DropColumn("dbo.Carreers", "HumanResourceDescription");
            DropColumn("dbo.Carreers", "HumanResourceinterviewerJob");
            DropColumn("dbo.Carreers", "HumanResourceinterviewerName");
            DropColumn("dbo.Carreers", "RequestedPrice");
            DropColumn("dbo.Carreers", "IsExpectedSalary");
            DropColumn("dbo.Carreers", "InterestedJob");
            DropColumn("dbo.Carreers", "IntroducePost");
            DropColumn("dbo.Carreers", "IntroduceName");
            DropColumn("dbo.Carreers", "Listening");
            DropColumn("dbo.Carreers", "Speaking");
            DropColumn("dbo.Carreers", "Reading");
            DropColumn("dbo.Carreers", "Writing");
            DropColumn("dbo.Carreers", "OtherSoftware");
            DropColumn("dbo.Carreers", "Windows");
            DropColumn("dbo.Carreers", "Software");
            DropColumn("dbo.Carreers", "LastCertificateDateTime");
            DropColumn("dbo.Carreers", "LastUniversity");
            DropColumn("dbo.Carreers", "Major");
            DropColumn("dbo.Carreers", "Education");
            DropColumn("dbo.Carreers", "DurationInsuranceHistory");
            DropColumn("dbo.Carreers", "IsInsurance");
            DropColumn("dbo.Carreers", "PhysicalCondition");
            DropColumn("dbo.Carreers", "MilitaryStatus");
            DropColumn("dbo.Carreers", "Address");
            DropColumn("dbo.Carreers", "Nationality");
            DropColumn("dbo.Carreers", "PeopleInChargeNumber");
            DropColumn("dbo.Carreers", "ChidNumber");
            DropColumn("dbo.Carreers", "IsMarried");
            DropColumn("dbo.Carreers", "IsLady");
            DropColumn("dbo.Carreers", "PlaceOfBirth");
            DropColumn("dbo.Carreers", "BirthdayDate");
            DropColumn("dbo.Carreers", "NationalCode");
            DropTable("dbo.CarreerPreviousExperiences");
            DropTable("dbo.CarreerIntroduceds");
            DropTable("dbo.CarreerFamilyInformations");
            DropTable("dbo.CarreerEducationalCourses");
        }
    }
}
