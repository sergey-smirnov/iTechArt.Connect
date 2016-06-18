using System.ComponentModel;

namespace itechart.PerformanceReview.Data.Model.Enum
{
    public enum Roles
    {
        [Description(Role.AdminRoleName)]
        Admin = 0,

        [Description(Role.DepartmentManagerRoleName)]
        DepartmentManager,

        [Description(Role.MentorRoleName)]
        Mentor,

        [Description(Role.UserRoleName)]
        User
    }
}