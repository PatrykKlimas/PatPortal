using NSubstitute;
using NUnit.Framework;
using PatPortal.Domain.Entities.Friendships;
using PatPortal.Domain.Entities.Users;
using PatPortal.Domain.Exceptions;
using PatPortal.Domain.Repositories.Interfaces;
using PatPortal.Domain.Services;
using PatPortal.Domain.Services.Interfaces;
using PatPortal.Infrastructure.Repositories.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatPortal.Unit.Tests.Domain.Services
{
    public class FriendshipServiceTests
    {
        private IFriendshipService _friendshipService;
        private IFriendshipRepository _friendshipRepository;
        private IEnumerable<User> _users;
        private IEnumerable<Friendship> _friendships;

        [SetUp]
        public void SetUp()
        {
            _users = MockDataProvider.MockUsers();
            _friendships = MockDataProvider.MockFriendships();
            _friendshipRepository = Substitute.For<IFriendshipRepository>();
            _friendshipService = new FriendshipService(_friendshipRepository);
        }

        #region Accept invitation

        [Test]
        public async Task AcceptInvitationForNotExistingUserThrowsError()
        {
            //Arrange
            var user = _users.First();

            //Act & Assert
            var ex = Assert.ThrowsAsync<DomainValidationException>(() => _friendshipService.AcceptInvitationAsync((User)default, user));
            Assert.True(ex.Message.Contains("User or friend not found", StringComparison.OrdinalIgnoreCase));

            ex = Assert.ThrowsAsync<DomainValidationException>(() => _friendshipService.AcceptInvitationAsync(user, (User)default));
            Assert.True(ex.Message.Contains("User or friend not found", StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public async Task AcceptNotExistingInvitationThrowsError()
        {
            //Arrange
            _friendshipRepository.GetByUserAndFriendOrDefault(Arg.Any<Guid>(), Arg.Any<Guid>()).Returns((Friendship)default);

            //Act & Assert
            var ex = Assert.ThrowsAsync<EntityNotFoundException>(() => _friendshipService.AcceptInvitationAsync(_users.ElementAt(0), _users.ElementAt(1)));
            Assert.True(ex.Message.Contains("Invitation not found.", StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public async Task AcceptUserOwnInvitationThrowsError()
        {
            var user = _users.ElementAt(0);
            var frriend = _users.ElementAt(2);

            //Assert
            _friendshipRepository.GetByUserAndFriendOrDefault(user.Id, frriend.Id).Returns(_friendships.ElementAt(2));
            _friendshipRepository.GetByUserAndFriendOrDefault(frriend.Id, user.Id).Returns(_friendships.ElementAt(3));

            //Act & Arrange
            var ex = Assert.ThrowsAsync<DomainValidationException>(() => _friendshipService.AcceptInvitationAsync(_users.ElementAt(0), _users.ElementAt(2)));
            Assert.True(ex.Message.Contains("User cannot accept own invitation.", StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public async Task AcceptnvitationVithCorrectDataCallsMethods()
        {
            var user = _users.ElementAt(2);
            var friend = _users.ElementAt(0);

            //Assert
            _friendshipRepository.GetByUserAndFriendOrDefault(user.Id, friend.Id).Returns(_friendships.ElementAt(3));
            _friendshipRepository.GetByUserAndFriendOrDefault(friend.Id, user.Id).Returns(_friendships.ElementAt(2));

            //Act
            await _friendshipService.AcceptInvitationAsync(user, friend);

            //Act & Arrange
            _friendshipRepository.Received(2).UpdateAsync(Arg.Any<Friendship>());
        }

        #endregion

        #region Add Friend

        [Test]
        public async Task AddIvitationForNotExistingUserThrowsError()
        {
            //Arrange
            var user = _users.First();

            //Act & Assert
            var ex = Assert.ThrowsAsync<DomainValidationException>(() => _friendshipService.AddFriendAsync((User)default, user));
            Assert.True(ex.Message.Contains("User or friend not found", StringComparison.OrdinalIgnoreCase));

            ex = Assert.ThrowsAsync<DomainValidationException>(() => _friendshipService.AddFriendAsync(user, (User)default));
            Assert.True(ex.Message.Contains("User or friend not found", StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public async Task AddUserToOwnFriendsThrowError()
        {
            //Arrange
            var user = _users.ElementAt(0);
            var friend = user;

            //Act & assert
            var ex = Assert.ThrowsAsync<DomainValidationException>(() => _friendshipService.AddFriendAsync(user, friend));
            Assert.True(ex.Message.Contains("Unable to add user to own friends."));
        }

        [Test]
        public async Task AddExistingFriendshipThrowError()
        {
            //Arrange
            var user = _users.ElementAt(0);
            var friend = _users.ElementAt(1);
            _friendshipRepository.GetByUserAndFriendOrDefault(user.Id, friend.Id).Returns(_friendships.ElementAt(0));

            //Act & Assert
            var ex = Assert.ThrowsAsync<DomainValidationException>(() => _friendshipService.AddFriendAsync(user, friend));
            Assert.True(ex.Message.Contains("Invitation has already been sent."));
        }

        [Test]
        public async Task AddCorrectInvitationCallsAddMethod()
        {
            //Arrange
            var user = _users.ElementAt(0);
            var friend = _users.ElementAt(1);
            _friendshipRepository.GetByUserAndFriendOrDefault(user.Id, friend.Id).Returns((Friendship)default);

            //Act
            _friendshipService.AddFriendAsync(user, friend);

            //Assert
            _friendshipRepository.Received(2).AddAsync(Arg.Any<Friendship>());
        }

        #endregion

        #region Cancel Invitation

        [Test]
        public async Task CancelIvitationForNotExistingUserThrowsError()
        {
            //Arrange
            var user = _users.First();

            //Act & Assert
            var ex = Assert.ThrowsAsync<DomainValidationException>(() => _friendshipService.CancelIntivationAsync((User)default, user));
            Assert.True(ex.Message.Contains("User or friend not found", StringComparison.OrdinalIgnoreCase));

            ex = Assert.ThrowsAsync<DomainValidationException>(() => _friendshipService.CancelIntivationAsync(user, (User)default));
            Assert.True(ex.Message.Contains("User or friend not found", StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public async Task CancelInvitationForNotExistingFriendshipThrowsError()
        {
            //Arrange 
            var user = _users.ElementAt(0);
            var friend = _users.ElementAt(1);
            
            _friendshipRepository.GetByUserAndFriendOrDefault(user.Id, friend.Id).Returns((Friendship)default);
            _friendshipRepository.GetByUserAndFriendOrDefault(friend.Id, user.Id).Returns((Friendship)default);

            //Act && assert
            var ex = Assert.ThrowsAsync<EntityNotFoundException>(() => _friendshipService.CancelIntivationAsync(user, friend));
            Assert.True(ex.Message.Contains("Friendship not found", StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public async Task CancelinvitationForExistingFriendsipCallsProperMethods()
        {
            //Arrange 
            var user = _users.ElementAt(0);
            var friend = _users.ElementAt(1);
            var userFriendship = _friendships.ElementAt(0);
            var friendFriendship = _friendships.ElementAt(1);

            _friendshipRepository.GetByUserAndFriendOrDefault(user.Id, friend.Id).Returns(userFriendship);
            _friendshipRepository.GetByUserAndFriendOrDefault(friend.Id, user.Id).Returns(friendFriendship);

            //Act
            await _friendshipService.CancelIntivationAsync(user, friend);

            //Assert
            _friendshipRepository.Received(1).GetByUserAndFriendOrDefault(friend.Id, user.Id);
            _friendshipRepository.Received(1).GetByUserAndFriendOrDefault(user.Id, friend.Id);
            _friendshipRepository.Received(1).DeleteAsync(userFriendship);
            _friendshipRepository.Received(1).DeleteAsync(friendFriendship);
        }

        #endregion
    }
}
