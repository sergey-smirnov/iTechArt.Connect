using System.Runtime.Serialization;
using itechart.connect.api.smg.SmgMobileService;

namespace itechart.connect.api.smg.Model.Parameters
{
    [DataContract]
    public class AuthenticateParameters : IBaseApiParameters<PostAuthenticateInput>
    {
        [DataMember(Name = "username", EmitDefaultValue = false)]
        public string UserName { get; set; }

        [DataMember(Name = "password", EmitDefaultValue = false)]
        public string Password { get; set; }

        public PostAuthenticateInput ToInput()
        {
            return new PostAuthenticateInput
            {
                Password = Password,
                Username = UserName
            };
        }
    }
}