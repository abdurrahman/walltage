using Autofac;
using Autofac.Integration.Mvc;
using log4net;
using log4net.Config;
using System;
using System.Reflection;
using System.Web.Mvc;
using Walltage.Domain;
using Walltage.Service.Helpers;
using Walltage.Service.Services;

namespace Walltage.Web
{
    public static class DependencyContainer
    {
        private static IContainer _container;
        private static ILog _logger;

        public static void Initialize()
        {
            if (_container != null)
                throw new InvalidOperationException("Dependency Container is already initialized.");

            var builder = new ContainerBuilder();

            // Register logger service
            _logger = LogManager.GetLogger("Walltage");
            XmlConfigurator.Configure();
            builder.RegisterInstance(_logger).As<ILog>().SingleInstance();

            // Register Dependencies
            builder.RegisterType<WalltageDbContext>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            //builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            // Register Wrappers
            builder.RegisterAssemblyTypes(Assembly.Load("Walltage.Service"))
                .Where(t => t.Name.EndsWith("Wrapper"))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            // Register Services
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<WallpaperService>().As<IWallpaperService>().InstancePerLifetimeScope();
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();

            builder.RegisterModule(new AutofacWebTypesModule());

            //builder.RegisterAssemblyTypes(Assembly.Load("Walltage.Service"))
            //    .Where(t => t.Name.EndsWith("Service"))
            //    .AsImplementedInterfaces()
            //    .InstancePerRequest();

            // Register Controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();

            // Register Action Filters
            builder.RegisterFilterProvider();

            _container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));
        }
    }
}