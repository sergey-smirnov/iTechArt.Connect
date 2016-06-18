using System.Runtime.Serialization;

namespace itechart.connect.api.smg.Model.Parameters
{
    [DataContract]
    public class BaseSmgParameters
    {
        [DataMember(Name = "sessionId", EmitDefaultValue = false)]
        public string SessionId { get; set; }

        public BaseSmgParameters(int sessionId)
        {
            SessionId = sessionId.ToString();
        }
        public BaseSmgParameters()
        {
        }
    }
}