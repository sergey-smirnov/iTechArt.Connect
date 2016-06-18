using System.ComponentModel;
using System.Runtime.Serialization;

namespace itechart.PerformanceReview.Data.Model.Enum
{
    [DataContract]
    public enum EmployeesPositions
    {
        [Description("None")]
        None = 0,

        [Description("Accountant")]
        Accountant,

        [Description("Business Development Manager")]
        BusinessDevelopmentManager,

        [Description("Chief Executive Officer")]
        ChiefExecutiveOfficer,

        [Description("Department Manager")]
        DepartmentManager,

        [Description("Designer")]
        Designer,

        [Description("HR Manager")]
        HRManager,

        [Description("Lead Software Engineer")]
        LeadSoftwareEngineer,

        [Description("Marketing Manager")]
        MarketingManager,

        [Description("PR Manager")]
        PRManager,

        [Description("Quality Assurance Engineer")]
        QualityAssuranceEngineer,

        [Description("Senior Quality Assurance Engineer")]
        SeniorQualityAssuranceEngineer,

        [Description("Senior Software Engineer")]
        SeniorSoftwareEngineer,

        [Description("Software Engineer")]
        SoftwareEngineer,

        [Description("Support Staff")]
        SupportStaff,

        [Description("Technical Support Engineer")]
        TechnicalSupportEngineer,

        [Description("Telemarketer")]
        Telemarketer
    }
}