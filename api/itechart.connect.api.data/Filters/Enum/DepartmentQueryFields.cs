using System.Runtime.Serialization;

namespace itechart.PerformanceReview.Data.Filters.Enum
{
    [DataContract]
    public enum DepartmentQueryFields
    {
        [EnumMember(Value = "name")]
        Name = 0,

        [EnumMember(Value = "code")]
        Code
    }
}