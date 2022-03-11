using AutoMapper;
using MediatR;
using PatPortal.Identity.Application.Contracts.Commands;
using PatPortal.Identity.Domain.Entities.Request;
using PatPortal.Identity.Domain.Services.Interfaces;

namespace PatPortal.Identity.Application.Hnadlers.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly ILoginService _loginService;
        private readonly IMapper _mapper;

        public LoginCommandHandler(
            ILoginService loginService, 
            IMapper mapper)
        {
            _loginService = loginService;
            _mapper = mapper;
        }

        Task<string> IRequestHandler<LoginCommand, string>.Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return _loginService.Login(_mapper.Map<UserLogin>(request.UserLogin));
        }
    }
}
