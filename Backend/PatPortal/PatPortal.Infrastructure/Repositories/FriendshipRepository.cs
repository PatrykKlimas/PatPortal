using PatPortal.Domain.Entities.Friendships;
using PatPortal.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Infrastructure.Repositories
{
    public class FriendshipRepository : IFriendshipRepository
    {
        public Task<Guid> AddAsync(Friendship friendship)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Friendship friendship)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Friendship>> GetByFriendAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<Friendship> GetByUserAndFriendOrDefault(Guid userId, Guid friendId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Friendship>> GetByUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Friendship>> GetByUserAsync(Guid userId, bool invitationAccepted)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Friendship>> GetByUserAsync(Guid userId, bool invitationAccepted, bool userInvitation)
        {
            throw new NotImplementedException();
        }

        public Task<Friendship> UpdateAsync(Friendship friendship)
        {
            throw new NotImplementedException();
        }
    }
}
