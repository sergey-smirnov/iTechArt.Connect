using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using itechart.connect.api.Helpers;
using itechart.connect.api.smg.Model.Parameters;
using itechart.connect.api.smg.Model.Result;
using itechart.connect.api.smg.Proxy;
using itechart.PerformanceReview.Data.Model;
using itechart.PerformanceReview.Data.Repository;

namespace itechart.connect.api.Services.Synchronizers.Impl
{
    public class DepartmentsSynchronizer : BaseDataSynchronizer<Department>
    {
        #region private fields

        private readonly SmgMobileServiceProxy api = new SmgMobileServiceProxy();

        #endregion

        #region ctor

        public DepartmentsSynchronizer(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion

        #region IDataSynchonizer implementation

        public override async Task<List<Department>> SyncData(string sessionId = null, bool returnUpdatedItems = false)
        {
            sessionId = sessionId ?? SessionHelper.GetSessionIdFromOwinContext();
            if (String.IsNullOrEmpty(sessionId))
            {
                //TODO: move this logic to one place
                throw new AuthenticationException();
            }

            var entityType = await UnitOfWork.EntityTypesRepository.FindAsync(x => x.Name == EntityType.DepartmentEntityName);

            var nowUtc = DateTime.UtcNow;

            var updatedDepartments = await GetUpdatedDepartments(sessionId, entityType);

            if (updatedDepartments != null && updatedDepartments.Any())
            {
                await SynchronizeChangesWithDatabase(updatedDepartments);
            }

            SaveUpdateToHistory(entityType, nowUtc);

            if (returnUpdatedItems && updatedDepartments != null && updatedDepartments.Any())
            {
                return updatedDepartments.AsParallel()
                    .Select(x => UnitOfWork.DepartmentsRepository.Find(d => d.SmgDepartmentId == x.Id))
                    .Where(x => x != null).ToList();
            }
            return null;
        }

        #endregion

        #region helpers

        private async Task<List<DepartmentResult>> GetUpdatedDepartments(string sessionId, EntityType entityType)
        {
            List<DepartmentResult> updatedDepartments;

            var lastUpdatedDate = await GetLastUpdatedDate(entityType);
            if (lastUpdatedDate != DateTime.MinValue)
            {
                var departmentsUpdatedResult = await api.GetAllDepartmentsUpdated(new GetAllDeparmentsUpdatedParameters
                {
                    SessionId = sessionId,
                    UpdatedDate = lastUpdatedDate
                });
                updatedDepartments = departmentsUpdatedResult.Departments;
            }
            else
            {
                var departmentsResult = await api.GetAllDepartments(new GetAllDeparmentsParameters
                {
                    SessionId = sessionId
                });
                updatedDepartments = departmentsResult.Departments;
            }
            return updatedDepartments;
        }

        private Department CreateOrUpdateDepartment(DepartmentResult departmentResult, Department department = null)
        {
            if (department == null)
            {
                department = new Department();
            }

            department.DepartmentCode = departmentResult.DepartmentCode;
            department.SmgDepartmentId = departmentResult.Id;
            department.Name = departmentResult.Name;

            return department;
        }

        private async Task SynchronizeChangesWithDatabase(List<DepartmentResult> updatedDepartments)
        {
            foreach (var departmentResult in updatedDepartments)
            {
                var result = departmentResult;
                var department =
                    await UnitOfWork.DepartmentsRepository.FindAsync(x => x.SmgDepartmentId == result.Id);
                var departmentAlreadyExist = department != null;
                department = CreateOrUpdateDepartment(departmentResult, department);

                if (departmentAlreadyExist)
                {
                    UnitOfWork.DepartmentsRepository.Update(department, false);
                }
                else
                {
                    UnitOfWork.DepartmentsRepository.Insert(department, false);
                }
            }
            UnitOfWork.DepartmentsRepository.SaveChanges();
        }

        #endregion
    }
}