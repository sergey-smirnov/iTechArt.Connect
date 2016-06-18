using System.Runtime.Serialization;
using itechart.PerformanceReview.Data.Model;

namespace itechart.connect.api.Model.Dto
{
    [DataContract]
    public class UserProfileShortDto
    {
        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        [DataMember(Name = "firstNameEng")]
        public string FirstNameEng { get; set; }

        [DataMember(Name = "lastNameEng")]
        public string LastNameEng { get; set; }

        [DataMember(Name = "position")]
        public string Position { get; set; }

        [DataMember(Name = "photoUrl")]
        public string PhotoUrl { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }


        public static UserProfileShortDto CreateFromModel(UserProfile model)
        {
            if (model == null)
            {
                return null;
            }

            return new UserProfileShortDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                FirstNameEng = model.FirstNameEng,
                LastNameEng = model.LastNameEng,
                Position = model.Position,
                PhotoUrl = model.PhotoUrl,
                Email = model.Email
            };
        }
    }
}