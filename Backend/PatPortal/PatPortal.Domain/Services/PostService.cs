using FluentValidation;
using PatPortal.Domain.Entities.Posts;
using PatPortal.Domain.Entities.Posts.Requests;
using PatPortal.Domain.Entities.Users;
using PatPortal.Domain.Enums;
using PatPortal.Domain.Exceptions;
using PatPortal.Domain.Repositories.Interfaces;
using PatPortal.Domain.Services.Interfaces;
using PatPortal.SharedKernel.Extensions;

namespace PatPortal.Domain.Services
{
    public class PostService : IPostService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<Post> _validator;
        private readonly IPostRepository _postRepository;
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly IUserService _userService;

        public PostService(
            IUserRepository userRepository,
            IValidator<Post> validator,
            IPostRepository postRepository,
            IFriendshipRepository friendshipRepository,
            IUserService userService)
        {
            _userRepository = userRepository;
            _validator = validator;
            _postRepository = postRepository;
            _friendshipRepository = friendshipRepository;
            _userService = userService;
        }

        public async Task<Guid> CreateAsync(PostCreate postCreate)
        {
            var userTask = GetUserOrThrowAsync(postCreate.OwnerId);
            var access = postCreate.Access.ParseToEnumOrThrow<DataAccess, CustomValidationnException>();
            var user = await userTask;

            var post = new Post(
                Guid.NewGuid(), 
                postCreate.Photo, 
                postCreate.Content, 
                access, 
                user, 
                DateTime.Now, 
                DateTime.Now);

            await ValidateOrThrow(post);
            var createdPost = await _postRepository.AddAsync(post);

            return createdPost.Id;
        }

        public async Task UpdateAsync(PostUpdate postUpdate)
        {
            var userTask = GetUserOrThrowAsync(postUpdate.OwnerId);
            var postTask = _postRepository.GetOrDefaultAsync(postUpdate.Id);
            var access = postUpdate.Access.ParseToEnumOrThrow<DataAccess, CustomValidationnException>();

            await Task.WhenAll(userTask, postTask);

           if (postTask.Result == default)
                throw new EntityNotFoundException($"Post with id: {postUpdate.Id} not found.");

            var post = new Post(
                postUpdate.Id,
                postUpdate.Photo,
                postUpdate.Content,
                access,
                userTask.Result,
                postTask.Result.AddedDate,
                DateTime.Now);

            await ValidateOrThrow(post);
            await _postRepository.UpdateAsync(post);
        }

        public async Task<IEnumerable<Post>> GetByUserAsync(Guid userId, Guid requestorId)
        {
            var userTask = GetUserOrThrowAsync(userId);
            var requestorTask = GetUserOrThrowAsync(requestorId);

            await Task.WhenAll(userTask, requestorTask);

            var user = userTask.Result;
            var requestor = requestorTask.Result;

            if (user.Id == requestor.Id)
                return await _postRepository.GetByOwnerAsync(requestor.Id);

            var friendship = await _friendshipRepository.GetByUserAndFriendOrDefault(user.Id, requestor.Id);

            if(friendship == default || !friendship.InviteAccepted)
                return await _postRepository.GetPostsByOwnerAndAccess(user.Id, DataAccess.Public, true);

            return await _postRepository.GetPostsByOwnerAndAccess(user.Id, DataAccess.Private, false);
        }

        public async Task<IEnumerable<Post>> GetForUserToSeeAsync(Guid userId)
        {
            var user = await GetUserOrThrowAsync(userId);
            var friends = await _userService.GetFriendsAsync(user.Id);

            if (friends == default)
                return new List<Post>();

            var postTasks = friends.Select(async friend =>
                await _postRepository.GetPostsByOwnerAndAccess(friend.Id, DataAccess.Private, false));

            await Task.WhenAll(postTasks);

            IEnumerable<Post> posts = new List<Post>();
            foreach (var postTask in postTasks)
            {
                var friendPosts = postTask.Result;
                posts = posts.Union(friendPosts);
            }

            return posts.Distinct();
        }

        private async Task<User> GetUserOrThrowAsync(Guid userId)
        {
            var user = await _userRepository.GetOrDefaultAsync(userId);
            if (user == default)
                throw new EntityNotFoundException($"User with id: {userId} not found.");

            return user;
        }

        private async Task ValidateOrThrow(Post post)
        {
            var validationResult = await _validator.ValidateAsync(post);

            if (!validationResult.IsValid)
                throw new CustomValidationnException(validationResult);
        }
    }
}
