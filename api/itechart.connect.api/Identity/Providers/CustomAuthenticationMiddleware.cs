using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace itechart.connect.api.Identity.Providers
{
    public class CustomAuthenticationMiddleware : OwinMiddleware
    {
        public CustomAuthenticationMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            await Next.Invoke(context);

            if (context.Response.StatusCode == (int)HttpStatusCode.BadRequest && context.Response.Headers.ContainsKey(CustomOAuthProvider.OwinChallengeFlag))
            {
                var headerValues = context.Response.Headers.GetValues(CustomOAuthProvider.OwinChallengeFlag);

                context.Response.StatusCode = Convert.ToInt16(headerValues.FirstOrDefault());

                context.Response.Headers.Remove(CustomOAuthProvider.OwinChallengeFlag);
            }

        }
    }
}