using System;
using System.Configuration;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;
using itechart.connect.api;
using itechart.connect.api.Identity.Infrastructure;
using itechart.connect.api.Identity.Providers;
using itechart.PerformanceReview.Data.Model;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace itechart.connect.api
{
    public class GlobalErrorLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            var exception = context.Exception;
            // Write your custom logging code here
        }
    }

    public class Startup
    {
        private const string TokenEndpointPathKey = "TokenEndpointPath";


        public void Configuration(IAppBuilder app)
        {
            var httpConfig = new HttpConfiguration();

            ConfigureOAuthTokenGeneration(app);
            ConfigureOAuthTokenConsumption(app);

            ConfigureWebApi(httpConfig);

            app.UseCors(CorsOptions.AllowAll);

            var corsAttribute = new EnableCorsAttribute("*", "*", "*");
            httpConfig.EnableCors(corsAttribute);

            app.Use<CustomAuthenticationMiddleware>();

//            ConfigureExceptionFilters(app, httpConfig);

            app.UseWebApi(httpConfig);
        }

//        public IAppBuilder ConfigureExceptionFilters(IAppBuilder builder, HttpConfiguration config)
//        {
//            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
//            config.Services.Add(typeof(IExceptionLogger), new GlobalErrorLogger());
//
//            return builder;
//        }

        private void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(PerformanceReviewDbContext.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            var oAuthServerOptions = new OAuthAuthorizationServerOptions
            {
                //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString(ConfigurationManager.AppSettings[TokenEndpointPathKey]),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                Provider = new CustomOAuthProvider(),
                AccessTokenFormat = new CustomJwtFormat(HttpContext.Current.Request.Url.AbsoluteUri)
            };

            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
        }

        private void ConfigureOAuthTokenConsumption(IAppBuilder app)
        {
            var issuer = HttpContext.Current.Request.Url.AbsoluteUri;
            string audienceId = ConfigurationManager.AppSettings[AudienceGenerator.AudienceClientIdKey];
            byte[] audienceSecret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings[AudienceGenerator.AudienceSecretKey]);

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audienceId },
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, audienceSecret)
                    }
                });
        }
    }
}
