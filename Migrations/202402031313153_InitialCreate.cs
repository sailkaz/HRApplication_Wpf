namespace HRApplication_Wpf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(nullable: false, maxLength: 30),
                        Comments = c.String(),
                        EmploymentDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        DismissDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        HourlyRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EmployeeTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmployeeTypes", t => t.EmployeeTypeId, cascadeDelete: true)
                .Index(t => t.EmployeeTypeId);
            
            CreateTable(
                "dbo.EmployeeTypes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "EmployeeTypeId", "dbo.EmployeeTypes");
            DropIndex("dbo.Employees", new[] { "EmployeeTypeId" });
            DropTable("dbo.EmployeeTypes");
            DropTable("dbo.Employees");
        }
    }
}
