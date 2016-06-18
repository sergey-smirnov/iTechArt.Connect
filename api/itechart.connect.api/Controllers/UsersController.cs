using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using itechart.connect.api.Helpers;
using itechart.connect.api.Model.Binding;
using itechart.connect.api.Model.Dto;
using itechart.connect.api.Model.Filter;
using itechart.PerformanceReview.Data.Filters;
using itechart.PerformanceReview.Data.Model;

namespace itechart.connect.api.Controllers
{
    [Authorize]
    [RoutePrefix("api/users")]
    public class UsersController : BaseApiController
    {
        [AllowAnonymous]
        [Route("{username}/picture")]
        public async Task<IHttpActionResult> GetUserPictureByName(string username)
        {
            var user = await UserManager.FindByNameAsync(username);

            if (user != null)
            {
                return Ok(ResponseDto<string>.Create(user.UserProfile.PhotoUrl));
            }

            return Ok(ResponseDto<UserDto>.NotFound());
        }

        [Route("{username}/profile")]
        public async Task<IHttpActionResult> GetUserByName(string username)
        {
            var user = await UserManager.FindByNameAsync(username);

            if (user != null)
            {
                return Ok(ResponseDto<UserDto>.Create(user, UserDto.CreateFromModel));
            }

            return Ok(ResponseDto<UserDto>.NotFound());
        }

        [Route("total")]
        public async Task<IHttpActionResult> GetTotalUsers([FromUri] UserFilterBindingModel filter)
        {
            var count = await UserManager.Users.FilteredCountAsync(filter);

            return Ok(ResponseDto<int>.Create(count));
        }

        [Route("profile")]
        public async Task<IHttpActionResult> GetCurrentUser()
        {
            var username = SessionHelper.GetUserNameFromOwinContext();

            if (!String.IsNullOrEmpty(username))
            {
                return await GetUserByName(username);
            }

            return Ok(ResponseDto<UserDto>.NotFound());
        }

        [Route("")]
        public async Task<IHttpActionResult> GetAllUsers([FromUri] UserFilterBindingModel filter)
        {
            var users = await UserManager.FilterUsers(filter);
            var filteredCount = await UserManager.Users.FilteredCountAsync(filter);

            var result = CollectionResponseDto<UserDto, User>.Create(users, UserDto.CreateFromModel, filteredCount);
            return Ok(result);
        }

        [Route("matches")]
        public async Task<IHttpActionResult> GetAllMatches([FromUri] UserQueriesBindingModel queries)
        {
            var filter = new UserFilter();
            if (queries != null && queries.FieldsQueries != null && queries.FieldsQueries.Any())
            {
                filter = queries.FieldsQueries.Aggregate(filter,
                    (current, query) => UserFilter.CreateFromUserQuery(query, current));
            }
            else
            {
                return Ok(ResponseDto<UserMatchDto>.NotFound());
            }

            var users = await UserManager.FilterUsers(filter);

            var result = CollectionResponseDto<UserMatchDto, User>.Create(users, user => UserMatchDto.CreateFromModel(user, queries.FieldsQueries), users.Count);

            return Ok(result);
        }
    }
}
