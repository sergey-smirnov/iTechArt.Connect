using System.ComponentModel;
using System.Runtime.Serialization;

namespace itechart.PerformanceReview.Data.Filters.Enum
{
    [DataContract]
    public enum DepartmentSortFields
    {
        [EnumMember(Value = "name")]
        [Description("Name")]
        Name = 0,

        [EnumMember(Value = "code")]
        [Description("DepartmentCode")]
        Code,

        [EnumMember(Value = "employeesCount")]
        [Description("Users.Count")]
        EmployeesCount
    }
}