namespace NükEM.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PersonnelDetails",
                c => new
                    {
                        PersonnelId = c.Int(nullable: false, identity: true),
                        Birthdate = c.DateTime(),
                        Disability = c.String(),
                        MaritalStatus = c.String(),
                        WorkingStatusOfSpouse = c.String(),
                        NumberOfChildren = c.Int(),
                    })
                .PrimaryKey(t => t.PersonnelId);
            
            CreateTable(
                "dbo.Personnels",
                c => new
                    {
                        PersonnelId = c.Int(nullable: false),
                        Name = c.String(),
                        Surname = c.String(),
                        Department = c.String(),
                        RecruitmentDate = c.DateTime(),
                        TimeInTitle = c.Int(),
                        TitleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PersonnelId)
                .ForeignKey("dbo.PersonnelDetails", t => t.PersonnelId)
                .ForeignKey("dbo.Titles", t => t.TitleId, cascadeDelete: true)
                .Index(t => t.PersonnelId)
                .Index(t => t.TitleId);
            
            CreateTable(
                "dbo.Titles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descriptions = c.String(),
                        DailySalary = c.Double(),
                        AdvancementRiseRate = c.Double(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SalaryCalculationConstants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SCCCode = c.String(),
                        Description = c.String(),
                        SCCRatio = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TaxBrackets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MinCITA = c.Int(),
                        MaxCITA = c.Int(),
                        Bracket = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Personnels", "TitleId", "dbo.Titles");
            DropForeignKey("dbo.Personnels", "PersonnelId", "dbo.PersonnelDetails");
            DropIndex("dbo.Personnels", new[] { "TitleId" });
            DropIndex("dbo.Personnels", new[] { "PersonnelId" });
            DropTable("dbo.TaxBrackets");
            DropTable("dbo.SalaryCalculationConstants");
            DropTable("dbo.Titles");
            DropTable("dbo.Personnels");
            DropTable("dbo.PersonnelDetails");
        }
    }
}
