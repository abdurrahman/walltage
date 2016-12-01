using Autofac;
using log4net;
using log4net.Config;
using System;
using Walltage.Domain;
using Walltage.Service.Helpers;
using Walltage.Service.Services;

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
            builder.RegisterType<WalltageDbContext>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            // Register Services
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<WallpaperService>().As<IWallpaperService>().InstancePerLifetimeScope();
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();

            _container = builder.Build();

            return _container;
        }
    }
}
