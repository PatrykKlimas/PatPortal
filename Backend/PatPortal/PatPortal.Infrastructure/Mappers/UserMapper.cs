using PatPortal.Domain.ValueObjects;
using PatPortal.Infrastructure.Factories.Interfaces;
using UserDb = PatPortal.Database.Models.User;
using UserEntity = PatPortal.Domain.Entities.Users.User;

namespace PatPortal.Infrastructure.Factories
{
    internal class UserMapper : IUserMapper
    {
        public UserDb Create(UserEntity user)
        {
            return new UserDb()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email.ToString(),
                Profession = user.Profession,
                DayOfBirth = user.DayOfBirht,
                Photo = user.Photo,
                IsDeleted = false,
            };
        }

        public UserEntity Create(UserDb user)
        {
            return new UserEntity(
                user.Id, 
                user.FirstName, 
                user.LastName,
                new Email(user.Email), 
                user.Profession, 
                user.DayOfBirth, 
                user.Photo);
        }
    }
}
