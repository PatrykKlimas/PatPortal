using FluentValidation;
using NSubstitute;
using NUnit.Framework;
using PatPortal.Domain.Entities.Friendships;
using PatPortal.Domain.Entities.Users;
using PatPortal.Domain.Entities.Users.Requests;
using PatPortal.Domain.Exceptions;
using PatPortal.Domain.Filters;
using PatPortal.Domain.Repositories.Interfaces;
using PatPortal.Domain.Services;
using PatPortal.Domain.Services.Interfaces;
using PatPortal.Domain.Validators.Factories;
using PatPortal.Domain.Validators.Factories.Interfaces;
using PatPortal.Domain.Validators.Users;
using PatPortal.Domain.ValueObjects;
using PatPortal.Infrastructure.Repositories.Filters;
using PatPortal.Infrastructure.Repositories.Mock;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PatPortal.Unit.Tests.Domain.Services
{
    public class UserServiceTests
    {
        private IUserService _userService;
        private IUserRepository _userRepository;
        private IUserValidatorFactory _userValidatorFactory;
        private IFriendshipRepository _friendshipRepository;
        private IValidator<UserCreate> _userCreateValidator;
        private IValidator<UserUpdate> _userUpdateValidator;
        private IEnumerable<Friendship> _friendships;
        private IEnumerable<User> _users;
        private IUserFilters _userFilters;

        private readonly string _id = "b20b88cd-90a0-4594-b67f-232cbd984a07";
        [SetUp]
        public void SetUp()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _friendshipRepository = Substitute.For<IFriendshipRepository>();
            _userCreateValidator = new CreateUserValidator();
            _userUpdateValidator = new UpdateUserValidator();

            _friendships = MockDataProvider.MockFriendships();
            _users = MockDataProvider.MockUsers();

            _userFilters = new UserFiltersFactory();

            _userValidatorFactory = new UserValidatorFaactory(_userCreateValidator, _userUpdateValidator);
            _userService = new UserService(_userRepository, _userValidatorFactory, _friendshipRepository, _userFilters);
        }

        #region Create User

        [Test]
        [TestCase("Patryk", "Kajtek", "xxxxx", "1/1/1994", "Invalid e-mail address.")]
        [TestCase("", "Kajtek", "xxxxx", "1/1/1994", "Name should be provided with at least 2 and no more than 40 characters.")]
        [TestCase("Patryk", "", "xxxxx", "1/1/1994", "Last Name should be provided with at least 2 and no more than 40 characters.")]
        [TestCase("P", "Kajtek", "xxxxx@gmail.com", "1/1/1994", "Name should be provided with at least 2 and no more than 40 characters.")]
        [TestCase("IiiiiIiiiiIiiiiIiiiiIiiiiIiiiiIiiiiIiiiiIiiiiIiiiiIiiiiIiiiiIiiii",
            "Kajtek", "xxxxx@gmail.com", "1/1/1994", "Name should be provided with at least 2 and no more than 40 characters.")]
        [TestCase("Patryk", "K", "xxxxx@gmail.com", "1/1/1994", "Last Name should be provided with at least 2 and no more than 40 characters.")]
        [TestCase("Patryk", "IiiiiIiiiiIiiiiIiiiiIiiiiIiiiiIiiiiIiiiiIiiiiIiiiiIiiiiIiiiiIiiiiIiiii",
            "xxxxx@gmail.com", "1/1/1994", "Last Name should be provided with at least 2 and no more than 40 characters.")]
        [TestCase("Patryk", "Kaj", "xxxxx@gmail.com", "1/1/1894", "Invalid date provided.")]
        [TestCase("Patryk", "Kaj", "xxxxx@gmail.com", "1/1/2300", "Invalid date provided.")]
        public async Task CreateUserWithInvalidDataThrowsError(
            string firstName,
            string lastName,
            string email,
            string dayOfBirth,
            string errMsg)
        {
            //Arrange
            var userCreate = new UserCreate(Guid.Parse(_id), firstName, lastName, new Email(email), DateTime.Parse(dayOfBirth));

            //Act & Assert
            var ex = Assert.ThrowsAsync<CustomValidationnException>(async () => await _userService.CreateAsync(userCreate));
            Assert.True(ex.Message.Contains(errMsg));
        }

        [Test]
        [TestCase("Patryk", "Coder", "pat@gmail.com", "1/1/1993", "User with email")]
        public async Task CreateExistingEmailThrowsError(
            string firstName,
            string lastName,
            string email,
            string dayOfBirth,
            string errMsg)
        {
            //Arrange
            _userRepository.GetAsync(
                Arg.Is<IDictionary<string, string>>(filter => filter.Values.All(value => value == email)))
                .Returns(
                    new List<User> {
                        new User(
                            Guid.Parse(_id), 
                            firstName, 
                            lastName, 
                            new Email(email), "" ,DateTime.Parse(dayOfBirth), 
                            new byte[] { })
                    }
            );

            var userCreate = new UserCreate(Guid.Parse(_id), firstName, lastName, new Email(email), DateTime.Parse(dayOfBirth));

            //Act & Assert
            var ex = Assert.ThrowsAsync<DomainValidationException>(async () => await _userService.CreateAsync(userCreate));
            Assert.True(ex.Message.Contains(errMsg));
        }

        [Test]
        [TestCase("Patryk", "Coder", "pat@gmail.com", "1/1/1993")]
        public async Task CreateValidUserReturnsId(
            string firstName,
            string lastName,
            string email,
            string dayOfBirth)
        {
            //Arrange
            var newUser = new User(
                 id: Guid.Parse(_id),
                 firstName: firstName,
                 lastName: lastName,
                 email: new Email(email),
                 profession: "",
                 dayOfBirht: DateTime.Parse(dayOfBirth),
                 photo: new byte[] { });
            _userRepository.AddAsync(Arg.Any<User>()).Returns(newUser);

            var userCreate = new UserCreate(Guid.Parse(_id), firstName, lastName, new Email(email), DateTime.Parse(dayOfBirth));

            //Act
            var id = await _userService.CreateAsync(userCreate);

            //Assert
            Assert.AreEqual(id, userCreate.Id);
        }
        #endregion

        #region Update user

        [Test]
        [TestCase("Patryk", "Kajtek", "xxxxx", "Teacher", "1/1/1994", @"..\..\..\Images\Photo.jpg", "Invalid e-mail address.")]
        [TestCase("", "Kajtek", "xxxxx", "Teacher", "1/1/1994", @"..\..\..\Images\Photo.jpg", "Name should be provided with at least 2 and no more than 40 characters.")]
        [TestCase("Patryk", "", "xxxxx", "Teacher", "1/1/1994", @"..\..\..\Images\Photo.jpg", "Last Name should be provided with at least 2 and no more than 40 characters.")]
        [TestCase("P", "Kajtek", "xxxxx@gmail.com", "Teacher", "1/1/1994", @"..\..\..\Images\Photo.jpg", "Name should be provided with at least 2 and no more than 40 characters.")]
        [TestCase("IiiiiIiiiiIiiiiIiiiiIiiiiIiiiiIiiiiIiiiiIiiiiIiiiiIiiiiIiiiiIiiii",
            "Kajtek", "xxxxx@gmail.com", "Teacher", "1/1/1994", @"..\..\..\Images\Photo.jpg", "Name should be provided with at least 2 and no more than 40 characters.")]
        [TestCase("Patryk", "K", "xxxxx@gmail.com", "Teacher", "1/1/1994", @"..\..\..\Images\Photo.jpg", "Last Name should be provided with at least 2 and no more than 40 characters.")]
        [TestCase("Patryk", "IiiiiIiiiiIiiiiIiiiiIiiiiIiiiiIiiiiIiiiiIiiiiIiiiiIiiiiIiiiiIiiiiIiiii",
            "xxxxx@gmail.com", "Teacher", "1/1/1994", @"..\..\..\Images\Photo.jpg", "Last Name should be provided with at least 2 and no more than 40 characters.")]
        [TestCase("Patryk", "Kaj", "xxxxx@gmail.com", "Teacher", "1/1/1894", @"..\..\..\Images\Photo.jpg", "Invalid date provided.")]
        [TestCase("Patryk", "Kaj", "xxxxx@gmail.com", "Teacher", "1/1/2300", @"..\..\..\Images\Photo.jpg", "Invalid date provided.")]
        [TestCase("Patryk", "Kajtek", "xxxxx@gmail.com", "Teacher", "1/1/1994", @"..\..\..\Images\Photo.txt", "Invalid image format.")]
        [TestCase("Patryk", "Kajtek", "xxxxx@gmail.com", "Tea", "1/1/1994", @"..\..\..\Images\Photo.gif",
                        "Profession should be provided with at least 5 and no more than 30 characters.")]
        [TestCase("Patryk", "Kajtek", "xxxxx@gmail.com", "Iiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii", "1/1/1994", @"..\..\..\Images\Photo.gif",
                        "Profession should be provided with at least 5 and no more than 30 characters.")]
        public async Task UpdateUserWithInvalidDataThrowsError(
            string firstName,
            string lastName,
            string email,
            string profession,
            string dayOfBirth,
            string imgPath,
            string errMsg)
        {
            //Arrange
            var fileBytes = !imgPath.Equals(String.Empty) ? File.ReadAllBytes(imgPath) : new byte[] { };
            var userCreate = new UserUpdate(Guid.Parse(_id), firstName, lastName, new Email(email),
                                            profession, DateTime.Parse(dayOfBirth), fileBytes);

            //Act & Assert
            var ex = Assert.ThrowsAsync<CustomValidationnException>(async () => await _userService.UpdateAsync(userCreate));
            Assert.True(ex.Message.Contains(errMsg));
        }

        [TestCase("Patryk", "Kajtek", "xxxxx@gmail.com", "Teacher", "1/1/1994", @"..\..\..\Images\Photo.jpg", "does not exist.")]
        [TestCase("Patryk", "Kajtek", "xxxxx@gmail.com", "Teacher", "1/1/1994", "", "does not exist.")]
        public async Task UpdateNotExistingUserThrowsError(
            string firstName,
            string lastName,
            string email,
            string profession,
            string dayOfBirth,
            string imgPath,
            string errMsg)
        {
            //Arrange
            var fileBytes = !imgPath.Equals(String.Empty) ? File.ReadAllBytes(imgPath) : new byte[] { };
            var userCreate = new UserUpdate(Guid.Parse(_id), firstName, lastName, new Email(email),
                                            profession, DateTime.Parse(dayOfBirth), fileBytes);

            _userRepository.GetOrDefaultAsync(Guid.Parse(_id)).Returns((User)default);

            //Act & Assert
            var ex = Assert.ThrowsAsync<EntityNotFoundException>(async () => await _userService.UpdateAsync(userCreate));
            Assert.True(ex.Message.Contains(errMsg));
        }


        [TestCase("Patryk", "Kajtek", "xxxxx@gmail.com", "Teacher", "1/1/1994", @"..\..\..\Images\Photo.jpg", "User with email:")]
        [TestCase("Patryk", "Kajtek", "xxxxx@gmail.com", "Teacher", "1/1/1994", "", "User with email:")]
        public async Task UpdateEmailForExistingInOtherUserThrowsError(
            string firstName,
            string lastName,
            string email,
            string profession,
            string dayOfBirth,
            string imgPath,
            string errMsg)
        {
            //Arrange
            var fileBytes = !imgPath.Equals(String.Empty) ? File.ReadAllBytes(imgPath) : new byte[] { };
            var userCreate = new UserUpdate(Guid.Parse(_id), firstName, lastName, new Email("pat@onet.pl"),
                                            profession, DateTime.Parse(dayOfBirth), fileBytes);
            var user = new User(Guid.Parse(_id), firstName, lastName, new Email(email),
                                            profession, DateTime.Parse(dayOfBirth), fileBytes);
            var otherUser = new User(Guid.Parse("a0c60989-e904-4b36-baac-115c0a282d99"), firstName, lastName,
                                    new Email("pat@onet.pl"), profession, DateTime.Parse(dayOfBirth), fileBytes);

            _userRepository.GetOrDefaultAsync(Guid.Parse(_id)).Returns(user);

            //Arrange
            _userRepository.GetAsync(
                Arg.Is<IDictionary<string, string>>(filter => filter.Values.All(value => value == "pat@onet.pl")))
                .Returns(new List<User> { otherUser });

            //Act & Assert
            var ex = Assert.ThrowsAsync<DomainValidationException>(async () => await _userService.UpdateAsync(userCreate));
            Assert.True(ex.Message.Contains(errMsg));
        }

        #endregion

        #region Get

        [Test]
        public async Task GetNotExistingUserThrowsError()
        {
            //Arrange
            _userRepository.GetOrDefaultAsync(Guid.Parse(_id)).Returns((User)default);

            //Act && Assert
            var ex = Assert.ThrowsAsync<EntityNotFoundException>(async () => await _userService.GetAsync(Guid.Parse(_id)));
            Assert.True(ex.Message.Contains(" not found.", StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public async Task GetExistingUserReturnsCorrectUser()
        {
            //Arrange

            _userRepository.GetOrDefaultAsync(Guid.Parse(_id)).Returns((User)default);

            //Act && Assert
            var ex = Assert.ThrowsAsync<EntityNotFoundException>(async () => await _userService.GetAsync(Guid.Parse(_id)));
            Assert.True(ex.Message.Contains(" not found.", StringComparison.OrdinalIgnoreCase));
        }

        #endregion

        #region Get Friends

        [Test]
        public async Task GetFriendsForNotExistingUserThrowsError()
        {
            //Arrange
            _userRepository.GetOrDefaultAsync(Guid.Parse(_id)).Returns((User)default);

            //Act && Assert
            var ex = Assert.ThrowsAsync<EntityNotFoundException>(async () => await _userService.GetFriendsAsync(Guid.Parse(_id)));
            Assert.True(ex.Message.Contains(" not found.", StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public async Task GetFriendsForExistingUserReturnCorrectNumberOfFriends()
        {
            //Arrange
            var user = _users.ElementAt(0);
            var friendships = new List<Friendship>()
            {
                _friendships.ElementAt(1),
                _friendships.ElementAt(3)
            };

            _userRepository.GetOrDefaultAsync(user.Id).Returns(user);
            _friendshipRepository.GetByUserAsync(user.Id, true).Returns(friendships);

            //Act

            var friends = await _userService.GetFriendsAsync(user.Id);

            //Assert
            Assert.True(friends.Count() == 2);
        }

        #endregion

        #region Get Invitations

        [Test]
        public async Task GetInvitationsForNotExistingUserThrowsError()
        {
            //Arrange
            _userRepository.GetOrDefaultAsync(Guid.Parse(_id)).Returns((User)default);

            //Act && Assert
            var ex = Assert.ThrowsAsync<EntityNotFoundException>(async () => await _userService.GetInvitationsAsync(Guid.Parse(_id)));
            Assert.True(ex.Message.Contains(" not found.", StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public async Task GetInvitationsForExistingUserReturnCorrectNumberOfFriends()
        {
            //Arrange
            var user = _users.ElementAt(0);
            var friendships = new List<Friendship>()
            {
                _friendships.ElementAt(1),
                _friendships.ElementAt(3)
            };

            _userRepository.GetOrDefaultAsync(user.Id).Returns(user);
            _friendshipRepository.GetByUserAsync(user.Id, false, false).Returns(friendships);

            //Act

            var friends = await _userService.GetInvitationsAsync(user.Id);

            //Assert
            Assert.True(friends.Count() == 2);
        }

        #endregion

        #region Get Send Invitations

        [Test]
        public async Task GetSentInvitationsForNotExistingUserThrowsError()
        {
            //Arrange
            _userRepository.GetOrDefaultAsync(Guid.Parse(_id)).Returns((User)default);

            //Act && Assert
            var ex = Assert.ThrowsAsync<EntityNotFoundException>(async () => await _userService.GetSendInvitationsAsync(Guid.Parse(_id)));
            Assert.True(ex.Message.Contains(" not found.", StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public async Task GetSentInvitationsForExistingUserReturnCorrectNumberOfFriends()
        {
            //Arrange
            var user = _users.ElementAt(0);
            var friendships = new List<Friendship>()
            {
                _friendships.ElementAt(1),
                _friendships.ElementAt(3)
            };

            _userRepository.GetOrDefaultAsync(user.Id).Returns(user);
            _friendshipRepository.GetByUserAsync(user.Id, false, true).Returns(friendships);

            //Act
            var friends = await _userService.GetSendInvitationsAsync(user.Id);

            //Assert
            Assert.True(friends.Count() == 2);
        }

        #endregion
    }
}
