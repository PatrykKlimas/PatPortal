using Microsoft.EntityFrameworkCore;
using PatPortal.Database;
using PatPortal.Domain.Entities.Comments;
using PatPortal.Domain.Exceptions;
using PatPortal.Domain.Repositories.Interfaces;
using PatPortal.Infrastructure.Mappers.Interfaces;

namespace PatPortal.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ICommentMapper _commentMapper;
        private readonly PatPortalDbContext _context;

        public CommentRepository(ICommentMapper commentMapper, PatPortalDbContext patPortalDbContext)
        {
            _commentMapper = commentMapper;
            _context = patPortalDbContext;
        }
        public async Task<Comment> AddAsync(Comment comment)
        {
            await _context.Comments.AddAsync(_commentMapper.Create(comment));
            await SaveChangesAsync();

            var commentNew = await _context.Comments.FirstOrDefaultAsync(c => c.Id == comment.Id);

            if (commentNew is null)
                throw new InvalidOperationException("Comment cannot be saved to the database.");

            return _commentMapper.Create(commentNew);
        }

        public async Task DeleteAsync(Guid id)
        {
            var commentDb = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (commentDb is null)
                throw new EntityNotFoundException($"Comment with {id} not found.");

            commentDb.IsDeleted = true;
            await SaveChangesAsync();
        }

        public Task DeleteByPostAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Comment> GetAsync(Guid id)
        {
            var commentDb = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (commentDb is null)
                throw new EntityNotFoundException($"Comment with {id} not found.");

            return _commentMapper.Create(commentDb);
        }

        public Task<IEnumerable<Comment>> GetByPostAsync(Guid postId)
        {
            throw new NotImplementedException();
        }

        public Task<Comment> UpdateAsync(Comment comment)
        {
            throw new NotImplementedException();
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
