using FluentValidation;
using NSubstitute;
using NUnit.Framework;
using PatPortal.Domain.Entities.Posts;
using PatPortal.Domain.Repositories.Interfaces;
using PatPortal.Domain.Services;
using PatPortal.Domain.Services.Interfaces;
using PatPortal.Domain.Validators.Posts;

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

        [SetUp]
        public void SetUp()
        {
            _postService = Substitute.For<IPostService>();
            _userRepository = Substitute.For<IUserRepository>();
            _postRepository = Substitute.For<IPostRepository>();
            _friendshipRepository = Substitute.For<IFriendshipRepository>();
            _userService = Substitute.For<IUserService>();
            _validator = new PostValidator();
            _postService = new PostService(_userRepository, _validator, _postRepository, _friendshipRepository, _userService);
        }
    }
}
