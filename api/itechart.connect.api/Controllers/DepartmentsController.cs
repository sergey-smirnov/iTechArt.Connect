using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using itechart.connect.api.Model.Binding;
using itechart.connect.api.Model.Dto;
using itechart.connect.api.Model.Extended;
using itechart.connect.api.Model.Filter;
using itechart.connect.api.Services;
using itechart.PerformanceReview.Data.Filters;

namespace itechart.connect.api.Controllers
{
    [Authorize]
    [RoutePrefix("api/departments")]
    public class DepartmentsController : BaseApiController
    {
        [Route("{code}")]
        public async Task<IHttpActionResult> GetDepartmentByCode(string code)
        {
            var codeLower = code.ToLower();
            var department = await DepartmentsService.Instance.Query().FirstOrDefaultAsync(x => x.DepartmentCode.ToLower() == codeLower);

            if (department != null)
            {
                return Ok(ResponseDto<DepartmentDto>.Create(department.ToExtended(true), DepartmentDto.CreateFromModel));
            }

            return Ok(ResponseDto<DepartmentDto>.NotFound());
        }

        [Route("")]
        public async Task<IHttpActionResult> GetAllDepartments([FromUri] DepartmentFilterBindingModel filter)
        {

            var departments = await DepartmentsService.Instance.Query().FilterQuery(filter).ToListAsync();

            var filteredCount = await DepartmentsService.Instance.Query().FilteredCountAsync(filter);

            var result = CollectionResponseDto<DepartmentDto, DepartmentExtended>.Create(departments.SelectExtended(),
                DepartmentDto.CreateFromModel, filteredCount);

            return Ok(result);
        }

        [Route("total")]
        public async Task<IHttpActionResult> GetTotalDepartments([FromUri] DepartmentFilterBindingModel filter)
        {
            var count = await DepartmentsService.Instance.Query().FilteredCountAsync(filter);

            return Ok(ResponseDto<int>.Create(count));
        }

        [Route("matches")]
        public async Task<IHttpActionResult> GetAllMatches([FromUri] DepartmentQueriesBindingModel queries)
        {
            var filter = new DepartmentFilter();
            if (queries != null && queries.FieldsQueries != null && queries.FieldsQueries.Any())
            {
                filter = queries.FieldsQueries.Aggregate(filter,
                    (current, query) => DepartmentFilter.CreateFromDepartmentQuery(query, current));
            }
            else
            {
                return Ok(ResponseDto<DepartmentMatchDto>.NotFound());
            }

            var departments = await DepartmentsService.Instance.Query().FilterQuery(filter).ToListAsync();

            var result = CollectionResponseDto<DepartmentMatchDto, DepartmentExtended>.Create(departments.SelectExtended(),
                user => DepartmentMatchDto.CreateFromModel(user, queries.FieldsQueries), departments.Count);

            return Ok(result);
        }
    }
}
