using PatPortal.Identity.Domain.Entities;
using PatPortal.Identity.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Identity.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<User> AddAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByEmailOrDefaultsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByGlobalIdOrDefaultsync(Guid globalId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByUserNameOrDefaultAsync(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
