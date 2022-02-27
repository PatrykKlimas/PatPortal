using PatPortal.Domain.Entities.Posts;
using PatPortal.Domain.Enums;
using PatPortal.Domain.Repositories.Interfaces;

namespace PatPortal.Infrastructure.Repositories.Mock
{
    public class PostMockRepository : IPostRepository
    {

        private IEnumerable<Post> _posts;
        public PostMockRepository()
        {
            _posts = MockDataProvider.MockPosts();
        }

        public async Task<Post> AddAsync(Post post)
        {
            var posts = _posts.ToList();
            posts.Add(post);

            _posts = posts;
            var createdPost = _posts.FirstOrDefault(p => p.Id == post.Id);
            return await Task.FromResult(createdPost);
        }

        public async Task<Post> GetOrDefaultAsync(Guid Id)
        {
            var post = _posts.FirstOrDefault(p => p.Id == Id);
            return await Task.FromResult(post == default ? default : post);
        }

        public async Task<Post> UpdateAsync(Post post)
        {
            var postToUpdate = _posts.FirstOrDefault(p => p.Id == post.Id);
            postToUpdate = post;

            return await Task.FromResult(postToUpdate);
        }

        public async Task<IEnumerable<Post>> GetByOwnerAsync(Guid ownerId)
        {
            return await Task.FromResult(_posts.Where(post => post.Owner.Id == ownerId));
        }

        public async Task<IEnumerable<Post>> GetPostsByOwnerAndAccess(Guid ownerId, DataAccess access, bool equals)
        {
            var posts = _posts.Where(post => post.Owner.Id == ownerId && 
                                     (post.Access == access) == equals);

            return await Task.FromResult(posts);
        }
    }
}
