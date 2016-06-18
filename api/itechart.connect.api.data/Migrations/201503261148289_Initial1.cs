namespace itechart.PerformanceReview.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationSettings", "EmployeesAccessRule", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationSettings", "EmployeesAccessRule");
        }
    }
}
