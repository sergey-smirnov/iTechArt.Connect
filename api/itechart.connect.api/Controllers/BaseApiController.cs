using System.Net.Http;
using System.Web.Http;
using itechart.connect.api.Identity.Infrastructure;
using itechart.PerformanceReview.Data.Model;
using itechart.PerformanceReview.Data.Repository;
using itechart.PerformanceReview.Data.Repository.Impl;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace itechart.connect.api.Controllers
{
    public class BaseApiController : ApiController
    {
        private ApplicationUserManager appUserManager;
        private ApplicationRoleManager roleManager;
        private IUnitOfWork unitOfWork;


        protected ApplicationUserManager UserManager
        {
            get
            {
                return appUserManager ?? (appUserManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>());
            }
        }

        protected ApplicationRoleManager RoleManager
        {
            get
            {
                return roleManager ?? (roleManager = Request.GetOwinContext().GetUserManager<ApplicationRoleManager>());
            }
        }

        protected IUnitOfWork UnitOfWork
        {
            get
            {
                return unitOfWork ?? (unitOfWork = new UnitOfWork(Request.GetOwinContext().Get<PerformanceReviewDbContext>()));
            }
        }


        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
