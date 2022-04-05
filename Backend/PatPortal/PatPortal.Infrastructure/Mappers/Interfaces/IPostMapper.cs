using PostEntity = PatPortal.Domain.Entities.Posts.Post;
using PostDb = PatPortal.Database.Models.Post;

namespace PatPortal.Infrastructure.Factories.Interfaces
{
    public interface IPostMapper
    {
        public PostDb Create(PostEntity post);
        public PostEntity Create(PostDb post);
    }
}
