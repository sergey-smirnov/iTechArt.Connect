using System;
using System.Runtime.Serialization;
using itechart.PerformanceReview.Data.Model;

namespace itechart.connect.api.Model.Dto
{
    [DataContract]
    public class UserProfileDto : UserProfileShortDto
    {

        [DataMember(Name = "middleName")]
        public string MiddleName { get; set; }

        [DataMember(Name = "room")]
        public string Room { get; set; }

        [DataMember(Name = "birthday")]
        public DateTime Birthday { get; set; }

        [DataMember(Name = "skypeId")]
        public string SkypeId { get; set; }

        [DataMember(Name = "phoneNumber")]
        public string PhoneNumber { get; set; }

        [DataMember(Name = "domainName")]
        public string DomainName { get; set; }


        public new static UserProfileDto CreateFromModel(UserProfile model)
        {
            if (model == null)
            {
                return null;
            }

            return new UserProfileDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                FirstNameEng = model.FirstNameEng,
                LastNameEng = model.LastNameEng,
                Position = model.Position,
                Room = model.Room,
                PhotoUrl = model.PhotoUrl,
                Birthday = model.Birthday,
                SkypeId = model.SkypeId,
                PhoneNumber = model.PhoneNumber,
                DomainName = model.DomainName,
                Email = model.Email
            };
        }
    }
}