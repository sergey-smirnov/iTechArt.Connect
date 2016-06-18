using System.Runtime.Serialization;

namespace itechart.PerformanceReview.Data.Filters.Queries
{
    [DataContract]
    public class BaseQuery<TQueryField>
    {
        [DataMember(Name = "field")]
        public TQueryField Field { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}