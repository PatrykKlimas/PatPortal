using PatPortal.Domain.Entities.Comments;

namespace PatPortal.Domain.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment> GetAsync(Guid id);
        Task<IEnumerable<Comment>> GetByPostAsync(Guid positId);
        Task<Comment> AddAsync(Comment comment);
        Task<Comment> UpdateAsync(Comment comment);
        Task DeleteAsync(Guid id);
        Task DeleteByPostAsync(Guid id);
    }
}
