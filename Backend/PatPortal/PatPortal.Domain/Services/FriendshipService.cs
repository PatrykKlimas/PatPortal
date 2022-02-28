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
            if (user == default || friend == default)
                throw new DomainValidationException("User or friend not found");

            var friendshipFromUserTask =  _friendshipRepository.GetByUserAndFriendOrDefault(user.Id, friend.Id);
            var friendshipFromFriendTask =  _friendshipRepository.GetByUserAndFriendOrDefault(friend.Id, user.Id);

            await Task.WhenAll(friendshipFromUserTask, friendshipFromFriendTask);

            var friendshipFromUser = friendshipFromUserTask.Result;
            var friendshipFromFriend = friendshipFromFriendTask.Result;

            if (friendshipFromFriend == default || friendshipFromUser == default)
                throw new EntityNotFoundException("Invitation not found.");

            if (friendshipFromUser.UserInvited)
                throw new DomainValidationException("User cannot accept own invitation.");

            friendshipFromUser.AcceptInvitation();
            friendshipFromFriend.AcceptInvitation();

            await Task.WhenAll(_friendshipRepository.UpdateAsync(friendshipFromUser),
                                _friendshipRepository.UpdateAsync(friendshipFromFriend));
        }

        public async Task<Guid> AddFriendAsync(User user, User friend)
        {
            if (user == default || friend == default)
                throw new DomainValidationException("User or friend not found");

            if (user.Id == friend.Id)
                throw new DomainValidationException("Unable to add user to own friends.");
            var friendships = await _friendshipRepository.GetByUserAndFriendOrDefault(user.Id, friend.Id);

            if (friendships != default)
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
            if (user == default || friend == default)
                throw new DomainValidationException("User or friend not found");

            var userFriendshipsTask =  _friendshipRepository.GetByUserAndFriendOrDefault(user.Id, friend.Id);
            var friendFriendshipsTask = _friendshipRepository.GetByUserAndFriendOrDefault(friend.Id, user.Id);

            await Task.WhenAll(userFriendshipsTask, friendFriendshipsTask);

            var toDeleteUsers = userFriendshipsTask.Result;
            var toDeleteFriend = friendFriendshipsTask.Result;

            if (toDeleteFriend == default && toDeleteFriend == default)
                throw new EntityNotFoundException("Friendship not found;");

            if (toDeleteUsers != default)
                await _friendshipRepository.DeleteAsync(toDeleteFriend);

            if (toDeleteFriend != default)
                await _friendshipRepository.DeleteAsync(toDeleteUsers);
        }
    }
}
