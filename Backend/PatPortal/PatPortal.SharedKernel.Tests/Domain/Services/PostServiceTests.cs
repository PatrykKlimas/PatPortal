using FluentValidation;
using NSubstitute;
using NUnit.Framework;
using PatPortal.Domain.Entities.Friendships;
using PatPortal.Domain.Entities.Posts;
using PatPortal.Domain.Entities.Posts.Requests;
using PatPortal.Domain.Entities.Users;
using PatPortal.Domain.Enums;
using PatPortal.Domain.Exceptions;
using PatPortal.Domain.Repositories.Interfaces;
using PatPortal.Domain.Services;
using PatPortal.Domain.Services.Interfaces;
using PatPortal.Domain.Validators.Posts;
using PatPortal.Infrastructure.Repositories.Mock;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PatPortal.Unit.Tests.Domain.Services
{
    public class PostServiceTests
    {
        private IPostService _postService;
        private IUserRepository _userRepository;
        private IPostRepository _postRepository;
        private IFriendshipRepository _friendshipRepository;
        private IUserService _userService;
        private IValidator<Post> _validator;
        private IEnumerable<User> _users;
        private IEnumerable<Post> _posts;
        private IEnumerable<Friendship> _friendships;
        private readonly string _id = "b20b88cd-90a0-4594-b67f-232cbd984a07";
        [SetUp]
        public void SetUp()
        {
            _postService = Substitute.For<IPostService>();
            _userRepository = Substitute.For<IUserRepository>();
            _postRepository = Substitute.For<IPostRepository>();
            _friendshipRepository = Substitute.For<IFriendshipRepository>();
            _userService = Substitute.For<IUserService>();
            _users = MockDataProvider.MockUsers();
            _posts = MockDataProvider.MockPosts();
            _friendships = MockDataProvider.MockFriendships();
            _validator = new PostValidator();
            _postService = new PostService(_userRepository, _validator, _postRepository, _friendshipRepository, _userService);
        }

        #region Create Post

        [Test]
        public async Task CreatePostForNoExisthingUserThrowsError()
        {
            //Arrange
            _userRepository.GetOrDefaultAsync(Guid.Parse(_id)).Returns((User)default);
            var createPost = new PostCreate(new byte[] { }, "Friends", Guid.NewGuid(), "Some content");

            //Act & Assert
            var ex = Assert.ThrowsAsync<EntityNotFoundException>(() => _postService.CreateAsync(createPost));
            Assert.True(ex.Message.Contains(" not found.", StringComparison.OrdinalIgnoreCase));
        }
        
        [Test]
        [TestCase("friend")]
        [TestCase("default")]
        [TestCase("")]
        [TestCase("xxxxx")]
        public async Task CreatePostWIthInvalidDataAccessThrowsError(string dataAccess)
        {
            //Arrange
            var user = _users.ElementAt(0);
            _userRepository.GetOrDefaultAsync(user.Id).Returns(user);
            var createPost = new PostCreate(new byte[] { }, dataAccess, Guid.NewGuid(), "Some content");

            //Act & Assert
            var ex = Assert.ThrowsAsync<CustomValidationnException>(() => _postService.CreateAsync(createPost));
            Assert.True(ex.Message.Contains("Unable to parse", StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        [TestCase(@"..\..\..\Images\Photo.jpg", "Undefined", "Some content", "Invalid date access has benn provided.")]
        [TestCase(@"..\..\..\Images\Photo.txt", "Friends", "Some content", "Invalid image format.")]
        [TestCase(@"..\..\..\Images\Photo.gif", "Private", "S", "Content length must be between 2 and 500 characters.")]
        [TestCase(@"..\..\..\Images\Photo.gif", "Private", "xxxxx", "Content length must be between 2 and 500 characters.")]
        public async Task CreatePostWithInvalidDataThrowsException(
            string imgPath, 
            string dateAccess, 
            string content,
            string errMsg)
        {
            //Arrange
            var user = _users.ElementAt(0);
            _userRepository.GetOrDefaultAsync(user.Id).Returns(user);

            content = content == "xxxxx" ? string.Concat(Enumerable.Repeat(content, 101)) : content;
            var fileBytes = !imgPath.Equals(String.Empty) ? File.ReadAllBytes(imgPath) : new byte[] { };
            var createPost = new PostCreate(fileBytes, dateAccess, user.Id, content);

            //Act && Assert
            var ex = Assert.ThrowsAsync<CustomValidationnException>(() => _postService.CreateAsync(createPost));
            Assert.True(ex.Message.Contains(errMsg, StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        [TestCase(@"..\..\..\Images\Photo.jpg", "Friends", "Some content")]
        [TestCase(@"..\..\..\Images\Photo.gif", "Private", "Some content")]
        [TestCase(@"..\..\..\Images\Photo.bmp", "Public", "Some content")]
        public async Task CreatePostWithValidDataReturnsGuid(
            string imgPath,
            string dateAccess,
            string content)
        {
            //Arrange
            var user = _users.ElementAt(0);
            _userRepository.GetOrDefaultAsync(user.Id).Returns(user);

            var post = _posts.ElementAt(0);
            _postRepository.AddAsync(Arg.Any<Post>()).Returns(post);

            var fileBytes = !imgPath.Equals(String.Empty) ? File.ReadAllBytes(imgPath) : new byte[] { };
            var createPost = new PostCreate(fileBytes, dateAccess, user.Id, content);

            //Act
            var guid = await _postService.CreateAsync(createPost);

            //Assert
            Assert.True(guid.Equals(post.Id));
        }

        #endregion

        #region Update Post
        [Test]
        public async Task UpdatePostWithNonExistingUserThrowsError()
        {
            //Arrange
            _userRepository.GetOrDefaultAsync(Guid.Parse(_id)).Returns((User)default);
            var updatePost = new PostUpdate(Guid.Parse(_id) ,new byte[] { }, "Friends", Guid.NewGuid(), "Some content");

            //Act & Assert
            var ex = Assert.ThrowsAsync<EntityNotFoundException>(() => _postService.UpdateAsync(updatePost));
            Assert.True(ex.Message.Contains(" not found.", StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public async Task UpdatePostForNotExistingPostThrowsError()
        {
            //Arrange
            _userRepository.GetOrDefaultAsync(Arg.Any<Guid>()).Returns(_users.ElementAt(0));
            _postRepository.GetOrDefaultAsync(Guid.Parse(_id)).Returns((Post)default);

            var updatePost = new PostUpdate(Guid.Parse(_id), new byte[] { }, "Friends", Guid.NewGuid(), "Some content");

            //Act & Assert
            var ex = Assert.ThrowsAsync<EntityNotFoundException>(() => _postService.UpdateAsync(updatePost));
            Assert.True(ex.Message.Contains(" not found.", StringComparison.OrdinalIgnoreCase) &&
                        ex.Message.Contains("Post with id:", StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        [TestCase("friend")]
        [TestCase("default")]
        [TestCase("")]
        [TestCase("xxxxx")]
        public async Task UpdatePostWIthInvalidDataAccessThrowsError(string dataAccess)
        {
            //Arrange
            var user = _users.ElementAt(0);
            _userRepository.GetOrDefaultAsync(user.Id).Returns(user);
            var updatePost = new PostUpdate(Guid.Parse(_id) ,new byte[] { }, dataAccess, Guid.NewGuid(), "Some content");

            //Act & Assert
            var ex = Assert.ThrowsAsync<CustomValidationnException>(() => _postService.UpdateAsync(updatePost));
            Assert.True(ex.Message.Contains("Unable to parse", StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        [TestCase(@"..\..\..\Images\Photo.jpg", "Undefined", "Some content", "Invalid date access has benn provided.")]
        [TestCase(@"..\..\..\Images\Photo.txt", "Friends", "Some content", "Invalid image format.")]
        [TestCase(@"..\..\..\Images\Photo.gif", "Private", "S", "Content length must be between 2 and 500 characters.")]
        [TestCase(@"..\..\..\Images\Photo.gif", "Private", "xxxxx", "Content length must be between 2 and 500 characters.")]
        public async Task UpdatePostWithInvalidDataThrowsException(
            string imgPath,
            string dateAccess,
            string content,
            string errMsg)
        {
            //Arrange
            var user = _users.ElementAt(0);
            _userRepository.GetOrDefaultAsync(user.Id).Returns(user);

            var post = _posts.ElementAt(0);
            _postRepository.GetOrDefaultAsync(post.Id).Returns(post);

            content = content == "xxxxx" ? string.Concat(Enumerable.Repeat(content, 101)) : content;
            var fileBytes = !imgPath.Equals(String.Empty) ? File.ReadAllBytes(imgPath) : new byte[] { };
            var updatePost = new PostUpdate(post.Id, fileBytes, dateAccess, user.Id, content);

            //Act && Assert
            var ex = Assert.ThrowsAsync<CustomValidationnException>(() => _postService.UpdateAsync(updatePost));
            Assert.True(ex.Message.Contains(errMsg, StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        [TestCase(@"..\..\..\Images\Photo.jpg", "Friends", "Some content")]
        [TestCase(@"..\..\..\Images\Photo.gif", "Private", "Some content")]
        [TestCase(@"..\..\..\Images\Photo.bmp", "Public", "Some content")]
        public async Task UpdatePostWithValidDataDoesNotThrowsError(
            string imgPath,
            string dateAccess,
            string content)
        {
            //Arrange
            var user = _users.ElementAt(0);
            _userRepository.GetOrDefaultAsync(user.Id).Returns(user);

            var post = _posts.ElementAt(0);
            _postRepository.GetOrDefaultAsync(post.Id).Returns(post);

            var fileBytes = !imgPath.Equals(String.Empty) ? File.ReadAllBytes(imgPath) : new byte[] { };
            var updatePost = new PostUpdate(post.Id, fileBytes, dateAccess, user.Id, content);

            //Act && asert
            Assert.DoesNotThrowAsync(() => _postService.UpdateAsync(updatePost));
        }

        #endregion

        #region Get By User

        [Test]
        public async Task GetPostsForNoExistingUserThrowsError()
        {
            var requestor = _users.ElementAt(0);
            //Arrange
            _userRepository.GetOrDefaultAsync(Guid.Parse(_id)).Returns((User)default);
            _userRepository.GetOrDefaultAsync(requestor.Id).Returns(requestor);

            //Act && assert
            var ex = Assert.ThrowsAsync<EntityNotFoundException>(() => _postService.GetByUserAsync(Guid.Parse(_id), requestor.Id));
            Assert.True(ex.Message.Contains("User with id:") &&
                        ex.Message.Contains("not found."));
        }

        [Test]
        public async Task GetPostsForNoExistingRequestorThrowsError()
        {
            var user = _users.ElementAt(0);
            //Arrange
            _userRepository.GetOrDefaultAsync(Guid.Parse(_id)).Returns((User)default);
            _userRepository.GetOrDefaultAsync(user.Id).Returns(user);

            //Act && assert
            var ex = Assert.ThrowsAsync<EntityNotFoundException>(() => _postService.GetByUserAsync(user.Id, Guid.Parse(_id)));
            Assert.True(ex.Message.Contains("User with id:") &&
                        ex.Message.Contains("not found."));
        }

        [Test]
        public async Task GetPostsForOwnRequestorCallsOnlyGetByOwnerAsync()
        {
            var user = _users.ElementAt(0);
            var requestor = user;
            //Arrange
            _userRepository.GetOrDefaultAsync(requestor.Id).Returns(requestor);
            _userRepository.GetOrDefaultAsync(user.Id).Returns(user);

            //Act
            await _postService.GetByUserAsync(user.Id, requestor.Id);

            //Assert
            await _postRepository.Received(1).GetByOwnerAsync(Arg.Any<Guid>());
            await _postRepository.DidNotReceive().GetPostsByOwnerAndAccess(Arg.Any<Guid>(), Arg.Any<DataAccess>(), Arg.Any<bool>());
        }

        [Test]
        public async Task GetPostsForPersonWhoIsNotFriendCallOnlyPublicPostsAsync()
        {
            var user = _users.ElementAt(0);
            var requestor = _users.ElementAt(1);
            //Arrange
            _userRepository.GetOrDefaultAsync(requestor.Id).Returns(requestor);
            _userRepository.GetOrDefaultAsync(user.Id).Returns(user);

            //Act
            await _postService.GetByUserAsync(user.Id, requestor.Id);

            //Assert
            await _postRepository.DidNotReceive().GetByOwnerAsync(Arg.Any<Guid>());
            await _postRepository.Received().GetPostsByOwnerAndAccess(user.Id, DataAccess.Public, true);
        }

        [Test]
        public async Task GetPostsForPersonWhoIsNotAcceptedFriendCallOnlyPublicPostsAsync()
        {
            var userNotAcceptedFriend = _users.ElementAt(0);
            var requestor = _users.ElementAt(2);
            //Arrange
            _userRepository.GetOrDefaultAsync(requestor.Id).Returns(requestor);
            _userRepository.GetOrDefaultAsync(userNotAcceptedFriend.Id).Returns(userNotAcceptedFriend);
            _friendshipRepository.GetByUserAndFriendOrDefault(userNotAcceptedFriend.Id, requestor.Id).Returns(_friendships.ElementAt(2));
            //Act
            await _postService.GetByUserAsync(userNotAcceptedFriend.Id, requestor.Id);

            //Assert
            await _postRepository.DidNotReceive().GetByOwnerAsync(Arg.Any<Guid>());
            await _postRepository.Received().GetPostsByOwnerAndAccess(userNotAcceptedFriend.Id, DataAccess.Public, true);
        }

        [Test]
        public async Task GetPostsForPersonWhoIsFriendCallsForNotPrivatePostsAsync()
        {
            var userNotAcceptedFriend = _users.ElementAt(0);
            var requestor = _users.ElementAt(2);
            //Arrange
            _userRepository.GetOrDefaultAsync(requestor.Id).Returns(requestor);
            _userRepository.GetOrDefaultAsync(userNotAcceptedFriend.Id).Returns(userNotAcceptedFriend);
            _friendshipRepository.GetByUserAndFriendOrDefault(userNotAcceptedFriend.Id, requestor.Id).Returns(_friendships.ElementAt(1));

            //Act
            await _postService.GetByUserAsync(userNotAcceptedFriend.Id, requestor.Id);

            //Assert
            await _postRepository.DidNotReceive().GetByOwnerAsync(Arg.Any<Guid>());
            await _postRepository.DidNotReceive().GetPostsByOwnerAndAccess(userNotAcceptedFriend.Id, DataAccess.Public, true);
            await _postRepository.Received().GetPostsByOwnerAndAccess(userNotAcceptedFriend.Id, DataAccess.Private, false);
        }
        #endregion

        #region Get For User To See
        [Test]
        public async Task GetToSeePostsForNoExistingUserThrowsError()
        {
            //Arrange
            _userRepository.GetOrDefaultAsync(Guid.Parse(_id)).Returns((User)default);

            //Act && assert
            var ex = Assert.ThrowsAsync<EntityNotFoundException>(() => _postService.GetForUserToSeeAsync(Guid.Parse(_id)));
            Assert.True(ex.Message.Contains("User with id:") &&
                        ex.Message.Contains("not found."));
        }

        [Test]
        public async Task IfUserHasNotFriendsThenNoMethodCalled()
        {
            //Arrange
            var user = _users.ElementAt(0);
            _userRepository.GetOrDefaultAsync(user.Id).Returns(user);
            _userService.GetFriendsAsync(user.Id).Returns((IEnumerable<User>)default);

            //Act
            await _postService.GetForUserToSeeAsync(user.Id);

            //Assert
            await _postRepository.DidNotReceive().GetPostsByOwnerAndAccess(Arg.Any<Guid>(), DataAccess.Private, false);
        }

        [Test]
        public async Task IfUserHasFriendsThenGetPostFromRepository()
        {
            //Arrange
            var user = _users.ElementAt(0);
            var friend = _users.ElementAt(1);
            _userRepository.GetOrDefaultAsync(user.Id).Returns(user);
            _userService.GetFriendsAsync(user.Id).Returns(new List<User> { friend });

            //Act
            await _postService.GetForUserToSeeAsync(user.Id);

            //Assert
            await _postRepository.Received().GetPostsByOwnerAndAccess(friend.Id, DataAccess.Private, false);
        }

        [Test]
        public async Task IfUserHasFriendsThenGetOnlyDistinctPosts()
        {
            //Arrange
            var user = _users.ElementAt(0);
            var friend = _users.ElementAt(1);
            _userRepository.GetOrDefaultAsync(user.Id).Returns(user);
            _userService.GetFriendsAsync(user.Id).Returns(new List<User> { friend });
            _postRepository.GetPostsByOwnerAndAccess(friend.Id, DataAccess.Private, false)
                            .Returns(new List<Post> { _posts.ElementAt(0), _posts.ElementAt(1), _posts.ElementAt(0) });
            //Act
            var posts = await _postService.GetForUserToSeeAsync(user.Id);

            //Assert
            Assert.True(posts.Count() == 2);
        }

        #endregion


    }
}
