using System;
using System.Linq;
using System.Runtime.Serialization;
using itechart.PerformanceReview.Data.Filters.Enum;
using itechart.PerformanceReview.Data.Filters.Queries;
using itechart.PerformanceReview.Data.Model;

namespace itechart.connect.api.Model.Dto
{
    [DataContract]
    public class UserMatchDto
    {
        [DataMember(Name = "smgId")]
        public int? SmgId { get; set; }

        [DataMember(Name = "nameEng")]
        public string NameEng { get; set; }

        [DataMember(Name = "firstNameEng")]
        public string FirstNameEng { get; set; }

        [DataMember(Name = "lastNameEng")]
        public string LastNameEng { get; set; }

        [DataMember(Name = "position")]
        public string Position { get; set; }

        [DataMember(Name = "room")]
        public string Room { get; set; }

        [DataMember(Name = "departmentName")]
        public string DepartmentName { get; set; }

        [DataMember(Name = "departmentCode")]
        public string DepartmentCode { get; set; }

        public static UserMatchDto CreateFromModel(User user, UserQuery[] queries)
        {
            var userMatch = new UserMatchDto();


            foreach (var query in queries.Where(x => !String.IsNullOrEmpty(x.Value)))
            {
                switch (query.Field)
                {
                    case UserQueryFields.SmgId:
                        userMatch.SmgId = user.SmgUserId;
                        break;
                    case UserQueryFields.FirstNameEng:
                        userMatch.FirstNameEng = user.UserProfile.FirstNameEng;
                        break;
                    case UserQueryFields.LastNameEng:
                        userMatch.LastNameEng = user.UserProfile.LastNameEng;
                        break;
                    case UserQueryFields.NameEng:
                        userMatch.NameEng = String.Format("{0} {1}", user.UserProfile.FirstNameEng, user.UserProfile.LastNameEng);
                        break;
                    case UserQueryFields.Room:
                        userMatch.Room = user.UserProfile.Room;
                        break;
                    case UserQueryFields.Position:
                        userMatch.Position = user.UserProfile.Position;
                        break;
                    case UserQueryFields.DepartmentName:
                        userMatch.DepartmentName = user.Department.Name;
                        break;
                    case UserQueryFields.DepartmentCode:
                        userMatch.DepartmentCode = user.Department.DepartmentCode;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return userMatch;
        }
    }
}