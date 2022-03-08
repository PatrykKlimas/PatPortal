using MediatR;
using PatPortal.Identity.Application.DTOs.Request;

namespace PatPortal.Identity.Application.Contracts.Commands
{
    public class LoginCommand : IRequest<string>
    {
        public UserLoginDto UserLogin { get; }

        public LoginCommand(UserLoginDto userLogin)
        {
            UserLogin = userLogin;
        }
    }
}
