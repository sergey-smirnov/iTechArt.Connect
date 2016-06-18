using System.Runtime.Serialization;
using itechart.connect.api.Model.Extended;
using itechart.connect.api.Services;
using itechart.PerformanceReview.Data.Model;

namespace itechart.connect.api.Model.Dto
{
    [DataContract]
    public class UserDto
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "smgId")]
        public int SmgId { get; set; }

        [DataMember(Name = "login")]
        public string Login { get; set; }

        [DataMember(Name = "profile")]
        public UserProfileDto Profile { get; set; }

        [DataMember(Name = "department")]
        public DepartmentDto Department { get; set; }

        public static UserDto CreateFromModel(User model)
        {
            var department = new DepartmentExtended(model.Department,
                DepartmentsService.Instance.IsDepartmentLocked(model.Department));
            return new UserDto
            {
                Id = model.Id,
                SmgId = model.SmgUserId,
                Login = model.UserName,
                Profile = UserProfileDto.CreateFromModel(model.UserProfile),
                Department = DepartmentDto.CreateFromModel(department)
            };
        }
    }
}