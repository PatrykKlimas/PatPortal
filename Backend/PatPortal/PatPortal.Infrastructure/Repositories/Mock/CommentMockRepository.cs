using PatPortal.Domain.Entities.Comments;
using PatPortal.Domain.Repositories.Interfaces;

namespace PatPortal.Infrastructure.Repositories.Mock
{
    public class CommentMockRepository : ICommentRepository
    {
        IEnumerable<Comment> _comments;
        public CommentMockRepository()
        {
            _comments = MockDataProvider.MockComments();
        }
        public async Task<Comment> AddAsync(Comment comment)
        {
            var newComments = _comments.ToList();
            newComments.Add(comment);

            _comments = newComments;
            var newComment = _comments.FirstOrDefault(c => c.Id == comment.Id);
            return await Task.FromResult(newComment);
        }

        public async Task DeleteAsync(Guid id)
        {
            var newComments = _comments.Where(c => c.Id != id);
            _comments = newComments;

            await Task.FromResult(_comments);
        }

        public async Task DeleteByPostAsync(Guid id)
        {
            var newComments = _comments.Where(c => c.Post.Id != id);
            _comments = newComments;

            await Task.FromResult(_comments);
        }

        public async Task<Comment> GetAsync(Guid id)
        {
            var comment = _comments.FirstOrDefault(c => c.Id == id);
            return await Task.FromResult(comment);
        }

        public async Task<IEnumerable<Comment>> GetByPostAsync(Guid postId)
        {
            var comments = _comments.Where(c => c.Post.Id == postId);
            return await Task.FromResult(comments);
        }

        public async Task<Comment> UpdateAsync(Comment comment)
        {
            var commentToUpdate = _comments.FirstOrDefault(c => c.Id == comment.Id);
            commentToUpdate = comment;

            return await Task.FromResult(_comments.FirstOrDefault(c => c.Id == comment.Id));
        }
    }
}
