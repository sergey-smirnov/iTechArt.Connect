using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using itechart.PerformanceReview.Data.Model;
using itechart.PerformanceReview.Data.Model.Enum;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace itechart.PerformanceReview.Data.Initializers
{
    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<PerformanceReviewDbContext>
    {
        protected override void Seed(PerformanceReviewDbContext context)
        {
            InitializeUsers(context);
            InitializeRoles(context);
            InitializeEntityTypes(context);
            InitializeApplicationSettings(context);
        }

        private void InitializeUsers(PerformanceReviewDbContext context)
        {

        }

        private void InitializeRoles(PerformanceReviewDbContext context)
        {
            context.Roles.AddOrUpdate(x => x.Name,
                new Role { Name = Role.AdminRoleName },
                new Role { Name = Role.DepartmentManagerRoleName },
                new Role { Name = Role.MentorRoleName },
                new Role { Name = Role.UserRoleName });
        }

        private void InitializeEntityTypes(PerformanceReviewDbContext context)
        {
            //TODO: use names from enum
            context.EntityTypes.AddOrUpdate(x => x.Name, new[]
            {
                new EntityType {Name = EntityType.DepartmentEntityName}, 
                new EntityType {Name = EntityType.UserEntityName} 
            });
        }

        private void InitializeApplicationSettings(PerformanceReviewDbContext context)
        {
            context.ApplicationSettings.AddOrUpdate(x => x.IsDefault,
                new ApplicationSetting
                {
                    IsDefault = true,
                    MinDataSynchronizationInterval = (long)TimeSpan.FromMinutes(10).TotalMilliseconds,
                    EmployeesAccessRule = (int)EmployeesAccessRules.EmployeesInsideDepartment,
                    DepartmentsAccessRule = (int)DepartmentsAccessRules.OnlyOwnDepartment
                },
                new ApplicationSetting
                {
                    IsDefault = false,
                    MinDataSynchronizationInterval = (long)TimeSpan.FromMinutes(10).TotalMilliseconds,
                    EmployeesAccessRule = (int)EmployeesAccessRules.AllEmployees,
                    DepartmentsAccessRule = (int)DepartmentsAccessRules.AllDepartmentVisible
                });
        }
    }
}