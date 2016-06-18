namespace itechart.PerformanceReview.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationSettings", "DepartmentsAccessRule", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationSettings", "DepartmentsAccessRule");
        }
    }
}
