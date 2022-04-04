using UserDb = PatPortal.Database.Models.User;
using UserEntity = PatPortal.Domain.Entities.Users.User;

namespace PatPortal.Infrastructure.Factories.Interfaces
{
    internal interface IUserFactory
    {
        public UserDb Create(UserEntity user);
        public UserEntity Create(UserDb user);
    }
}
