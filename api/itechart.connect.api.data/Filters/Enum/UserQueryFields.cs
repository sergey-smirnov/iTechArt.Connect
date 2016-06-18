using System.Runtime.Serialization;

namespace itechart.PerformanceReview.Data.Filters.Enum
{
    [DataContract]
    public enum UserQueryFields
    {
        [EnumMember(Value = "smgId")]
        SmgId,

        [EnumMember(Value = "nameEng")]
        NameEng,

        [EnumMember(Value = "firstNameEng")]
        FirstNameEng,

        [EnumMember(Value = "lastNameEng")]
        LastNameEng,

        [EnumMember(Value = "room")]
        Room,

        [EnumMember(Value = "position")]
        Position,

        [EnumMember(Value = "departmentName")]
        DepartmentName,

        [EnumMember(Value = "departmentCode")]
        DepartmentCode
    }
}