using System;
using System.Threading.Tasks;
using itechart.PerformanceReview.Data.Model;
using itechart.PerformanceReview.Data.Model.Enum;

namespace itechart.PerformanceReview.Data.Repository.Impl
{
    public class ApplicationSettingsRepository : GenericRepository<ApplicationSetting>, IApplicationSettingsRepository
    {
        public ApplicationSettingsRepository(PerformanceReviewDbContext context)
            : base(context)
        {
        }

        private static readonly ApplicationSetting DefaulApplicationSetting = new ApplicationSetting
        {
            IsDefault = true,
            MinDataSynchronizationInterval = (long)TimeSpan.FromMinutes(10).TotalMilliseconds,
            EmployeesAccessRule = (int)EmployeesAccessRules.EmployeesInsideDepartment,
            DepartmentsAccessRule = (int)DepartmentsAccessRules.OnlyOwnDepartment
        };

        public ApplicationSetting GetApplicationSettings()
        {
            var applicationSettings = Find(x => !x.IsDefault);
            return applicationSettings ?? CreateFromDefault();
        }

        private ApplicationSetting CreateFromDefault()
        {
            var defaultSettings = Find(x => x.IsDefault);
            if (defaultSettings == null)
            {
                defaultSettings = DefaulApplicationSetting;
                Insert(defaultSettings);
            }

            return defaultSettings;
        }
    }
}