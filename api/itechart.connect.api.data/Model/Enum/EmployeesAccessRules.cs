using System.ComponentModel;

namespace itechart.PerformanceReview.Data.Model.Enum
{
    public enum EmployeesAccessRules
    {
        [Description("EmployeesInsideDepartment")]
        EmployeesInsideDepartment = 0,

        [Description("AllEmployees")]
        AllEmployees = 1
    }
}