using Autofac;
using Autofac.Integration.Mvc;
using log4net;
using log4net.Config;
using System;
using System.Web.Mvc;
using Walltage.Domain;
using Walltage.Service;

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

            // Register Services
            builder.RegisterType<AccountService>().As<IAccountService>().InstancePerLifetimeScope();
            builder.RegisterType<HomeService>().As<IHomeService>().InstancePerLifetimeScope();
            builder.RegisterType<SettingService>().As<ISettingService>().InstancePerLifetimeScope();

            // Register Controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Register Action Filters
            builder.RegisterFilterProvider();

            _container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));
        }
    }
}