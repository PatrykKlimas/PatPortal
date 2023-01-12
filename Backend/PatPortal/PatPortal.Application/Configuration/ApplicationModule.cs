using Autofac;
using PatPortal.Application.Factories;
using PatPortal.Application.Services;

namespace PatPortal.Application.Configuration
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterServices(builder);
            RegisterFactories(builder);
        }

        private void RegisterFactories(ContainerBuilder builder)
        {
            builder.RegisterType<UserDtoFactory>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<DataAccessService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

        }
    }
}
