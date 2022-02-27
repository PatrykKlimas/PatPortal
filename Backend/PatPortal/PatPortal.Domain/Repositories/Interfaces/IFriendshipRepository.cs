using PatPortal.Domain.Entities.Friendships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Domain.Repositories.Interfaces
{
    public interface IFriendshipRepository
    {
        Task<IEnumerable<Friendship>> GetByUserAsync(Guid userId);
        Task<IEnumerable<Friendship>> GetByUserAsync(Guid userId, bool invitationAccepted);
        Task<IEnumerable<Friendship>> GetByUserAsync(Guid userId, bool invitationAccepted, bool userInvitation);
        Task<IEnumerable<Friendship>> GetByFriendAsync(Guid userId);
        Task<Friendship> GetByUserAndFriendOrDefault(Guid userId, Guid friendId);
        Task<Guid> AddAsync(Friendship friendship);
        Task<Friendship> UpdateAsync(Friendship friendship);
        Task DeleteAsync(Friendship friendship);
    }
}
