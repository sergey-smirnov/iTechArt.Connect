using System.Runtime.Serialization;

namespace itechart.connect.api.smg.Model.Parameters
{
    [DataContract]
    public class GetEmployeeDetailsParameters : BaseSmgParameters
    {
        [DataMember(Name = "profileId", EmitDefaultValue = false)]
        public int ProfileId { get; set; }

        public GetEmployeeDetailsParameters(int sessionId) : base(sessionId)
        {
        }

        public GetEmployeeDetailsParameters()
        {
        }
    }
}