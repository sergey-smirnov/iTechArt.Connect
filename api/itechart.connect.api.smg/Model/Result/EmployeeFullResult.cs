using System;
using System.Runtime.Serialization;
using itechart.connect.api.smg.SmgMobileService;

namespace itechart.connect.api.smg.Model.Result
{
    [DataContract]
    public class EmployeeFullResult : EmployeeShortResult, IBaseApiResult<ProfileFullWS>
    {
        [DataMember(Name = "MiddleName", EmitDefaultValue = false)]
        public string MiddleName { get; set; }

        [DataMember(Name = "Birthday", EmitDefaultValue = false)]
        public DateTime Birthday { get; set; }

        [DataMember(Name = "Skype", EmitDefaultValue = false)]
        public string SkypeId { get; set; }

        [DataMember(Name = "Phone", EmitDefaultValue = false)]
        public string PhoneNumber { get; set; }

        [DataMember(Name = "DomenName", EmitDefaultValue = false)]
        public string DomainName { get; set; }

        [DataMember(Name = "Email", EmitDefaultValue = false)]
        public string Email { get; set; }

        public void InitializeFromOutput(ProfileFullWS output)
        {
            ProfileId = output.ProfileId;
            FirstName = output.FirstName;
            LastName = output.LastName;
            FirstNameEng = output.FirstNameEng;
            LastNameEng = output.LastNameEng;
            Position = !String.IsNullOrEmpty(output.Rank) ? output.Rank.Trim() : null;
            Room = output.Room;
            DepartmentId = output.DeptId;
            PhotoUrl = output.Image;

            MiddleName = output.MiddleName;
            Birthday = output.Birthday;
            SkypeId = output.Skype;
            PhoneNumber = !String.IsNullOrEmpty(output.Phone) ? output.Phone.Replace(" ", "") : null;
            DomainName = (String.IsNullOrEmpty(output.DomenName) ? GenerateDomainName(FirstNameEng, LastNameEng) : output.DomenName).Replace(" ", "");
            Email = output.Email;
        }

        private static string GenerateDomainName(string firstname, string lastName)
        {
            return String.Format("{0}.{1}", firstname, lastName);
        }
    }
}