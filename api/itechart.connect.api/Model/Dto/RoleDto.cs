using System.Runtime.Serialization;
using itechart.PerformanceReview.Data.Model;

namespace itechart.connect.api.Model.Dto
{
    [DataContract]
    public class RoleDto
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        public static RoleDto CreateFromModel(Role model)
        {
            return new RoleDto
            {
                Id = model.Id,
                Name = model.Name
            };
        }
    }
}