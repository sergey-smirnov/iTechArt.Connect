using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using itechart.connect.api.Helpers;
using itechart.connect.api.smg.Model.Parameters;
using itechart.connect.api.smg.Model.Struct;
using itechart.connect.api.smg.Proxy;
using itechart.connect.api.Services;
using itechart.connect.api.Services.Synchronizers.Impl;
using itechart.PerformanceReview.Data.Filters;
using itechart.PerformanceReview.Data.Filters.Enum;
using itechart.PerformanceReview.Data.Model;
using itechart.PerformanceReview.Data.Model.Enum;
using itechart.PerformanceReview.Data.Repository;
using itechart.PerformanceReview.Data.Repository.Impl;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace itechart.connect.api.Identity.Infrastructure
{
    public class ApplicationUserManager : UserManager<User>
    {
        private readonly IUnitOfWork unitOfWork;

        public ApplicationUserManager(IUserStore<User> store, IUnitOfWork unitOfWork)
            : base(store)
        {
            this.unitOfWork = unitOfWork;
        }

        public override IQueryable<User> Users
        {
            get
            {
                if (ApplicationSettingsService.Instance.Settings.EmployeesAccessRule == (int)EmployeesAccessRules.EmployeesInsideDepartment)
                {
                    var departmentId = SessionHelper.GetUserDepartmentIdFromOwinContext();
                    if (departmentId != Guid.Empty)
                    {
                        return unitOfWork.UsersRepository.QueryInDepartment(departmentId);
                    }
                }

                return unitOfWork.UsersRepository.Query();
            }
        }

        public async override Task<IdentityResult> UpdateAsync(User user)
        {
            string[] errors = null;
            try
            {
                unitOfWork.UsersRepository.Context.Entry(user).State = EntityState.Modified;
                unitOfWork.UsersRepository.Update(user);
            }
            catch (Exception exception)
            {
                errors = new[] { exception.Message };
            }

            var identityResult = errors != null ? IdentityResult.Failed(errors) : IdentityResult.Success;
            return await Task.FromResult(identityResult);
        }

        public async Task<List<User>> FilterUsers(PagedFilter<User, UserSortFields> filter)
        {
            var users = Users;
            if (filter != null)
            {
                users = users.FilterQuery(filter);
            }

            return await users.ToListAsync();
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var appDbContext = context.Get<PerformanceReviewDbContext>();
            var appUserManager = new ApplicationUserManager(new UserStore<User>(appDbContext), new UnitOfWork(appDbContext));

            return appUserManager;
        }

        public async Task<User> FindByNameAsync(string userName, EmployeesAccessRules? accessRule = null, bool skipSync = false)
        {
            accessRule = accessRule ?? (EmployeesAccessRules?)ApplicationSettingsService.Instance.Settings.EmployeesAccessRule;

            if (!skipSync)
            {
                await DataSyncService.SynchronizeDataWithSmg();
            }

            IQueryable<User> users;
            if ((int)accessRule.Value != ApplicationSettingsService.Instance.Settings.EmployeesAccessRule)
            {
                if (accessRule.Value == EmployeesAccessRules.AllEmployees)
                {
                    users = unitOfWork.UsersRepository.Query();
                }
                else
                {
                    var departmentId = SessionHelper.GetUserDepartmentIdFromOwinContext();
                    if (departmentId != Guid.Empty)
                    {
                        users = unitOfWork.UsersRepository.QueryInDepartment(departmentId);
                    }
                    else
                    {
                        throw new ApplicationException("User's department not found");
                    }
                }
            }
            else
            {
                users = Users;
            }

            var lowerUserName = userName.ToLower();
            return await users.FirstOrDefaultAsync(x => x.UserName.ToLower() == lowerUserName);
        }

        public async Task<User> FindByNameAsync(string userName, string sessionId)
        {
            if (String.IsNullOrEmpty(userName))
            {
                return null;
            }

            await DataSyncService.SynchronizeDataWithSmg(sessionId);

            return await FindByNameAsync(userName);
        }

        public override async Task<User> FindByIdAsync(string userId)
        {
            return await Users.FirstOrDefaultAsync(x => x.Id == userId);
        }

        public override async Task<User> FindAsync(string userName, string password)
        {
            var api = new SmgMobileServiceProxy();
            var authResult = await api.Authenticate(new AuthenticateParameters { Password = password, UserName = userName });

            if (authResult.ErrorCode.Equals(ErrorCode.None) && !String.IsNullOrEmpty(authResult.SesionId))
            {
                var user = await FindByNameAsync(userName, authResult.SesionId); ;
                if (user != null)
                {
                    user.SessionId = authResult.SesionId;

                    //TODO: remove, added just for testing
                    if (!(await IsInRoleAsync(user.Id, Role.AdminRoleName)))
                    {
                        var addToRoleResult = await AddToRoleAsync(user.Id, Role.AdminRoleName);
                        if (!addToRoleResult.Succeeded)
                        {
                            throw new ApplicationException(String.Format("Cannot add user to role {0}: {1}",
                                Role.AdminRoleName, String.Join(Environment.NewLine, addToRoleResult.Errors)));
                        }
                    }

                    return user;
                }
            }

            return null;
        }
    }
}