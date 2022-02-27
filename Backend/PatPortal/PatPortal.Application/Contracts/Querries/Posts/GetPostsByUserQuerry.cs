using MediatR;
using PatPortal.Application.DTOs.Response.Posts;

namespace PatPortal.Application.Contracts.Querries.Posts
{
    public class GetPostsByUserQuerry : IRequest<IEnumerable<PostForViewDto>>
    {
        public string UserId { get; }
        public string RequestorId { get; }
        public GetPostsByUserQuerry(string userId, string requestorId)
        {
            UserId = userId;
            RequestorId = requestorId;
        }

    }
}
