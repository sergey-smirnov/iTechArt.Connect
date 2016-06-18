namespace itechart.PerformanceReview.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationSettings",
                c => new
                    {
                        ApplicationSettingId = c.Guid(nullable: false, identity: true),
                        MinDataSynchronizationInterval = c.Long(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ApplicationSettingId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Guid(nullable: false, identity: true),
                        SmgDepartmentId = c.Int(nullable: false),
                        DepartmentCode = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        SmgUserId = c.Int(nullable: false),
                        UserProfileId = c.Guid(nullable: false),
                        DepartmentId = c.Guid(nullable: false),
                        Email = c.String(maxLength: 256),
                        SecurityStamp = c.String(),
                        AccessFailedCount = c.Int(nullable: false),
                        Login = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfiles", t => t.UserProfileId, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.UserProfileId)
                .Index(t => t.DepartmentId)
                .Index(t => t.Login, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        UserProfileId = c.Guid(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MiddleName = c.String(),
                        FirstNameEng = c.String(),
                        LastNameEng = c.String(),
                        Position = c.String(),
                        Room = c.String(),
                        PhotoUrl = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        SkypeId = c.String(),
                        PhoneNumber = c.String(),
                        DomainName = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.UserProfileId);
            
            CreateTable(
                "dbo.EntitiesUpdateHistories",
                c => new
                    {
                        EntitiesUpdateHistoryId = c.Guid(nullable: false, identity: true),
                        UpdatedDateUtc = c.DateTime(nullable: false),
                        EntityTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.EntitiesUpdateHistoryId)
                .ForeignKey("dbo.EntityTypes", t => t.EntityTypeId, cascadeDelete: true)
                .Index(t => t.EntityTypeId);
            
            CreateTable(
                "dbo.EntityTypes",
                c => new
                    {
                        EntityTypeId = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.EntityTypeId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.EntitiesUpdateHistories", "EntityTypeId", "dbo.EntityTypes");
            DropForeignKey("dbo.Users", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Users", "UserProfileId", "dbo.UserProfiles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.Users");
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.EntitiesUpdateHistories", new[] { "EntityTypeId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.Users", new[] { "DepartmentId" });
            DropIndex("dbo.Users", new[] { "UserProfileId" });
            DropTable("dbo.Roles");
            DropTable("dbo.EntityTypes");
            DropTable("dbo.EntitiesUpdateHistories");
            DropTable("dbo.UserProfiles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Departments");
            DropTable("dbo.ApplicationSettings");
        }
    }
}
