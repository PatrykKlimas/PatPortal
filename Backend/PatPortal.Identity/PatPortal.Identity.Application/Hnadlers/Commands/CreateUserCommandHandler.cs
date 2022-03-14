using AutoMapper;
using MediatR;
using PatPortal.Identity.Application.Contracts.Commands;
using PatPortal.Identity.Domain.Entities.Request;
using PatPortal.Identity.Domain.Exceptions;
using PatPortal.Identity.Domain.Services.Interfaces;

namespace PatPortal.Identity.Application.Hnadlers.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(
            IUserService userService, 
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.UserCreate;

            Guid globalId;
            var parsableToGuid = Guid.TryParse(user.GlobalId, out globalId);

            if (!parsableToGuid)
                throw new InitValidationException($"{user.GlobalId} is invalid guid.");

            var createdUserId = await _userService.CreateAsync(_mapper.Map<UserCreate>(user));

            return createdUserId.ToString();
        }
    }
}
