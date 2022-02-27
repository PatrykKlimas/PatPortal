using MediatR;
using PatPortal.Application.DTOs.Request.Posts;

namespace PatPortal.Application.Contracts.Commands.Posts
{
    public class CreatePostsCommand : IRequest<string>
    {
        public PostForCreationDto Post { get; }
        public CreatePostsCommand(PostForCreationDto post)
        {
            Post = post;
        }

    }
}
