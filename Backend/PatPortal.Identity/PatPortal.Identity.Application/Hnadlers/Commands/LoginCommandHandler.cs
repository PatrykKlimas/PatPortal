using MediatR;
using PatPortal.Identity.Application.Contracts.Commands;
using PatPortal.Identity.Domain.Entities.Request;
using PatPortal.Identity.Domain.Services.Interfaces;

namespace PatPortal.Identity.Application.Hnadlers.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly ILoginService _loginService;

        public LoginCommandHandler(ILoginService loginService)
        {
            _loginService = loginService;
        }
        Task<string> IRequestHandler<LoginCommand, string>.Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var userLogin = new UserLogin()
            {
                Password = request.UserLogin.Password,
                UserName = request.UserLogin.UserName
            };

            return _loginService.Login(userLogin);
        }
    }
}
