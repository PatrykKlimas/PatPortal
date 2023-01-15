using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PatPortal.Database;
using PatPortal.Domain.Repositories.Interfaces;
using PatPortal.Infrastructure.Factories;
using PatPortal.Infrastructure.Repositories;
using PatPortal.Infrastructure.Repositories.Mock;
using PatPortal.SharedKernel.Database;

namespace PatPortal.Infrastructure.Configuration
{
    public class InfrastructureModule : Module
    {
        private readonly ApplicationConfiguration _settings;

        public InfrastructureModule(ApplicationConfiguration settings)
        {
            _settings = settings;
        }
        protected override void Load(ContainerBuilder builder)
        {
            LoadRepositories(builder);
            LoadDatabase(builder);
            LoadFactories(builder);
        }

        private void LoadFactories(ContainerBuilder builder)
        {
            builder.RegisterType<UserMapper>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

        private void LoadDatabase(ContainerBuilder builder)
        {
            var contextBuilder = new DbContextOptionsBuilder<PatPortalDbContext>();
            contextBuilder.UseSqlServer(_settings.ConnectionStrings.PatPortalDataBase, builder =>
            {
                builder.EnableRetryOnFailure(
                    maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
            });
            contextBuilder.EnableSensitiveDataLogging();

            builder.Register(_ => contextBuilder.Options)
                .As<DbContextOptions<PatPortalDbContext>>()
                .InstancePerLifetimeScope();

            //builder.Register(provider => new PatPortalDbContext(contextBuilder.Options))
            //    .InstancePerLifetimeScope();

            builder.RegisterType<ContextProvider<PatPortalDbContext>>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

        private void LoadRepositories(ContainerBuilder builder)
        {
            if (_settings.UseMockRepositories)
            {
                builder.RegisterType<UserMockRepository>()
                    .As<IUserRepository>()
                    .SingleInstance();

                builder.RegisterType<FriendshipMockRepository>()
                    .As<IFriendshipRepository>()
                    .SingleInstance();

                builder.RegisterType<CommentMockRepository>()
                    .As<ICommentRepository>()
                    .SingleInstance();

                builder.RegisterType<PostMockRepository>()
                    .As<IPostRepository>()
                    .SingleInstance();
            }
            else
            {
                builder.RegisterType<UserRepository>()
                    .As<IUserRepository>()
                    .InstancePerLifetimeScope();

                builder.RegisterType<FriendshipRepository>()
                    .As<IFriendshipRepository>()
                    .InstancePerLifetimeScope();

                builder.RegisterType<CommentRepository>()
                    .As<ICommentRepository>()
                    .InstancePerLifetimeScope();

                builder.RegisterType<PostRepository>()
                    .As<IPostRepository>()
                    .InstancePerLifetimeScope();
            }
        }
    }
}
