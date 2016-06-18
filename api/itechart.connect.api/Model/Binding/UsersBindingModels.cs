using System.Runtime.Serialization;
using itechart.connect.api.Model.Filter;
using itechart.PerformanceReview.Data.Filters.Queries;

namespace itechart.connect.api.Model.Binding
{
    [DataContract]
    public class UserFilterBindingModel : UserFilter
    {
        [DataMember(Name = "fullProfile")]
        public bool FullProfile { get; set; }
    }

    [DataContract]
    public class UserQueriesBindingModel
    {
        [DataMember(Name = "fieldsQueries")]
        public UserQuery[] FieldsQueries { get; set; }
    }
}