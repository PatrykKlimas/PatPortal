using PatPortal.Domain.Entities.Users;
using PatPortal.Domain.Repositories.Interfaces;
using PatPortal.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Infrastructure.Repositories.Mock
{
    public class UserRepository : IUserRepository
    {
        public Task<User?> AddAsync(User user)
        {
            //TODO Implement after register Entity Framework
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            //TODO Implement after register Entity Framework
            throw new NotImplementedException();
        }

        public Task<User> GetOrDefaultAsync(Guid Id)
        {
            //TODO Implement after register Entity Framework
            throw new NotImplementedException();
        }

        public Task<User> GetOrDefaultByEmailAsync(Email email)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateAsync(User user)
        {
            //TODO Implement after register Entity Framework
            throw new NotImplementedException();
        }
    }
}
