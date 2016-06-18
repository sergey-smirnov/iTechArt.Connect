using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using System.Web;
using itechart.connect.api.Helpers;
using itechart.connect.api.Identity.Infrastructure;
using itechart.connect.api.smg.Model.Parameters;
using itechart.connect.api.smg.Model.Result;
using itechart.connect.api.smg.Proxy;
using itechart.PerformanceReview.Data.Model;
using itechart.PerformanceReview.Data.Model.Enum;
using itechart.PerformanceReview.Data.Repository;
using Microsoft.AspNet.Identity.Owin;

namespace itechart.connect.api.Services.Synchronizers.Impl
{
    public class UsersSynchronizer : BaseDataSynchronizer<User>
    {
        #region constants

        private const int MinUsersCountToBatchUpdate = 3;

        #endregion

        #region private fields

        private readonly SmgMobileServiceProxy api = new SmgMobileServiceProxy();

        #endregion

        #region ctor

        public UsersSynchronizer(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion

        #region IDataSynchonizer impl

        public override async Task<List<User>> SyncData(string sessionId = null, bool returnUpdatedItems = false)
        {
            sessionId = sessionId ?? SessionHelper.GetSessionIdFromOwinContext();
            if (String.IsNullOrEmpty(sessionId))
            {
                //TODO: move this logic to one place
                throw new AuthenticationException();
            }

            var entityType = await UnitOfWork.EntityTypesRepository.FindAsync(x => x.Name == EntityType.UserEntityName);

            var nowUtc = DateTime.UtcNow;

            var updatedUsers = await GetUpdatedUsers(sessionId, entityType);

            if (updatedUsers != null && updatedUsers.Any())
            {
                await SynchronizeChangesWithDatabase(updatedUsers);
            }

            SaveUpdateToHistory(entityType, nowUtc);

            if (returnUpdatedItems && updatedUsers != null && updatedUsers.Any())
            {
                var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var updatedUsersFromDb = new List<User>();
                foreach (List<EmployeeFullResult> employees in updatedUsers.Values)
                {
                    updatedUsersFromDb.AddRange(
                        employees.Select(x => userManager.FindByNameAsync(x.DomainName).Result).Where(x => x != null));
                }

                return updatedUsersFromDb;
            }

            return null;
        }

        #endregion

        #region helpers


        private async Task<Dictionary<Guid, List<EmployeeFullResult>>> GetUpdatedUsers(string sessionId, EntityType entityType)
        {
            var updatedUsers = new Dictionary<Guid, List<EmployeeFullResult>>();

            var lastUpdatedDate = await GetLastUpdatedDate(entityType);

            var departments = await UnitOfWork.DepartmentsRepository.FindAllAsync();
            foreach (var department in departments)
            {
                if (lastUpdatedDate == DateTime.MinValue)
                {
                    var usersInDepartment =
                        await api.GetEmployeeDetailsListByDeptId(new GetEmployeeDetailsListByDeptIdParameters
                        {
                            SessionId = sessionId,
                            DepartmentId = department.SmgDepartmentId
                        });

                    if (usersInDepartment != null && usersInDepartment.Profiles.Any())
                    {
                        updatedUsers.Add(department.DepartmentId, usersInDepartment.Profiles);
                    }
                }
                else
                {
                    var updatedUsersInDepartment =
                        await api.GetEmployeesByDeptIdUpdated(new GetEmployeesByDeptIdUpdatedParameters
                        {
                            SessionId = sessionId,
                            DepartmentId = department.SmgDepartmentId,
                            UpdatedDate = lastUpdatedDate,
                            OnlyActive = true
                        });

                    var employees = updatedUsersInDepartment.Profiles;
                    if (employees != null && employees.Any())
                    {
                        var employeesFullProfiles = new List<EmployeeFullResult>();
                        if (employees.Count >= MinUsersCountToBatchUpdate)
                        {
                            var employeesDetails =
                                await api.GetEmployeeDetailsListByDeptId(new GetEmployeeDetailsListByDeptIdParameters
                                {
                                    SessionId = sessionId,
                                    DepartmentId = department.SmgDepartmentId
                                });
                            if (employeesDetails != null && employeesDetails.Profiles.Any())
                            {
                                employeesFullProfiles.AddRange(
                                    employeesDetails.Profiles.Where(x => employees.Any(e => e.ProfileId == x.ProfileId)));
                            }
                        }
                        else
                        {
                            foreach (var emplolyeeShortResult in employees)
                            {
                                var employeeDetailsResult =
                                    await api.GetEmployeeDetails(new GetEmployeeDetailsParameters
                                    {
                                        SessionId = sessionId,
                                        ProfileId = emplolyeeShortResult.ProfileId
                                    });

                                if (employeeDetailsResult != null && employeeDetailsResult.Profile != null)
                                {
                                    employeesFullProfiles.Add(employeeDetailsResult.Profile);
                                }
                            }
                        }

                        updatedUsers.Add(department.DepartmentId, employeesFullProfiles);
                    }
                }
            }

            return updatedUsers;
        }

        private User CreateOrUpdateUser(EmployeeFullResult employee, string sessionId = null, User user = null)
        {
            if (user == null)
            {
                user = new User
                {
                    AccessFailedCount = 0,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
            }
            user.SmgUserId = employee.ProfileId;
            user.Email = String.IsNullOrEmpty(employee.Email) ? null : employee.Email.ToLower();
            user.UserName = String.IsNullOrEmpty(employee.DomainName)
                ? String.Format("{0}.{1}", employee.FirstNameEng, employee.LastNameEng).ToLower()
                : employee.DomainName.ToLower();

            user.SessionId = sessionId;

            if (user.UserProfile == null)
            {
                user.UserProfile = new UserProfile();
            }
            user.UserProfile.DomainName = user.UserName;
            user.UserProfile.Birthday = employee.Birthday;
            user.UserProfile.Email = user.Email;
            user.UserProfile.PhoneNumber = employee.PhoneNumber;
            user.UserProfile.FirstName = employee.FirstName;
            user.UserProfile.LastName = employee.LastName;
            user.UserProfile.FirstNameEng = employee.FirstNameEng;
            user.UserProfile.LastNameEng = employee.LastNameEng;
            user.UserProfile.MiddleName = employee.MiddleName;
            user.UserProfile.PhotoUrl = employee.PhotoUrl;
            user.UserProfile.Position = employee.Position;
            user.UserProfile.Room = employee.Room;
            user.UserProfile.SkypeId = employee.SkypeId;

            return user;
        }

        private async Task SynchronizeChangesWithDatabase(Dictionary<Guid, List<EmployeeFullResult>> updatedUsers)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

            foreach (KeyValuePair<Guid, List<EmployeeFullResult>> usersInDepartment in updatedUsers)
            {
                foreach (EmployeeFullResult employeeFullResult in usersInDepartment.Value)
                {
                    var user = await userManager.FindByNameAsync(employeeFullResult.DomainName, EmployeesAccessRules.AllEmployees, true);
                    var isNewUser = user == null;

                    user = CreateOrUpdateUser(employeeFullResult, null, user);
                    user.DepartmentId = usersInDepartment.Key;

                    if (isNewUser)
                    {
                        try
                        {
                            var addUserResult = await userManager.CreateAsync(user);
                            if (!addUserResult.Succeeded)
                            {
                                throw new ApplicationException("Cannot add new user: " +
                                                               String.Join(Environment.NewLine, addUserResult.Errors));
                            }
                        }
                        catch (DbEntityValidationException exception)
                        {
                            //TODO: log error
                            continue;
                        }
                    }
                    else
                    {
                        var updateUserResult = await userManager.UpdateAsync(user);
                        if (!updateUserResult.Succeeded)
                        {
                            throw new ApplicationException("Cannot update new user: " +
                                                           String.Join(Environment.NewLine, updateUserResult.Errors));
                        }
                    }
                }
            }
        }

        #endregion
    }
}