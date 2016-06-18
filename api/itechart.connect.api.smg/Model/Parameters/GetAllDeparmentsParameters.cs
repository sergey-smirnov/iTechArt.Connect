using System.Runtime.Serialization;

namespace itechart.connect.api.smg.Model.Parameters
{
    [DataContract]
    public class GetAllDeparmentsParameters : BaseSmgParameters
    {
        public GetAllDeparmentsParameters(int sessionId) : base(sessionId)
        {
        }

        public GetAllDeparmentsParameters()
        {
        }
    }
}