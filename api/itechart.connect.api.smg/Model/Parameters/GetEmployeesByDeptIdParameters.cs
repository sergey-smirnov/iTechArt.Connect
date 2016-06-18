using System.Runtime.Serialization;

namespace itechart.connect.api.smg.Model.Parameters
{
    [DataContract]
    public class GetEmployeesByDeptIdParameters : GetAllEmployeesParameters
    {
        [DataMember(Name = "departmentId", EmitDefaultValue = false)]
        public int DepartmentId { get; set; }

        public GetEmployeesByDeptIdParameters(int sessionId) : base(sessionId)
        {
        }

        protected GetEmployeesByDeptIdParameters()
        {
        }
    }
}