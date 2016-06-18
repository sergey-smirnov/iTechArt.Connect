using System.Runtime.Serialization;

namespace itechart.connect.api.smg.Model.Parameters
{
    [DataContract]
    public class GetAllEmployeesParameters : BaseSmgParameters
    {
        public GetAllEmployeesParameters(int sessionId) : base(sessionId)
        {
        }

        protected GetAllEmployeesParameters()
        {
        }
    }
}