using MediatR;

namespace PatPortal.Application.Contracts.Commands.Friendships
{
    public class CancelInvitationOrRemoveFriendCommand : IRequest
    {
        public string Id { get; }
        public string FriendId { get;}
        public CancelInvitationOrRemoveFriendCommand(string id, string friendId)
        {
            Id = id;
            FriendId = friendId;
        }

    }
}
