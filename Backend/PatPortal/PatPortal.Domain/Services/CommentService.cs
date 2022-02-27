using FluentValidation;
using PatPortal.Domain.Entities.Comments;
using PatPortal.Domain.Entities.Comments.Requests;
using PatPortal.Domain.Entities.Posts;
using PatPortal.Domain.Entities.Users;
using PatPortal.Domain.Exceptions;
using PatPortal.Domain.Repositories.Interfaces;
using PatPortal.Domain.Services.Interfaces;

namespace PatPortal.Domain.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IValidator<Comment> _validator;

        public CommentService(
            IUserRepository userRepository,
            IPostRepository postRepository,
            ICommentRepository commentRepository,
            IValidator<Comment> validator)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _validator = validator;
        }
        public async Task<Guid> CreateAsync(CommentCreate commentCreate)
        {
            var userTask = GetUserOrThrowAsync(commentCreate.Ownerid);
            var postTask = GetPostOrThrowAsync(commentCreate.PostId);
            await Task.WhenAll(userTask, postTask);

            var comment = new Comment(Guid.NewGuid(), userTask.Result, commentCreate.Content, 
                                      DateTime.Now, DateTime.Now, postTask.Result);

            var validationResult = await _validator.ValidateAsync(comment);

            if (!validationResult.IsValid)
                throw new CustomValidationnException(validationResult);

            await _commentRepository.AddAsync(comment);

            return comment.Id;
        }

        public async Task UpdateAsync(CommentUpdate commentUpdate)
        {
            var userTask = GetUserOrThrowAsync(commentUpdate.Ownerid);
            var postTask = GetPostOrThrowAsync(commentUpdate.PostId);
            var commentTask = _commentRepository.GetAsync(commentUpdate.Id);

            await Task.WhenAll(userTask, postTask, commentTask);

            if (commentTask.Result == default)
                throw new EntityNotFoundException($"Comment with id: {commentUpdate.Id} not found.");

            var comment = new Comment(Guid.NewGuid(), userTask.Result, commentUpdate.Content,
                                      DateTime.Now, DateTime.Now, postTask.Result);

            if (comment.Equals(commentTask.Result))
                return;

            var validationResult = await _validator.ValidateAsync(comment);

            if (!validationResult.IsValid)
                throw new CustomValidationnException(validationResult);

            await _commentRepository.UpdateAsync(comment);
        }

        private async Task<Post> GetPostOrThrowAsync(Guid postId)
        {
            var post = await _postRepository.GetOrDefaultAsync(postId);
            if (post == default)
                throw new EntityNotFoundException($"User with id: {postId} not found.");

            return post;
        }
        private async Task<User> GetUserOrThrowAsync(Guid userId)
        {
            var user = await _userRepository.GetOrDefaultAsync(userId);
            if (user == default)
                throw new EntityNotFoundException($"User with id: {userId} not found.");

            return user;
        }
    }
}
