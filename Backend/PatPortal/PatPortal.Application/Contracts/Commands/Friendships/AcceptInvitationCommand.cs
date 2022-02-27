using MediatR;

namespace PatPortal.Application.Contracts.Commands.Friendships
{
    public class AcceptInvitationCommand : IRequest
    {
        public string UserId { get; }
        public string FriendId { get; }
        public AcceptInvitationCommand(string userId, string friendId)
        {
            UserId = userId;
            FriendId = friendId;
        }

    }
}
