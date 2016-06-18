using System.ComponentModel;
using System.Runtime.Serialization;

namespace itechart.PerformanceReview.Data.Filters.Enum
{
    [DataContract]
    public enum UserSortFields
    {
        [EnumMember(Value = "id")]
        [Description("Id")]
        Id = 0,

        [EnumMember(Value = "smgId")]
        [Description("SmgUserId")]
        SmgId,

        [EnumMember(Value = "firstNameEng")]
        [Description("UserProfile.FirstNameEng")]
        FirstNameEng,

        [EnumMember(Value = "lastNameEng")]
        [Description("UserProfile.LastNameEng")]
        LastNameEng,

        [EnumMember(Value = "firstName")]
        [Description("UserProfile.FirstName")]
        FirstName,

        [EnumMember(Value = "lastName")]
        [Description("UserProfile.LastName")]
        LastName,

        [EnumMember(Value = "room")]
        [Description("UserProfile.Room")]
        Room,

        [EnumMember(Value = "position")]
        [Description("UserProfile.Position")]
        Position,

        [EnumMember(Value = "departmentName")]
        [Description("Department.Name")]
        DepartmentName,

        [EnumMember(Value = "departmentCode")]
        [Description("Department.DepartmentCode")]
        DepartmentCode
    }
}