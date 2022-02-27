using MediatR;
using PatPortal.Application.Contracts.Commands.Friendships;
using PatPortal.Application.Handlers.BaseHandlers;
using PatPortal.Domain.Services.Interfaces;

namespace PatPortal.Application.Handlers.Commands.Friendships
{
    public class AcceptInvitationCommandHandler : BaseFriendshipHandler<AcceptInvitationCommand>
    {

        public AcceptInvitationCommandHandler(
            IFriendshipService friendshipService,
            IUserService userService) : base(userService, friendshipService)
        {
        }

        public override async Task<Unit> Handle(AcceptInvitationCommand request, CancellationToken cancellationToken)
        {
            return await RunAsync(_friendshipService.AcceptInvitationAsync, request.UserId, request.FriendId);
        }
    }
}
