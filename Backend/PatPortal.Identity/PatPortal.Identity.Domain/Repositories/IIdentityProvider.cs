using PatPortal.Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Identity.Domain.Repositories
{
    public interface IIdentityProvider
    {
        string GenerateToken(User user);
    }
}
