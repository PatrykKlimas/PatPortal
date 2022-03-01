using FluentValidation;
using NSubstitute;
using NUnit.Framework;
using PatPortal.Domain.Entities.Comments;
using PatPortal.Domain.Entities.Comments.Requests;
using PatPortal.Domain.Entities.Posts;
using PatPortal.Domain.Entities.Users;
using PatPortal.Domain.Exceptions;
using PatPortal.Domain.Repositories.Interfaces;
using PatPortal.Domain.Services;
using PatPortal.Domain.Services.Interfaces;
using PatPortal.Domain.Validators.Comments;
using PatPortal.Infrastructure.Repositories.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatPortal.Unit.Tests.Domain.Services
{
    public class CommentServiceTests
    {
        private ICommentService _commentService;
        private IUserRepository _userRepository;
        private ICommentRepository _commentRepository;
        private IPostRepository _postRepository;
        private IValidator<Comment> _validator;
        private IEnumerable<Comment> _comments;
        private IEnumerable<Post> _posts;
        private IEnumerable<User> _user;

        [SetUp]
        public void Setup()
        {
            _commentRepository = Substitute.For<ICommentRepository>();
            _userRepository = Substitute.For<IUserRepository>();
            _postRepository = Substitute.For<IPostRepository>();
            _posts = MockDataProvider.MockPosts();
            _user = MockDataProvider.MockUsers();
            _comments = MockDataProvider.MockComments();
            _validator = new CommentValidator();
            _commentService = new CommentService(_userRepository, _postRepository, _commentRepository, _validator);
        }

        #region Create Comment

        [Test]
        public async Task IfOwnerNotFoundThrowsException()
        {
            //Arrange
            var ownerId = Guid.NewGuid();
            var postId = Guid.NewGuid();
            var commentCreate = new CommentCreate(ownerId, "Some content", postId);

            _userRepository.GetOrDefaultAsync(ownerId).Returns((User)default);
            _postRepository.GetOrDefaultAsync(postId).Returns(_posts.ElementAt(0));

            //Act & Assert
            var ex = Assert.ThrowsAsync<EntityNotFoundException>(async () => await _commentService.CreateAsync(commentCreate));
            Assert.True(ex.Message.Contains("User") && ex.Message.Contains("not found"));
        }

        [Test]
        public async Task IfPostNotFoundThrowsException()
        {
            //Arrange
            var ownerId = Guid.NewGuid();
            var postId = Guid.NewGuid();
            var commentCreate = new CommentCreate(ownerId, "Some content", postId);

            _userRepository.GetOrDefaultAsync(ownerId).Returns(_user.ElementAt(0));
            _postRepository.GetOrDefaultAsync(postId).Returns((Post)default);

            //Act & Assert
            var ex = Assert.ThrowsAsync<EntityNotFoundException>(async () => await _commentService.CreateAsync(commentCreate));
            Assert.True(ex.Message.Contains("Post") && ex.Message.Contains("not found"));
        }

        [Test]
        [TestCase("a", "Comment length must be between 2 and 500 characters.")]
        [TestCase("xxxxx", "Comment length must be between 2 and 500 characters.")]
        public async Task WithInvalidDataThrowsException(string content, string errorMsg)
        {
            //Arrange
            var ownerId = Guid.NewGuid();
            var postId = Guid.NewGuid();

            content = content == "xxxxx" ? string.Concat(Enumerable.Repeat(content, 101)) : content;
            var commentCreate = new CommentCreate(ownerId, content, postId);

            _userRepository.GetOrDefaultAsync(ownerId).Returns(_user.ElementAt(0));
            _postRepository.GetOrDefaultAsync(postId).Returns(_posts.ElementAt(0));

            //Act & Assert
            var ex = Assert.ThrowsAsync<CustomValidationnException>(async () => await _commentService.CreateAsync(commentCreate));
            Assert.True(ex.Message.Contains(errorMsg));
        }

        [Test]
        public async Task WithValidDataCreateComment()
        {
            //Arrange
            var ownerId = Guid.NewGuid();
            var postId = Guid.NewGuid();

            var commentCreate = new CommentCreate(ownerId, "Some content", postId);

            _userRepository.GetOrDefaultAsync(ownerId).Returns(_user.ElementAt(0));
            _postRepository.GetOrDefaultAsync(postId).Returns(_posts.ElementAt(0));

            //Act
            var id = await _commentService.CreateAsync(commentCreate);

            //Act & Assert
            Assert.True(id != null);
            _commentRepository.Received(1).AddAsync(Arg.Any<Comment>());
        }

        #endregion

        #region Update Comment

        [Test]
        public async Task IfOwnerNotFoundUpdateThrowsException()
        {
            //Arrange
            var comment = _comments.First();
            var commentUpdate = new CommentUpdate(comment.Id, comment.Owner.Id, comment.Content, comment.Post.Id);
            _userRepository.GetOrDefaultAsync(comment.Owner.Id).Returns((User)default);
            _postRepository.GetOrDefaultAsync(comment.Post.Id).Returns(_posts.ElementAt(0));
            _commentRepository.GetAsync(comment.Id).Returns(_comments.ElementAt(0));

            //Act & Assert
            var ex = Assert.ThrowsAsync<EntityNotFoundException>(async () => await _commentService.UpdateAsync(commentUpdate));
            Assert.True(ex.Message.Contains("User") && ex.Message.Contains("not found"));
        }

        [Test]
        public async Task IfPostNotFoundUpdateThrowsException()
        {
            //Arrange
            var comment = _comments.First();
            var commentUpdate = new CommentUpdate(comment.Id, comment.Owner.Id, comment.Content, comment.Post.Id);
            _userRepository.GetOrDefaultAsync(comment.Owner.Id).Returns(_user.ElementAt(0));
            _postRepository.GetOrDefaultAsync(comment.Post.Id).Returns((Post)default);
            _commentRepository.GetAsync(comment.Id).Returns(_comments.ElementAt(0));

            //Act & Assert
            var ex = Assert.ThrowsAsync<EntityNotFoundException>(async () => await _commentService.UpdateAsync(commentUpdate));
            Assert.True(ex.Message.Contains("Post") && ex.Message.Contains("not found"));
        }

        [Test]
        public async Task IfCommentNotFoundUpdateThrowsException()
        {
            //Arrange
            var comment = _comments.First();
            var commentUpdate = new CommentUpdate(comment.Id, comment.Owner.Id, comment.Content, comment.Post.Id);
            _userRepository.GetOrDefaultAsync(comment.Owner.Id).Returns(_user.ElementAt(0));
            _postRepository.GetOrDefaultAsync(comment.Post.Id).Returns(_posts.ElementAt(0));
            _commentRepository.GetAsync(comment.Id).Returns((Comment)default);

            //Act & Assert
            var ex = Assert.ThrowsAsync<EntityNotFoundException>(async () => await _commentService.UpdateAsync(commentUpdate));
            Assert.True(ex.Message.Contains("Comment") && ex.Message.Contains("not found"));
        }

        [Test]
        public async Task IfNothingChangedReturns()
        {
            //Arrange
            var comment = _comments.ElementAt(0);
            var commentUpdate = new CommentUpdate(comment.Id, comment.Owner.Id, comment.Content, comment.Post.Id);
            _userRepository.GetOrDefaultAsync(comment.Owner.Id).Returns(comment.Owner);
            _postRepository.GetOrDefaultAsync(comment.Post.Id).Returns(comment.Post);
            _commentRepository.GetAsync(comment.Id).Returns(comment);

            //Act
            await _commentService.UpdateAsync(commentUpdate);

            //Assert
            _commentRepository.DidNotReceiveWithAnyArgs().UpdateAsync(Arg.Any<Comment>());
        }

        [Test]
        [TestCase("a", "Comment length must be between 2 and 500 characters.")]
        [TestCase("xxxxx", "Comment length must be between 2 and 500 characters.")]
        public async Task WithInvalidDataThrowsError(string content, string errorMsg)
        {
            //Arrange
            var comment = _comments.ElementAt(0);
            content = content == "xxxxx" ? string.Concat(Enumerable.Repeat(content, 101)) : content;

            var commentUpdate = new CommentUpdate(comment.Id, comment.Owner.Id, content, comment.Post.Id);

            _userRepository.GetOrDefaultAsync(comment.Owner.Id).Returns(comment.Owner);
            _postRepository.GetOrDefaultAsync(comment.Post.Id).Returns(comment.Post);
            _commentRepository.GetAsync(comment.Id).Returns(comment);

            //Act & Assert
            var ex = Assert.ThrowsAsync<CustomValidationnException>(async () => await _commentService.UpdateAsync(commentUpdate));
            Assert.True(ex.Message.Contains(errorMsg));
        }

        [Test]
        public async Task IfValidDataUpdate()
        {
            //Arrange
            var comment = _comments.ElementAt(0);
            var commentUpdate = new CommentUpdate(comment.Id, comment.Owner.Id, "Some content", comment.Post.Id);
            _userRepository.GetOrDefaultAsync(comment.Owner.Id).Returns(comment.Owner);
            _postRepository.GetOrDefaultAsync(comment.Post.Id).Returns(comment.Post);
            _commentRepository.GetAsync(comment.Id).Returns(comment);

            //Act
            await _commentService.UpdateAsync(commentUpdate);

            //Assert
            _commentRepository.Received(1).UpdateAsync(Arg.Any<Comment>());
        }

        #endregion
    }
}
