using MediatR;
using PatPortal.Application.Contracts.Commands.Friendships;
using PatPortal.Application.Handlers.BaseHandlers;
using PatPortal.Domain.Services.Interfaces;

namespace PatPortal.Application.Handlers.Commands.Friendships
{
    public class AddFriendCommandHandler : BaseFriendshipHandler<AddFriendCommand>
    {

        public AddFriendCommandHandler(
            IUserService userService,
            IFriendshipService friendshipService) : base(userService, friendshipService)
        {
        }

        public override async Task<Unit> Handle(AddFriendCommand request, CancellationToken cancellationToken)
        {
            return await RunAsync(_friendshipService.AddFriendAsync, request.Id, request.FriendId);
        }
    }
}
