using MediatR;

namespace PatPortal.Identity.Application.Contracts.Commands
{
    public class DeleteUserCommand : IRequest
    {
        public string UserId { get; }
        public DeleteUserCommand(string userId)
        {
            UserId = userId;
        }
    }
}
