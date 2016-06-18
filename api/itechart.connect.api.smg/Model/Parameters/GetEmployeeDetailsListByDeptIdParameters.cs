using System.Runtime.Serialization;

namespace itechart.connect.api.smg.Model.Parameters
{
    [DataContract]
    public class GetEmployeeDetailsListByDeptIdParameters : BaseSmgParameters
    {
        [DataMember(Name = "departmentId", EmitDefaultValue = false)]
        public int DepartmentId { get; set; }

        public GetEmployeeDetailsListByDeptIdParameters(int sessionId) : base(sessionId)
        {
        }

        public GetEmployeeDetailsListByDeptIdParameters()
        {
        }
    }
}