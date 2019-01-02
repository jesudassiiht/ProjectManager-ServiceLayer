using PrjManager.Infrastructure.Logging;
using PrjManager.Repositories;

namespace PrjManager.Services.App_Start
{
    public class Bootstrapper
    {
        public static void Configure()
        {
            ObjectFactory.Container.Configure(x =>
            {
                x.AddRegistry<ServicesRegistry>();
                x.For<ILogger>().Use<TextFileLogger>().Singleton();
            });

            var log = ObjectFactory.Container.WhatDoIHave();
        }
    }
    public class ServicesRegistry : StructureMap.Registry
    {
        public ServicesRegistry()
        {
            Scan(x =>
            {
                x.Assembly("PrjManager.Business");
                x.Assembly("PrjManager.Infrastructure");
                x.Assembly("PrjManager.Repositories");
                x.Assembly("PrjManager.Services");
                x.WithDefaultConventions();
            });

            For(typeof(IRepository<>)).Use(typeof(Repository<>));
        }
    }
}