using Autofac;
using log4net;
using log4net.Config;
using System;
using System.Data.Entity;
using Walltage.Domain;
using Walltage.Service;

namespace Walltage.Web.Tests
{
    public static class DependencyContainer
    {
        private static IContainer _container;
        private static ILog _logger;

        public static IContainer Initialize()
        {
            if (_container != null)
                throw new InvalidOperationException("Dependency Container is already initialized.");

            var builder = new ContainerBuilder();

            // Register logger service
            _logger = LogManager.GetLogger("Walltage");
            XmlConfigurator.Configure();
            builder.RegisterInstance(_logger).As<ILog>().SingleInstance();

            // Register Dependencies
            builder.RegisterType<WalltageDbContext>().As(typeof(DbContext)).InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            // Register Services
            builder.RegisterType<AccountService>().As<IAccountService>().InstancePerLifetimeScope();
            builder.RegisterType<HomeService>().As<IHomeService>().InstancePerLifetimeScope();
            builder.RegisterType<SettingService>().As<ISettingService>().InstancePerLifetimeScope();

            _container = builder.Build();

            return _container;
        }
    }
}
