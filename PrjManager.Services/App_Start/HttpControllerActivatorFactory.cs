using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace PrjManager.Services.App_Start
{
    public class HttpControllerActivatorFactory : IHttpControllerActivator
    {
        public HttpControllerActivatorFactory(HttpConfiguration configuration)
        {

        }
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            if (request == null || controllerType == null) return null;
            return ObjectFactory.Container.GetInstance(controllerType) as IHttpController;
        }
    }
}