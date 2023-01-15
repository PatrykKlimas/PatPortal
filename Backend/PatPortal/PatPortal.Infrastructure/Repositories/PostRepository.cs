using Microsoft.EntityFrameworkCore;
using PatPortal.Database;
using PatPortal.Domain.Entities.Posts;
using PatPortal.Domain.Enums;
using PatPortal.Domain.Exceptions;
using PatPortal.Domain.Repositories.Interfaces;
using PatPortal.Infrastructure.Factories.Interfaces;

namespace PatPortal.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly PatPortalDbContext _context;
        private readonly IPostMapper _postFactory;

        public PostRepository(PatPortalDbContext context, IPostMapper postFactory)
        {
            _context = context;
            _postFactory = postFactory;
        }
        public async Task<Post> AddAsync(Post post)
        {
            var postDb = _postFactory.Create(post);
            await _context.Posts.AddAsync(postDb);
            await _context.SaveChangesAsync();

            var newPost = await _context.Posts.FirstOrDefaultAsync(post => post.Id == post.Id);

            if(newPost is null)
                throw new InvalidOperationException("Post cannot be saved to the database.");

            return _postFactory.Create(newPost);
        }

        public async Task<IEnumerable<Post>> GetByOwnerAsync(Guid ownerId)
        {
            var posts = _context.Posts.Where(post => post.OwnerId == ownerId);

            return await posts.Select(post => _postFactory.Create(post)).ToListAsync();
        }

        public async Task<Post> GetOrDefaultAsync(Guid Id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(post => post.Id == Id);

            return post is null ? default : _postFactory.Create(post);
        }

        public async Task<IEnumerable<Post>> GetPostsByOwnerAndAccess(Guid ownerId, DataAccess access, bool equals)
        {
            var posts = _context.Posts.Where(post => post.OwnerId == ownerId &&
                (post.Access == access) == equals);

            return await posts.Select(post => _postFactory.Create(post)).ToListAsync();
        }

        public async Task<Post> UpdateAsync(Post post)
        {
            var postDb = await _context.Posts.FirstOrDefaultAsync(p => p.Id == post.Id);

            if (postDb is null)
                throw new EntityNotFoundException($"Post with id {post.Id} not found");

            postDb = _postFactory.Create(post);
            await SaveChangesAsync();

            return _postFactory.Create(postDb);

        }
        private async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new InvalidOperationException("Unable to save changes. Please try leater.");
            }
        }
    }
}
