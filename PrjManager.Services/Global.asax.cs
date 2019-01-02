using PrjManager.Repositories;
using PrjManager.Services;
using PrjManager.Services.App_Start;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PrjManager.Services
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            Bootstrapper.Configure();
            var configuration = GlobalConfiguration.Configuration;
            configuration.Services.Replace(typeof(IHttpControllerActivator), new HttpControllerActivatorFactory(configuration));

            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Database.SetInitializer<ProjectManagerDbContext>(null);

        }
    }
}
