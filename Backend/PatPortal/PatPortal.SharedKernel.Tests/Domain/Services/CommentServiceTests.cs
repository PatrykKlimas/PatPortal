using FluentValidation;
using NSubstitute;
using NUnit.Framework;
using PatPortal.Domain.Entities.Comments;
using PatPortal.Domain.Repositories.Interfaces;
using PatPortal.Domain.Services;
using PatPortal.Domain.Services.Interfaces;
using PatPortal.Domain.Validators.Comments;

namespace PatPortal.Unit.Tests.Domain.Services
{
    public class CommentServiceTests
    {
        private ICommentService _commentService;
        private IUserRepository _userRepository;
        private ICommentRepository _commentRepository;
        private IPostRepository _postRepository;
        private IValidator<Comment> _validator;

        [SetUp]
        public void Setup()
        {
            _commentRepository = Substitute.For<ICommentRepository>();
            _userRepository = Substitute.For<IUserRepository>();
            _postRepository = Substitute.For<IPostRepository>();
            _validator = new CommentValidator();
            _commentService = new CommentService(_userRepository, _postRepository, _commentRepository, _validator);
        }
    }
}
