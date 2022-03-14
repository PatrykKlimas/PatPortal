using AutoMapper;
using MediatR;
using PatPortal.Identity.Application.Contracts.Commands;
using PatPortal.Identity.Application.DTOs.Response;
using PatPortal.Identity.Domain.Entities.Request;
using PatPortal.Identity.Domain.Services.Interfaces;

namespace PatPortal.Identity.Application.Hnadlers.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, UserCredentialsDto>
    {
        private readonly IUserService _loginService;
        private readonly IMapper _mapper;

        public LoginCommandHandler(
            IUserService loginService, 
            IMapper mapper)
        {
            _loginService = loginService;
            _mapper = mapper;
        }

        async Task<UserCredentialsDto> IRequestHandler<LoginCommand, UserCredentialsDto>.Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var credentials = await _loginService.Login(_mapper.Map<UserLogin>(request.UserLogin));
            return _mapper.Map<UserCredentialsDto>(credentials);
        }
    }
}
