using PatPortal.Identity.Domain.Entities;
using PatPortal.Identity.Domain.Repositories;
using PatPortal.Identity.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Identity.Infrastructure.Prividers
{
    public class IdentityProvider : IIdentityProvider
    {
        private readonly ApplicationConfiguration _applicationConfiguration;

        public IdentityProvider(ApplicationConfiguration applicationConfiguration)
        {
            _applicationConfiguration = applicationConfiguration;
        }

        public string GenerateToken(User user)
        {
            throw new NotImplementedException();
        }
    }
}
