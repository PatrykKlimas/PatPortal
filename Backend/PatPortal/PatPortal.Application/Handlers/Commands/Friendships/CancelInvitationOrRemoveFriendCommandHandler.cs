using MediatR;
using PatPortal.Application.Contracts.Commands.Friendships;
using PatPortal.Application.Handlers.BaseHandlers;
using PatPortal.Domain.Services.Interfaces;

namespace PatPortal.Application.Handlers.Commands.Friendships
{
    public class CancelInvitationOrRemoveFriendCommandHandler : BaseFriendshipHandler<CancelInvitationOrRemoveFriendCommand>
    {
        public CancelInvitationOrRemoveFriendCommandHandler(
            IUserService userService, 
            IFriendshipService friendshipService) : base(userService, friendshipService)
        {
        }

        public override async Task<Unit> Handle(CancelInvitationOrRemoveFriendCommand request, CancellationToken cancellationToken)
        {
            return await RunAsync(_friendshipService.CancelIntivationAsync, request.Id, request.FriendId);
        }
    }
}
