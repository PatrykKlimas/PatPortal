using MediatR;
using PatPortal.Application.Contracts.Commands.Comments;
using PatPortal.Domain.Entities.Comments.Requests;
using PatPortal.Domain.Exceptions;
using PatPortal.Domain.Services.Interfaces;
using PatPortal.SharedKernel.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Application.Handlers.Commands.Comments
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, string>
    {
        private readonly ICommentService _commentService;

        public CreateCommentCommandHandler(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public async Task<string> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var postId = request.Ownerid.ParseToGuidOrEmpty();
            if (postId == Guid.Empty)
                throw new InitValidationException($"Inncorect post id: {postId}");

            var id = request.PostId.ParseToGuidOrEmpty();
            if (id == Guid.Empty)
                throw new InitValidationException($"Inncorect user id: {id}");

            var commentCreate = new CommentCreate(Guid.Parse(request.Ownerid), request.Content, Guid.Parse(request.PostId));

            var comment = await _commentService.CreateAsync(commentCreate);

            return comment.ToString();
        }
    }
}
