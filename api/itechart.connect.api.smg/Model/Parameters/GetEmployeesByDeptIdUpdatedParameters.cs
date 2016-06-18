using System;
using System.Runtime.Serialization;

namespace itechart.connect.api.smg.Model.Parameters
{
    [DataContract]
    public class GetEmployeesByDeptIdUpdatedParameters : GetEmployeesByDeptIdParameters
    {
        [DataMember(Name = "updatedDate", EmitDefaultValue = false)]
        public DateTime UpdatedDate { get; set; }

        [DataMember(Name = "initialRequest", EmitDefaultValue = false)]
        public bool OnlyActive { get; set; }

        public GetEmployeesByDeptIdUpdatedParameters(int sessionId) : base(sessionId)
        {
        }

        public GetEmployeesByDeptIdUpdatedParameters()
        {
        }
    }
}