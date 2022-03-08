using PatPortal.Identity.Domain.Entities;
using PatPortal.Identity.Domain.Enums;
using PatPortal.Identity.Domain.Repositories;
using PatPortal.Identity.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Identity.Infrastructure.Repositories.Mocks
{
    public class MockUserRepository : IUserRepository
    {
        List<User> _users;

        public MockUserRepository()
        {
            _users = new List<User>(){
                new User()
            {
                Id = Guid.NewGuid(),
                UserName = "Dawid12345",
                Email = new Email("dawid@o2.pl"),
                FirstName = "Dawid",
                LastName = "Kowalski",
                Password = "xxxyyyzzz",
                Role = Role.User
            },
                new User()
            {
                Id = Guid.NewGuid(),
                UserName = "Dawid12345",
                Email = new Email("dawid@o2.pl"),
                FirstName = "Dawid",
                LastName = "Kowalski",
                Password = "xxxyyyzzz",
                Role = Role.Admin
            }
            };
        }
        public Task<User> GetByEmailOrDefaultsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByUserNameOrDefaultAsync(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
