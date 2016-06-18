using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace itechart.connect.api.ExceptionHandlers.Handlers
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public async override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            // Access Exception
            // var exception = context.Exception;

            const string genericErrorMessage = "An unexpected error occured&quot";
            var response = context.Request.CreateResponse(HttpStatusCode.InternalServerError,
                new
                {
                    Message = genericErrorMessage
                });

            response.Headers.Add("X-Error", genericErrorMessage);
            context.Result = new ResponseMessageResult(response);
        }
    }
}