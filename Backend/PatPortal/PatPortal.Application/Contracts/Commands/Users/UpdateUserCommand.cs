using MediatR;
using PatPortal.Application.DTOs.Request.Users;

namespace PatPortal.Application.Contracts.Commands.Users
{
    public class UpdateUserCommand : IRequest
    {
        public string Id { get; set; }
        public UserForUpdateDto User { get; }
        public UpdateUserCommand(string id ,UserForUpdateDto user)
        {
            Id = id;
            User = user;
        }
    }
}
