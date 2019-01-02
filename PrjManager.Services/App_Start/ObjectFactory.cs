using StructureMap;
using System;
using System.Threading;

namespace PrjManager.Services.App_Start
{
    public static class ObjectFactory
    {
        static readonly Lazy<Container> ContainerBuilder = new Lazy<Container>(DefaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        public static IContainer Container
        {
            get { return ContainerBuilder.Value; }

        }
        private static Container DefaultContainer()
        {
            return new Container();
        }
    }
}