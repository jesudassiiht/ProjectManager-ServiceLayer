using PrjManager.Services.ActionFilters;
using PrjManager.Services.MessageHandlers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PrjManager.Services
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //var cors = new EnableCorsAttribute("*", "*", "*");
            //config.EnableCors(cors);
            
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Filters.Add(new ExceptionHandlerAttribute());
            config.MessageHandlers.Add(new RequestResponseMessageHandler());
        }
    }
}
