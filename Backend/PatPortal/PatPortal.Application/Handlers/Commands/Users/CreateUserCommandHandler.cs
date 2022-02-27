using AutoMapper;
using MediatR;
using PatPortal.Application.Contracts.Commands.Users;
using PatPortal.Domain.Entities.Users;
using PatPortal.Domain.Exceptions;
using PatPortal.Domain.Services.Interfaces;
using PatPortal.SharedKernel.Extensions;

namespace PatPortal.Application.Handlers.Commands.Users
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public CreateUserCommandHandler(
            IMapper mapper, 
            IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userDto = request.User;
            if (!userDto.DayOfBirht.ParsebleToDateTime())
                throw new InitValidationException($"Invalid data format: {userDto.DayOfBirht}");

            var user = _mapper.Map<UserCreate>(userDto);
            var id = await _userService.CreateAsync(user);

            return id.ToString();
        }
    }
}
