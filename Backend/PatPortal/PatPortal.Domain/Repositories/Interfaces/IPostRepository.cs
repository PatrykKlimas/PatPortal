using PatPortal.Domain.Entities.Posts;
using PatPortal.Domain.Enums;

namespace PatPortal.Domain.Repositories.Interfaces
{
    public interface IPostRepository
    {
        Task<Post> GetOrDefaultAsync(Guid Id);
        Task<IEnumerable<Post>> GetByOwnerAsync(Guid ownerId);
        Task<Post> AddAsync(Post post);
        Task<Post> UpdateAsync(Post post);
        Task<IEnumerable<Post>> GetPostsByOwnerAndAccess(Guid ownerId, DataAccess access, bool equals);
    }
}
