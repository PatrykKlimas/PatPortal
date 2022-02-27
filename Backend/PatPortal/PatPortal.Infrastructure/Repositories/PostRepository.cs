using PatPortal.Domain.Entities.Posts;
using PatPortal.Domain.Enums;
using PatPortal.Domain.Repositories.Interfaces;

namespace PatPortal.Infrastructure.Repositories.Mock
{
    public class PostRepository : IPostRepository
    {
        public Task<Post> AddAsync(Post post)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Post>> GetByOwnerAsync(Guid ownerId)
        {
            throw new NotImplementedException();
        }

        public Task<Post> GetOrDefaultAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Post>> GetPostsByOwnerAndAccess(Guid ownerId, DataAccess access, bool equals)
        {
            throw new NotImplementedException();
        }

        public Task<Post> UpdateAsync(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
