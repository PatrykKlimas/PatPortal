using PatPortal.Domain.Entities.Comments;
using PatPortal.Domain.Repositories.Interfaces;

namespace PatPortal.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        public Task<Comment> AddAsync(Comment comment)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByPostAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Comment> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Comment>> GetByPostAsync(Guid postId)
        {
            throw new NotImplementedException();
        }

        public Task<Comment> UpdateAsync(Comment comment)
        {
            throw new NotImplementedException();
        }
    }
}
