using MediatR;
using PatPortal.Application.DTOs.Request.Users;

namespace PatPortal.Application.Contracts.Commands.Users
{
    public class CreateUserCommand : IRequest<string>
    {
        public UserForCreationDto User { get; }
        public CreateUserCommand(UserForCreationDto user)
        {
            User = user;
        }
    }
}
