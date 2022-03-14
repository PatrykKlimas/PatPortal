using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Identity.Domain.Entities.Response
{
    public class UserCredentials
    {
        public UserCredentials(Guid userId, string token)
        {
            UserId = userId;
            Token = token;
        }
        public Guid UserId { get; }
        public string Token { get; }

    }
}
