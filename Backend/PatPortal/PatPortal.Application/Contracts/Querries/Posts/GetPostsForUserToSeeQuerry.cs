using MediatR;
using PatPortal.Application.DTOs.Response.Posts;

namespace PatPortal.Application.Contracts.Querries.Posts
{
    public class GetPostsForUserToSeeQuerry : IRequest<IEnumerable<PostForViewDto>>
    {
        public string UserId { get; }
        public GetPostsForUserToSeeQuerry(string userId)
        {
            UserId = userId;
        }

    }
}
