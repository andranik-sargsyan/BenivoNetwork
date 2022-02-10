using BenivoNetwork.BLL.Services;
using BenivoNetwork.DAL.Interfaces;
using BenivoNetwork.DAL.Repositories;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

namespace BenivoNetwork.Configuration
{
    public class SimpleInjectorConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            container.Options.ResolveUnregisteredConcreteTypes = true;

            // Register your types, for instance:
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);
            RegisterServices(container);

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterWebApiControllers(configuration);
            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        private static void RegisterServices(Container container)
        {
            var serviceTypes = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(t => t.GetTypes())
                .Where(t => t.IsClass && t.Namespace == "BenivoNetwork.BLL.Services");

            foreach (var serviceType in serviceTypes)
            {
                var interfaceType = serviceType.GetInterface($"I{serviceType.Name}");

                if (interfaceType != null)
                {
                    container.Register(interfaceType, serviceType, Lifestyle.Scoped);
                }
            }
        }
    }
}
