using PatPortal.Domain.Entities.Users;

namespace PatPortal.Domain.Services.Interfaces
{
    public interface IFriendshipService
    {
        Task<Guid> AddFriendAsync(User user, User friend);
        Task AcceptInvitationAsync(User user, User friend);
        Task CancelIntivationAsync(User user, User friend);
    }
}
