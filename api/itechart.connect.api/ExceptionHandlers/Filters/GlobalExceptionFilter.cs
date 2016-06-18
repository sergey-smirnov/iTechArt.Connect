using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace itechart.connect.api.ExceptionHandlers.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception;

            //TODO: use nlog
            //        var logger =
            //            actionExecutedContext.ActionContext.ControllerContext.Configuration.DependencyResolver.GetService(
            //                typeof (ILogger)) as ILogger;
            // 
            //        if (logger != null)
            //        {
            //            logger.Error(exception, exception.Message);
            //        }

            const string genericErrorMessage = "An unexpected error occured";

            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(genericErrorMessage)
            };

            response.Headers.Add("X-Error", genericErrorMessage);
        }
    }
}