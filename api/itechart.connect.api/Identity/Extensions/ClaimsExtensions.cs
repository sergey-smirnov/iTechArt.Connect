using System.Security.Claims;
using System.Threading.Tasks;
using itechart.PerformanceReview.Data.Model;
using Microsoft.AspNet.Identity;

namespace itechart.connect.api.Identity.Extensions
{
    public static class ClaimsExtensions
    {
        public static async Task<ClaimsIdentity> GenerateUserIdentityAsync(this User user, UserManager<User> manager, string authenticationType)
        {
            
            var userIdentity = await manager.CreateIdentityAsync(user, authenticationType);

            // Add custom user claims here
            userIdentity.AddClaim(new Claim(ClaimTypes.Sid, user.SessionId));
            userIdentity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            userIdentity.AddClaim(new Claim(ClaimTypes.GroupSid, user.DepartmentId.ToString()));
            
            return userIdentity;
        }
    }
}