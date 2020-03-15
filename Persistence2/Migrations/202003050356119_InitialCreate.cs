namespace Persistence2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agency",
                c => new
                    {
                        AgencyID = c.Int(nullable: false, identity: true),
                        AgencyName = c.String(nullable: false),
                        AgencyCode = c.String(nullable: false, maxLength: 4),
                        YearID = c.Int(nullable: false),
                        Added = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AgencyID)
                .ForeignKey("dbo.Years", t => t.YearID)
                .Index(t => t.YearID);
            
            CreateTable(
                "dbo.Years",
                c => new
                    {
                        YearID = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        Added = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.YearID)
                .Index(t => t.Year, unique: true);
            
            CreateTable(
                "dbo.Employment",
                c => new
                    {
                        EmploymentID = c.Int(nullable: false, identity: true),
                        AgencyID = c.Int(nullable: false),
                        EmployeeCount = c.Int(nullable: false),
                        QuitCount = c.Int(),
                        AttritionRate = c.Int(),
                        YearID = c.Int(nullable: false),
                        Added = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EmploymentID)
                .ForeignKey("dbo.Agency", t => t.AgencyID)
                .ForeignKey("dbo.Years", t => t.YearID)
                .Index(t => t.AgencyID)
                .Index(t => t.YearID);
            
            CreateTable(
                "dbo.Survey",
                c => new
                    {
                        SurveyID = c.Int(nullable: false, identity: true),
                        AgencyID = c.Int(nullable: false),
                        ResponseValue = c.Double(nullable: false),
                        YearID = c.Int(nullable: false),
                        QuestionNumber = c.Int(nullable: false),
                        Added = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SurveyID)
                .ForeignKey("dbo.Agency", t => t.AgencyID)
                .ForeignKey("dbo.Years", t => t.YearID)
                .Index(t => t.AgencyID)
                .Index(t => t.YearID)
                .Index(t => t.QuestionNumber);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Survey", "YearID", "dbo.Years");
            DropForeignKey("dbo.Survey", "AgencyID", "dbo.Agency");
            DropForeignKey("dbo.Employment", "YearID", "dbo.Years");
            DropForeignKey("dbo.Employment", "AgencyID", "dbo.Agency");
            DropForeignKey("dbo.Agency", "YearID", "dbo.Years");
            DropIndex("dbo.Survey", new[] { "QuestionNumber" });
            DropIndex("dbo.Survey", new[] { "YearID" });
            DropIndex("dbo.Survey", new[] { "AgencyID" });
            DropIndex("dbo.Employment", new[] { "YearID" });
            DropIndex("dbo.Employment", new[] { "AgencyID" });
            DropIndex("dbo.Years", new[] { "Year" });
            DropIndex("dbo.Agency", new[] { "YearID" });
            DropTable("dbo.Survey");
            DropTable("dbo.Employment");
            DropTable("dbo.Years");
            DropTable("dbo.Agency");
        }
    }
}
