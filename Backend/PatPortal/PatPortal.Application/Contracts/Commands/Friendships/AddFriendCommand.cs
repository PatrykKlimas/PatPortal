using MediatR;

namespace PatPortal.Application.Contracts.Commands.Friendships
{
    public class AddFriendCommand : IRequest
    {
        public string Id { get; }
        public string FriendId { get; }
        public AddFriendCommand(string id, string friendId)
        {
            Id = id;
            FriendId = friendId;
        }
    }
}
