using System.Runtime.Serialization;
using itechart.connect.api.smg.SmgMobileService;

namespace itechart.connect.api.smg.Model.Result
{
    [DataContract]
    public class EmployeeExtraShortResult : IBaseApiResult<ProfileExtraShortWS>
    {
        [DataMember(Name = "ProfileId", EmitDefaultValue = false)]
        public int ProfileId { get; set; }

        [DataMember(Name = "FirstName", EmitDefaultValue = false)]
        public string FirstName { get; set; }

        [DataMember(Name = "LastName", EmitDefaultValue = false)]
        public string LastName { get; set; }

        [DataMember(Name = "FirstNameEng", EmitDefaultValue = false)]
        public string FirstNameEng { get; set; }

        [DataMember(Name = "LastNameEng", EmitDefaultValue = false)]
        public string LastNameEng { get; set; }

        [DataMember(Name = "IsEnabled", EmitDefaultValue = false)]
        public bool IsActive { get; set; }

        public void InitializeFromOutput(ProfileExtraShortWS output)
        {
            ProfileId = output.ProfileId;
            FirstName = output.FirstName;
            LastName = output.LastName;
            FirstNameEng = output.FirstNameEng;
            LastNameEng = output.LastNameEng;
            IsActive = output.IsEnabled;
        }
    }
}