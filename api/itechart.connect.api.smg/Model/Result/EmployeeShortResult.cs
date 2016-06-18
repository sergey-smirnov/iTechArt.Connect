using System.Runtime.Serialization;
using itechart.connect.api.smg.SmgMobileService;

namespace itechart.connect.api.smg.Model.Result
{
    [DataContract]
    public class EmployeeShortResult : EmployeeExtraShortResult, IBaseApiResult<ProfileShortWS>
    {
        [DataMember(Name = "Position", EmitDefaultValue = false)]
        public string Position { get; set; }

        [DataMember(Name = "Room", EmitDefaultValue = false)]
        public string Room { get; set; }

        [DataMember(Name = "DepId", EmitDefaultValue = false)]
        public int DepartmentId { get; set; }

        [DataMember(Name = "Image", EmitDefaultValue = false)]
        public string PhotoUrl { get; set; }

        public void InitializeFromOutput(ProfileShortWS output)
        {
            ProfileId = output.ProfileId;
            FirstName = output.FirstName;
            LastName = output.LastName;
            FirstNameEng = output.FirstNameEng;
            LastNameEng = output.LastNameEng;
            IsActive = output.IsEnabled;

            Position = output.Position;
            Room = output.Room;
            DepartmentId = output.DeptId;
            PhotoUrl = output.Image;
        }
    }
}