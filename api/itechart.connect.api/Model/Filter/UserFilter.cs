using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using itechart.PerformanceReview.Data.Filters;
using itechart.PerformanceReview.Data.Filters.Enum;
using itechart.PerformanceReview.Data.Filters.Queries;
using itechart.PerformanceReview.Data.Model;

namespace itechart.connect.api.Model.Filter
{
    [DataContract]
    public class UserFilter : PagedFilter<User, UserSortFields>
    {
        [DataMember(Name = "smgId")]
        public string SmgId { get; set; }

        [DataMember(Name = "nameEng")]
        public string NameEng { get; set; }

        [DataMember(Name = "firstNameEng")]
        public string FirstNameEng { get; set; }

        [DataMember(Name = "lastNameEng")]
        public string LastNameEng { get; set; }

        [DataMember(Name = "departmentName")]
        public string DepartmentName { get; set; }

        [DataMember(Name = "departmentCode")]
        public string DepartmentCode { get; set; }

        [DataMember(Name = "room")]
        public string Room { get; set; }

        [DataMember(Name = "position")]
        public string Position { get; set; }

        [DataMember(Name = "queries")]
        public UserQuery[] Queries { get; set; }

        public override Expression<Func<User, bool>> ToPredicateExpression()
        {
            if (String.IsNullOrEmpty(SmgId) && String.IsNullOrEmpty(NameEng) && String.IsNullOrEmpty(FirstNameEng) &&
                String.IsNullOrEmpty(LastNameEng) && String.IsNullOrEmpty(DepartmentName) &&
                String.IsNullOrEmpty(DepartmentCode) && String.IsNullOrEmpty(Room) && String.IsNullOrEmpty(Position))
            {
                return user => true;
            }

            return user => (String.IsNullOrEmpty(SmgId) || user.SmgUserId.ToString().Contains(SmgId))
                && (String.IsNullOrEmpty(NameEng) || (user.UserProfile.FirstNameEng.ToLower() + " " + user.UserProfile.LastNameEng.ToLower()).Contains(NameEng.ToLower()))
                && (String.IsNullOrEmpty(FirstNameEng) || user.UserProfile.FirstNameEng.ToLower().Contains(FirstNameEng.ToLower()))
                && (String.IsNullOrEmpty(LastNameEng) || user.UserProfile.LastNameEng.ToLower().Contains(LastNameEng.ToLower()))
                && (String.IsNullOrEmpty(DepartmentName) || user.Department.Name.ToLower().Contains(DepartmentName.ToLower()))
                && (String.IsNullOrEmpty(DepartmentCode) || user.Department.DepartmentCode.ToLower().Contains(DepartmentCode.ToLower()))
                && (String.IsNullOrEmpty(Room) || user.UserProfile.Room.Contains(Room))
                && (String.IsNullOrEmpty(Position) || user.UserProfile.Position.ToLower().Contains(Position.ToLower()));
        }

        public static UserFilter CreateFromUserQuery(UserQuery query, UserFilter existedFilter = null)
        {
            var filter = existedFilter ?? new UserFilter();

            switch (query.Field)
            {
                case UserQueryFields.SmgId:
                    filter.SmgId = query.Value;
                    break;
                case UserQueryFields.NameEng:
                    filter.NameEng = query.Value;
                    break;
                case UserQueryFields.FirstNameEng:
                    filter.FirstNameEng = query.Value;
                    break;
                case UserQueryFields.LastNameEng:
                    filter.LastNameEng = query.Value;
                    break;
                case UserQueryFields.Room:
                    filter.Room = query.Value;
                    break;
                case UserQueryFields.Position:
                    filter.Position = query.Value;
                    break;
                case UserQueryFields.DepartmentName:
                    filter.DepartmentName = query.Value;
                    break;
                case UserQueryFields.DepartmentCode:
                    filter.DepartmentCode = query.Value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return filter;
        }
    }
}