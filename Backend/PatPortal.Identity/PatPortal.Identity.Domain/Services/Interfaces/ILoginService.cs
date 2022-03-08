using PatPortal.Identity.Domain.Entities.Request;

namespace PatPortal.Identity.Domain.Services.Interfaces
{
    public interface ILoginService
    {
        Task<string> Login(UserLogin userLogin);
    }
}
