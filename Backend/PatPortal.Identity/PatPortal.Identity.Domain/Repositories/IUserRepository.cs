using PatPortal.Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Identity.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByUserNameOrDefaultAsync(string userName);
        Task<User> GetByEmailOrDefaultsync(string userName);
    }
}
