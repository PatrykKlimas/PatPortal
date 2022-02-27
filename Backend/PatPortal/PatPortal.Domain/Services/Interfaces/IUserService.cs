using PatPortal.Domain.Entities.Users;
using PatPortal.Domain.Entities.Users.Requests;

namespace PatPortal.Domain.Services.Interfaces
{
    public interface IUserService
    {
        Task<Guid> CreateAsync(UserCreate userToCreate);
        Task UpdateAsync(UserUpdate userToUpdate);
        Task<User> GetAsync(Guid userId);
        Task<IEnumerable<User>> GetFriendsAsync(Guid userId);
        Task<IEnumerable<User>> GetInvitationsAsync(Guid userId);
        Task<IEnumerable<User>> GetSendInvitationsAsync(Guid userId);
    }
}
