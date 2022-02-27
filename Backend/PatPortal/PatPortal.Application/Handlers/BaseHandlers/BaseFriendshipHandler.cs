using MediatR;
using PatPortal.Domain.Entities.Users;
using PatPortal.Domain.Exceptions;
using PatPortal.Domain.Services.Interfaces;
using PatPortal.SharedKernel.Extensions;

namespace PatPortal.Application.Handlers.BaseHandlers
{
    public abstract class BaseFriendshipHandler<T> : BaseHandler ,IRequestHandler<T> where T : IRequest
    {
        protected readonly IUserService _userService;
        protected readonly IFriendshipService _friendshipService;

        public BaseFriendshipHandler(
            IUserService userService,
            IFriendshipService friendshipService)
        {
            _userService = userService;
            _friendshipService = friendshipService;
        }
        public abstract Task<Unit> Handle(T request, CancellationToken cancellationToken);
        protected async Task<Unit> RunAsync(Func<User, User, Task> actionAsync, string userId, string friendId)
        {
            var userGuid = GetGuidOrThrow("user", userId);
            var friendGuid = GetGuidOrThrow("user's friend", friendId);

            var friendTask = _userService.GetAsync(friendGuid);
            var userTask = _userService.GetAsync(userGuid);

            await Task.WhenAll(friendTask, userTask);
            await actionAsync(userTask.Result, friendTask.Result);

            return Unit.Value;
        }
    }
}
