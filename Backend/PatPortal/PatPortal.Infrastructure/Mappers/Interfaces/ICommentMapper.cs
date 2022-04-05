using CommentDb = PatPortal.Database.Models.Comment;
using CommentEntity = PatPortal.Domain.Entities.Comments.Comment;

namespace PatPortal.Infrastructure.Mappers.Interfaces
{
    public interface ICommentMapper
    {
        CommentDb Create(CommentEntity comment);
        CommentEntity Create(CommentDb comment);
    }
}
