using MediatR;
using PatPortal.Identity.Application.DTOs.Request;
using PatPortal.Identity.Application.DTOs.Response;

namespace PatPortal.Identity.Application.Contracts.Commands
{
    public class LoginCommand : IRequest<UserCredentialsDto>
    {
        public UserLoginDto UserLogin { get; }

        public LoginCommand(UserLoginDto userLogin)
        {
            UserLogin = userLogin;
        }
    }
}
