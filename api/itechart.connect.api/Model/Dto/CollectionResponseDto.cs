using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace itechart.connect.api.Model.Dto
{
    [DataContract]
    public class CollectionResponseDto<TDto, TEntity> : ResponseDto<CollectionResult<TDto, TEntity>>
    {
        public static CollectionResponseDto<TDto, TEntity> Create(IEnumerable<TEntity> entities, Func<TEntity, TDto> mapFunc, int totalItems)
        {
            return new CollectionResponseDto<TDto, TEntity>
            {
                Result = CollectionResult<TDto, TEntity>.Create(entities, mapFunc, totalItems)
            };
        }
    }

    [DataContract]
    public class CollectionResult<TDto, TEntity>
    {
        [DataMember(Name = "total")]
        public int Total { get; set; }

        [DataMember(Name = "items")]
        public IEnumerable<TDto> Items { get; set; }

        public static CollectionResult<TDto, TEntity> Create(IEnumerable<TEntity> entities, Func<TEntity, TDto> mapFunc, int totalItems)
        {
            return new CollectionResult<TDto, TEntity>
            {
                Total = totalItems,
                Items = entities.Select(mapFunc)
            };
        }
    }
}