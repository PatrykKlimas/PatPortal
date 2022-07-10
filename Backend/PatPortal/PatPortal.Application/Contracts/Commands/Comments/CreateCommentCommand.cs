using MediatR;

namespace PatPortal.Application.Contracts.Commands.Comments
{
    public class CreateCommentCommand : IRequest<string>
    {
        public CreateCommentCommand(string postId, string ownerid, string content)
        {
            PostId = postId;
            Ownerid = ownerid;
            Content = content;
        }

        public string PostId { get; }
        public string Ownerid { get; }
        public string Content { get; }
    }
}
