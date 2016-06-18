using System.Runtime.Serialization;
using itechart.connect.api.smg.Model.Struct;
using itechart.connect.api.smg.SmgMobileService;

namespace itechart.connect.api.smg.Model.Result
{
    [DataContract]
    public abstract class BaseApiResult<TOutput> : IBaseApiResult<TOutput>
        where TOutput : BaseResponse
    {
        [DataMember(Name = "Permission", EmitDefaultValue = false)]
        public UserPermission Permission { get; set; }

        [DataMember(Name = "ErrorCode", EmitDefaultValue = false)]
        public ErrorCode ErrorCode { get; set; }

        public virtual void InitializeFromOutput(TOutput output)
        {
            ErrorCode = ErrorCode.FromString(output.ErrorCode);
            Permission = UserPermission.FromString(output.Permission);
        }
    }
}