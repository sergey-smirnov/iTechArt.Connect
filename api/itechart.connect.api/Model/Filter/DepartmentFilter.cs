using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using itechart.PerformanceReview.Data.Filters;
using itechart.PerformanceReview.Data.Filters.Enum;
using itechart.PerformanceReview.Data.Filters.Queries;
using itechart.PerformanceReview.Data.Model;

namespace itechart.connect.api.Model.Filter
{
    [DataContract]
    public class DepartmentFilter : PagedFilter<Department, DepartmentSortFields>
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "code")]
        public string Code { get; set; }

        public override Expression<Func<Department, bool>> ToPredicateExpression()
        {
            if (String.IsNullOrEmpty(Name) && String.IsNullOrEmpty(Code))
            {
                return x => true;
            }

            return x => (String.IsNullOrEmpty(Name) || x.Name.ToLower().Contains(Name))
                        && (String.IsNullOrEmpty(Code) || x.DepartmentCode.ToLower().Contains(Code));
        }

        public static DepartmentFilter CreateFromDepartmentQuery(DepartmentQuery query, DepartmentFilter existedFilter = null)
        {
            var filter = existedFilter ?? new DepartmentFilter();

            switch (query.Field)
            {
                case DepartmentQueryFields.Code:
                    filter.Code = query.Value;
                    break;
                case DepartmentQueryFields.Name:
                    filter.Name = query.Value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return filter;
        }
    }
}