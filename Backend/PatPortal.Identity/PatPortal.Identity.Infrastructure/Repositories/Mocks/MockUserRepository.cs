using PatPortal.Identity.Domain.Entities;
using PatPortal.Identity.Domain.Enums;
using PatPortal.Identity.Domain.Repositories;
using PatPortal.Identity.Domain.ValueObjects;

namespace PatPortal.Identity.Infrastructure.Repositories.Mocks
{
    public class MockUserRepository : IUserRepository
    {
        List<User> _users;

        public MockUserRepository()
        {
            //"8Pw7aDRPvN44Y5k58k9dJlW5KLIL7oxCL5Hb8UN3+dmSVRle3oN20todPlvOWzTXQzHSz8WzIC4iyVUlHB+p3W73C4d3rw=="
            _users = new List<User>(){
                new User()
            {
                Id = Guid.NewGuid(),
                UserName = "Dawid12345",
                Email = new Email("dawid@o2.pl"),
                FirstName = "Dawid",
                LastName = "Kowalski",
                Password = "H3sXtFKYnUQREERFtcuMmtQ0VtG67JLvEk9VvwOB4Oxq5+vlcn4iUhdTbE9slsTXSiydSBkQ/ktdd7BqkuRW9l8ZQCCcHA==", //"xxxyyyzzz"
                Role = Role.User
            },
                new User()
            {
                Id = Guid.NewGuid(),
                UserName = "Dawid12345",
                Email = new Email("dawid@o2.pl"),
                FirstName = "Dawid",
                LastName = "Kowalski",
                Password = "H3sXtFKYnUQREERFtcuMmtQ0VtG67JLvEk9VvwOB4Oxq5+vlcn4iUhdTbE9slsTXSiydSBkQ/ktdd7BqkuRW9l8ZQCCcHA==", //"xxxyyyzzz"
                Role = Role.Admin
            }
            };
        }

        public async Task<User> GetByEmailOrDefaultsync(string email)
        {
            var user = _users.FirstOrDefault(u => u.Email.ToString().ToLower().Equals(email.ToLower()));
            return await Task.FromResult(user);
        }

        public async Task<User> GetByUserNameOrDefaultAsync(string userName)
        {
            var user = _users.FirstOrDefault(u => u.UserName.ToLower().Equals(userName.ToLower()));
            return await Task.FromResult(user);
        }
    }
}
