using System.Runtime.Serialization;
using itechart.PerformanceReview.Data.Filters.Enum;

namespace itechart.PerformanceReview.Data.Filters.Queries
{
    [DataContract]
    public class UserQuery : BaseQuery<UserQueryFields>
    {
    }
}