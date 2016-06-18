using System.Runtime.Serialization;
using itechart.connect.api.Model.Filter;
using itechart.PerformanceReview.Data.Filters.Queries;

namespace itechart.connect.api.Model.Binding
{
    [DataContract]
    public class DepartmentFilterBindingModel : DepartmentFilter
    {
    }

    [DataContract]
    public class DepartmentQueriesBindingModel
    {
        [DataMember(Name = "fieldsQueries")]
        public DepartmentQuery[] FieldsQueries { get; set; }
    }
}