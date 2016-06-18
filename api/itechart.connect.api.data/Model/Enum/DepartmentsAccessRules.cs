using System.ComponentModel;

namespace itechart.PerformanceReview.Data.Model.Enum
{
    public enum DepartmentsAccessRules
    {
        [Description("AllDepartmentVisible")]
        AllDepartmentVisible = 0,

        [Description("AllDepartmentAvailable")]
        AllDepartmentAvailable = 1,

        [Description("OnlyOwnDepartment")]
        OnlyOwnDepartment = 2
    }
}