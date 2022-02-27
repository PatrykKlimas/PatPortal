using AutoMapper;
using MediatR;
using PatPortal.Application.Contracts.Querries.Comments;
using PatPortal.Application.DTOs.Response.Comments;
using PatPortal.Application.Handlers.BaseHandlers;
using PatPortal.Domain.Repositories.Interfaces;
using PatPortal.SharedKernel.Extensions;

namespace PatPortal.Application.Handlers.Queries.Comments
{
    public class GetPostCommentsQuerryHandler : BaseHandler, IRequestHandler<GetPostCommentsQuerry, IEnumerable<CommentForViewDto>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public GetPostCommentsQuerryHandler(
            ICommentRepository commentRepository,
            IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CommentForViewDto>> Handle(GetPostCommentsQuerry request, CancellationToken cancellationToken)
        {
            var postId = GetGuidOrThrow("post", request.PostId);
            var comments = await _commentRepository.GetByPostAsync(postId);
            var commentDtos = _mapper.Map<IEnumerable<CommentForViewDto>>(comments);

            return commentDtos.OrderByDescending(comment => comment.AddedDate.ParseToDateTime());
        }
    }
}
