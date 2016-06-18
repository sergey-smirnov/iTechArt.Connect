using itechart.PerformanceReview.Data.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace itechart.connect.api.Identity.Infrastructure
{
    public class ApplicationRoleManager: RoleManager<Role>
    {
        public ApplicationRoleManager(IRoleStore<Role, string> store) : base(store)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            var appRoleManager = new ApplicationRoleManager(new RoleStore<Role>(context.Get<PerformanceReviewDbContext>()));

            return appRoleManager;
        }
    }
}