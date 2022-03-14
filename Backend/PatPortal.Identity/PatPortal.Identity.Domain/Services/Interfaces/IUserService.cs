using PatPortal.Identity.Domain.Entities;
using PatPortal.Identity.Domain.Entities.Request;
using PatPortal.Identity.Domain.Entities.Response;
using System.Security.Claims;

namespace PatPortal.Identity.Domain.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserCredentials> Login(UserLogin userLogin);
        Task<User> GetAsync(ClaimsIdentity claims);
        Task<Guid> CreateAsync(UserCreate userCreate);
    }
}
