using PatPortal.Domain.Entities.Friendships;
using PatPortal.Domain.Exceptions;
using PatPortal.Domain.Repositories.Interfaces;

namespace PatPortal.Infrastructure.Repositories.Mock
{
    public class FriendshipMockRepository : IFriendshipRepository
    {

        private IEnumerable<Friendship> _friendships;
        public FriendshipMockRepository()
        {
            _friendships = MockDataProvider.MockFriendships();
        }

        public async Task<Guid> AddAsync(Friendship friendship)
        {
            var friendships = _friendships.ToList();
            friendships.Add(friendship);
            _friendships = friendships;

            var createdFriendship = _friendships.Where(frs => frs.Id == friendship.Id).FirstOrDefault();
            return await Task.FromResult(createdFriendship.Id);
        }

        public Task DeleteAsync(Friendship friendship)
        {
            var newFriendships = _friendships.Where(frs => frs.Id != friendship.Id).ToList();
            _friendships = newFriendships;
            return Task.FromResult(newFriendships);
        }

        public async Task<IEnumerable<Friendship>> GetByFriendAsync(Guid friendId)
        {
            var friendships = _friendships.Where(frs => frs.Friend.Id == friendId);
            return await Task.FromResult(friendships);
        }

        public async Task<IEnumerable<Friendship>> GetByUserAsync(Guid userId)
        {
            var friendships = _friendships.Where(frs => frs.User.Id == userId);
            return await Task.FromResult(friendships);
        }
        public async Task<IEnumerable<Friendship>> GetByUserAsync(Guid userId, bool invitationAccepted)
        {
            var friendships = _friendships.Where(frs => frs.User.Id == userId && 
                                                 frs.InviteAccepted == invitationAccepted);
            return await Task.FromResult(friendships);
        }

        public async Task<IEnumerable<Friendship>> GetByUserAsync(Guid userId, bool invitationAccepted, bool userInvitation)
        {
            var friendships = _friendships.Where(frs => frs.User.Id == userId && 
                                                 frs.InviteAccepted == invitationAccepted && 
                                                 frs.UserInvited == userInvitation);
            return await Task.FromResult(friendships);
        }

        public async Task<Friendship> UpdateAsync(Friendship friendship)
        {
            var friendshipTarget = _friendships.FirstOrDefault(frs => frs.Id == friendship.Id);
            if (friendship == default)
                throw new EntityNotFoundException($"Friendship with id: {friendship.Id} not found.");

            friendshipTarget = friendship;
            return await Task.FromResult(friendshipTarget);
        }

        public async Task<Friendship> GetByUserAndFriendOrDefault(Guid userId, Guid friendId)
        {
            var freindship = _friendships.FirstOrDefault(frs => frs.User.Id == userId && frs.Friend.Id == friendId);
            if (freindship == default) return default;

            return await Task.FromResult(freindship);
        }
    }
}
