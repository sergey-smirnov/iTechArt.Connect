using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using itechart.connect.api.Helpers;
using itechart.connect.api.Model.Extended;
using itechart.PerformanceReview.Data.Helpers;
using itechart.PerformanceReview.Data.Model;
using itechart.PerformanceReview.Data.Model.Enum;
using itechart.PerformanceReview.Data.Repository;
using itechart.PerformanceReview.Data.Repository.Impl;
using Microsoft.AspNet.Identity.Owin;

namespace itechart.connect.api.Services
{
    public class DepartmentsService
    {
        private IUnitOfWork UnitOfWork
        {
            get
            {
                var dbContext = HttpContext.Current.GetOwinContext().Get<PerformanceReviewDbContext>();
                return new UnitOfWork(dbContext);
            }
        }

        #region sigleton

        private static DepartmentsService instance;

        public static DepartmentsService Instance
        {
            get
            {
                return instance ?? (instance = new DepartmentsService());
            }
        }

        private DepartmentsService()
        {
        }

        #endregion

        public IQueryable<Department> Query()
        {
            var query = UnitOfWork.DepartmentsRepository.Query();

            if (ApplicationSettingsService.Instance.Settings.DepartmentsAccessRule != (int)DepartmentsAccessRules.AllDepartmentAvailable)
            {
                var departmentId = SessionHelper.GetUserDepartmentIdFromOwinContext();
                if (departmentId != Guid.Empty)
                {
                    if (ApplicationSettingsService.Instance.Settings.DepartmentsAccessRule ==
                        (int)DepartmentsAccessRules.OnlyOwnDepartment)
                    {
                        query = query.Where(x => x.DepartmentId == departmentId);
                    }
                }
            }

            return query;
        }

        public bool IsDepartmentLocked(Department department)
        {
            if (ApplicationSettingsService.Instance.Settings.DepartmentsAccessRule != (int)DepartmentsAccessRules.AllDepartmentAvailable)
            {
                var departmentId = SessionHelper.GetUserDepartmentIdFromOwinContext();
                if (departmentId != Guid.Empty)
                {
                    return departmentId != department.DepartmentId;
                }
            }

            return true;
        }
    }

    public static class DepartmentsExtensions
    {
        private static IUnitOfWork UnitOfWork
        {
            get
            {
                var dbContext = HttpContext.Current.GetOwinContext().Get<PerformanceReviewDbContext>();
                return new UnitOfWork(dbContext);
            }
        }

        public static IEnumerable<DepartmentExtended> SelectExtended(this IEnumerable<Department> source, bool includeDepartmentManager = false)
        {
            if (ApplicationSettingsService.Instance.Settings.DepartmentsAccessRule != (int)DepartmentsAccessRules.AllDepartmentAvailable)
            {
                var departmentId = SessionHelper.GetUserDepartmentIdFromOwinContext();
                if (departmentId != Guid.Empty)
                {
                    return source.Select(x => x.ToExtended(departmentId, includeDepartmentManager));
                }
            }

            return source.Select(x => new DepartmentExtended(x, false));
        }

        public static DepartmentExtended ToExtended(this Department department, Guid currentDepartmentId, bool includeDepartmentManager = false)
        {
            var isLocked = false;
            if (ApplicationSettingsService.Instance.Settings.DepartmentsAccessRule !=
                (int) DepartmentsAccessRules.AllDepartmentAvailable && currentDepartmentId != Guid.Empty)
            {
                isLocked = department.DepartmentId != currentDepartmentId;
            }

            User departmentManager = null;
            if (includeDepartmentManager)
            {
                var departmentManagerPositionName =
                    EnumHelper<EmployeesPositions>.GetDescriptionValue(EmployeesPositions.DepartmentManager).ToLower();
                departmentManager =
                    UnitOfWork.UsersRepository.Find(
                        x =>
                            x.DepartmentId == department.DepartmentId &&
                            x.UserProfile.Position.ToLower() == departmentManagerPositionName);
            }

            return new DepartmentExtended(department, isLocked, departmentManager);
        }

        public static DepartmentExtended ToExtended(this Department department, bool includeDepartmentManager = false)
        {
            var departmentId = SessionHelper.GetUserDepartmentIdFromOwinContext();
            return ToExtended(department, departmentId, includeDepartmentManager);
        }
    }
}