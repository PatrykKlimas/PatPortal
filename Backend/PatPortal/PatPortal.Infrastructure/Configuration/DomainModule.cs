using Autofac;
using PatPortal.Domain.Filters;
using PatPortal.Domain.Services;
using PatPortal.Domain.Validators.Comments;
using PatPortal.Domain.Validators.Factories;
using PatPortal.Domain.Validators.Posts;
using PatPortal.Domain.Validators.Users;
using PatPortal.Infrastructure.Repositories.Filters;

namespace PatPortal.Infrastructure.Configuration
{
    public class DomainModule : Module
    {
        private readonly ApplicationConfiguration _settings;

        public DomainModule(ApplicationConfiguration settings)
        {
            _settings = settings;
        }
        protected override void Load(ContainerBuilder builder)
        {
            LoadServices(builder);
            LoadFactories(builder);
            LoadValidator(builder);
            LoadFilters(builder);
        }

        private void LoadFilters(ContainerBuilder builder)
        {
            builder.RegisterType<UserFiltersFactory>()
                .As<IUserFilters>()
                .InstancePerLifetimeScope();
        }

        private void LoadValidator(ContainerBuilder builder)
        {
            builder.RegisterType<PostValidator>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<CommentValidator>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<CreateUserValidator>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<UpdateUserValidator>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

        private void LoadServices(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<FriendshipService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<PostService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<CommentService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
        private void LoadFactories(ContainerBuilder builder)
        {
            builder.RegisterType<UserValidatorFaactory>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

    }
}
