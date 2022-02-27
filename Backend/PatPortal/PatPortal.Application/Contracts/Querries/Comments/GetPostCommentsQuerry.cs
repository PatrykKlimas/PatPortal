using MediatR;
using PatPortal.Application.DTOs.Response.Comments;

namespace PatPortal.Application.Contracts.Querries.Comments
{
    public class GetPostCommentsQuerry : IRequest<IEnumerable<CommentForViewDto>>
    {
        public string PostId { get; }
        public GetPostCommentsQuerry(string postId)
        {
            PostId = postId;
        }
    }
}
