using PatPortal.Identity.Domain.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Identity.Domain.Services.Interfaces
{
    public interface ILoginService
    {
        Task<string> Login(UserLogin userLogin);
    }
}
