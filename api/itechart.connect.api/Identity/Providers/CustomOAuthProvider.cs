using System.Net;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using itechart.connect.api.Identity.Extensions;
using itechart.connect.api.Identity.Infrastructure;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace itechart.connect.api.Identity.Providers
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        public const string OwinChallengeFlag = "X-Challenge";

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }  
        

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            const string allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            try
            {
                var user = await userManager.FindAsync(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                }
                else
                {
                    ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, "JWT");

                    var ticket = new AuthenticationTicket(oAuthIdentity, null);

                    context.Validated(ticket);
                }
            }
            catch (InvalidCredentialException)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");

                context.OwinContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.OwinContext.Response.Headers.Add(OwinChallengeFlag, new[] { ((int)HttpStatusCode.Unauthorized).ToString() });

//                context.Response.Headers.Add(OwinChallengeFlag, new[] { ((int)HttpStatusCode.Unauthorized).ToString() });
                //                context.Rejected();

                //TODO: sign out user
                //                throw;
                //var responseObject = new BaseReturnModel();

                //                context.SetError("invalid_grant", "The user name or password is incorrect.");
                //                context.Rejected();
            }

        }
    }
}