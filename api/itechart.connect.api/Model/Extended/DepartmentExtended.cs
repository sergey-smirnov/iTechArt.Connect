using itechart.PerformanceReview.Data.Model;

namespace itechart.connect.api.Model.Extended
{
    public sealed class DepartmentExtended : Department
    {
        public bool IsLocked { get; set; }

        public User DepartmentManager { get; set; }

        public DepartmentExtended(Department department, bool isLocked, User departmentManager = null)
        {
            IsLocked = isLocked;
            DepartmentCode = department.DepartmentCode;
            Name = department.Name;
            SmgDepartmentId = department.SmgDepartmentId;
            Users = department.Users;
            DepartmentId = department.DepartmentId;
            DepartmentManager = departmentManager;
        }
    }
}