using PrjManager.Infrastructure.Logging;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace PrjManager.Services.ActionFilters
{
    public class ExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        readonly ILogger _logger;

        public ExceptionHandlerAttribute() : this(new TextFileLogger())
        {
        }
        public ExceptionHandlerAttribute(ILogger logger)
        {
            _logger = logger;
        }
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception;
            var guid = _logger.Error(exception);
            var details = string.Empty;
            var message = exception.Message;
#if DEBUG
            details = exception.ToString();
#else
            details = string.Format("There was a problem in processing your request, please contact support team with this number {0}", guid);
            message="";
#endif
            var response = new
            {
                Message = message,
                ReferenceId= guid,
                Details = details
            };

            var responseMessage = actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError, response);
            throw new HttpResponseException(responseMessage);
        }
    }
}