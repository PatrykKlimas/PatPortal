using PatPortal.Infrastructure.Factories.Interfaces;
using PostEntity = PatPortal.Domain.Entities.Posts.Post;
using PostDb = PatPortal.Database.Models.Post;

namespace PatPortal.Infrastructure.Factories
{
    public class PostMapper : IPostMapper
    {
        private readonly IUserMapper _userFactory;

        public PostMapper(IUserMapper userFactory)
        {
            _userFactory = userFactory;
        }
        public PostDb Create(PostEntity post)
        {
            var user = _userFactory.Create(post.Owner);
            return new PostDb()
            {
                Photo = post.Photo,
                Content = post.Content,
                Access = post.Access,
                AddedDate = post.AddedDate,
                EditedTime = post.EditedTime,
                Owner = user,
                OwnerId = post.Owner.Id,
                IsDeleted = false
            };
        }

        public PostEntity Create(PostDb post)
        {
            var user = _userFactory.Create(post.Owner);
            return new PostEntity(
                post.Id,
                post.Photo,
                post.Content,
                post.Access,
                user,
                post.AddedDate,
                post.EditedTime);
        }
    }
}
