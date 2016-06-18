using Microsoft.AspNet.Identity.EntityFramework;

namespace itechart.PerformanceReview.Data.Model
{
    public class Role : IdentityRole, IEntityBase
    {
        #region role names

        public const string AdminRoleName = "Administrator";
        public const string DepartmentManagerRoleName = "DepartmentManager";
        public const string MentorRoleName = "Mentor";
        public const string UserRoleName = "User";

        #endregion
    }
}