using System;
using System.Runtime.Serialization;

namespace itechart.connect.api.Model.Dto
{
    [DataContract]
    public class ResponseDto<TDto>
    {
        private bool isSuccess = true;

        [DataMember(Name = "success")]
        public bool IsSuccess
        {
            get { return isSuccess; }
            set { isSuccess = value; }
        }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "result")]
        public TDto Result { get; set; }

        public static ResponseDto<TDto> NotFound()
        {
            return new ResponseDto<TDto> { IsSuccess = false, Message = "Not found" };
        }

        public static ResponseDto<TDto> Create<TEntity>(TEntity entity, Func<TEntity, TDto> mapFunc)
        {
            return new ResponseDto<TDto> { Result = mapFunc(entity) };
        }
        public static ResponseDto<TDto> Create(TDto entity)
        {
            return new ResponseDto<TDto> { Result = entity };
        }

    }
}