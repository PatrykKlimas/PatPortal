using AutoMapper;
using MediatR;
using PatPortal.Application.Contracts.Querries.Comments;
using PatPortal.Application.DTOs.Response.Comments;
using PatPortal.Application.Handlers.BaseHandlers;
using PatPortal.Domain.Entities.Comments;
using PatPortal.Domain.Exceptions;
using PatPortal.Domain.Repositories.Interfaces;

namespace PatPortal.Application.Handlers.Queries.Comments
{
    public class GetCommentQueryHandler : BaseHandler, IRequestHandler<GetCommentQuery, CommentForViewDto>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public GetCommentQueryHandler(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }
        public async Task<CommentForViewDto> Handle(GetCommentQuery request, CancellationToken cancellationToken)
        {
            var commentId = GetGuidOrThrow(nameof(Comment), request.Id);
            var comment = await _commentRepository.GetAsync(commentId);

            if (comment == default)
                throw new EntityNotFoundException($"{nameof(Comment)} with id: {commentId} not ofund");

            return _mapper.Map<CommentForViewDto>(comment);
        }
    }
}
