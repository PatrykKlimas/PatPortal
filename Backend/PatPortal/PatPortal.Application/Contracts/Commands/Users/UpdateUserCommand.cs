using MediatR;
using PatPortal.Application.DTOs.Request.Users;

namespace PatPortal.Application.Contracts.Commands.Users
{
    public class UpdateUserCommand : IRequest
    {
        public UserForUpdateDto User { get; }
        public UpdateUserCommand(UserForUpdateDto user)
        {
            User = user;
        }
    }
}
