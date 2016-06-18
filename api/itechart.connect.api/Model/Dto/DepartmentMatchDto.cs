using System;
using System.Linq;
using System.Runtime.Serialization;
using itechart.connect.api.Model.Extended;
using itechart.PerformanceReview.Data.Filters.Enum;
using itechart.PerformanceReview.Data.Filters.Queries;

namespace itechart.connect.api.Model.Dto
{
    [DataContract]
    public class DepartmentMatchDto
    {
        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        public static DepartmentMatchDto CreateFromModel(DepartmentExtended model, DepartmentQuery[] queries)
        {
            var match = new DepartmentMatchDto();

            foreach (var query in queries.Where(x => !String.IsNullOrEmpty(x.Value)))
            {
                switch (query.Field)
                {
                    case DepartmentQueryFields.Name:
                        match.Name = model.Name;
                        break;
                    case DepartmentQueryFields.Code:
                        match.Code = model.DepartmentCode;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return match;
        }
    }
}