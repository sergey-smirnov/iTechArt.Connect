using System.Runtime.Serialization;
using itechart.connect.api.smg.SmgMobileService;

namespace itechart.connect.api.smg.Model.Result
{
    [DataContract]
    public class DepartmentResult : IBaseApiResult<DepartmentWS>
    {
        [DataMember(Name = "Id", EmitDefaultValue = false)]
        public int Id { get; set; }

        [DataMember(Name = "DepCode", EmitDefaultValue = false)]
        public string DepartmentCode { get; set; }

        [DataMember(Name = "Name", EmitDefaultValue = false)]
        public string Name { get; set; }

        [DataMember(Name = "NumOfUsers", EmitDefaultValue = false)]
        public int NumberOfUsers { get; set; }


        public void InitializeFromOutput(DepartmentWS output)
        {
            Id = output.Id;
            DepartmentCode = output.DepCode;
            Name = output.Name;
            NumberOfUsers = output.NumUsers;
        }
    }
}