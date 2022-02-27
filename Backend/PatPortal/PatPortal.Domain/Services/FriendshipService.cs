using PatPortal.Domain.Entities.Friendships;
using PatPortal.Domain.Entities.Users;
using PatPortal.Domain.Exceptions;
using PatPortal.Domain.Repositories.Interfaces;
using PatPortal.Domain.Services.Interfaces;

namespace PatPortal.Domain.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly IFriendshipRepository _friendshipRepository;

        public FriendshipService(IFriendshipRepository friendshipRepository)
        {
            _friendshipRepository = friendshipRepository;
        }
        public async Task AcceptInvitationAsync(User user, User friend)
        {
            var friendshipFromUserTask =  _friendshipRepository.GetByUserAsync(user.Id);
            var friendshipFromFriendTask =  _friendshipRepository.GetByUserAsync(friend.Id);

            await Task.WhenAll(friendshipFromFriendTask, friendshipFromUserTask);

            var friendshipFromUser = friendshipFromUserTask.Result.FirstOrDefault(frs => frs.Friend.Id == friend.Id);
            var friendshipFromFriend = friendshipFromFriendTask.Result.FirstOrDefault(frs => frs.Friend.Id == user.Id);

            if (friendshipFromFriend == default || friendshipFromUser == default)
                throw new EntityNotFoundException("Invitation not found.");

            if (friendshipFromUser.UserInvited)
                throw new DomainValidationException("User cannot accept own invitation");

            friendshipFromUser.AcceptInvitation();
            friendshipFromFriend.AcceptInvitation();

            await Task.WhenAll(_friendshipRepository.UpdateAsync(friendshipFromUser),
                                _friendshipRepository.UpdateAsync(friendshipFromFriend));
        }

        public async Task<Guid> AddFriendAsync(User user, User friend)
        {
            if (user.Id == friend.Id)
                throw new DomainValidationException("Unable to add user to own freidns.");
            var friendships = await _friendshipRepository.GetByUserAsync(user.Id);

            if (friendships.Any(frs => frs.Friend.Id == friend.Id))
                throw new DomainValidationException("Invitation has already been sent.");

            var userInvitation = new Friendship(Guid.NewGuid(), user, friend, false, true);
            var friendInvitation = new Friendship(Guid.NewGuid(), friend, user, false, false);

            var addForUserTask = _friendshipRepository.AddAsync(userInvitation);
            var addForFriendTask = _friendshipRepository.AddAsync(friendInvitation);

            await Task.WhenAll(addForUserTask, addForFriendTask);

            return addForFriendTask.Result;
        }

        public async Task CancelIntivationAsync(User user, User friend)
        {
            var userFriendshipsTask =  _friendshipRepository.GetByUserAsync(user.Id);
            var friendFriendshipsTask = _friendshipRepository.GetByUserAsync(friend.Id);

            await Task.WhenAll(userFriendshipsTask, friendFriendshipsTask);

            var toDeleteUsers = userFriendshipsTask.Result.FirstOrDefault(frs => frs.Friend.Id == friend.Id);
            var toDeleteFriend = friendFriendshipsTask.Result.FirstOrDefault(frs => frs.Friend.Id == user.Id);

            if (toDeleteFriend != default)
                await _friendshipRepository.DeleteAsync(toDeleteFriend);

            if (toDeleteFriend != default)
                await _friendshipRepository.DeleteAsync(toDeleteUsers);
        }
    }
}
