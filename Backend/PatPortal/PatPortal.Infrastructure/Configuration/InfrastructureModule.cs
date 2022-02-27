using Autofac;
using PatPortal.Domain.Repositories.Interfaces;
using PatPortal.Infrastructure.Repositories;
using PatPortal.Infrastructure.Repositories.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    .As<IUserRepository>()
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
