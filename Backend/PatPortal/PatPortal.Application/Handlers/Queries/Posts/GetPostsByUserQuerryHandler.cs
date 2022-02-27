using AutoMapper;
using MediatR;
using PatPortal.Application.Contracts.Querries.Posts;
using PatPortal.Application.DTOs.Response.Posts;
using PatPortal.Application.Handlers.BaseHandlers;
using PatPortal.Domain.Exceptions;
using PatPortal.Domain.Services.Interfaces;
using PatPortal.SharedKernel.Extensions;

namespace PatPortal.Application.Handlers.Queries.Posts
{
    internal class GetPostsByUserQuerryHandler : BaseHandler, IRequestHandler<GetPostsByUserQuerry, IEnumerable<PostForViewDto>>
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public GetPostsByUserQuerryHandler(
            IPostService postService,
            IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PostForViewDto>> Handle(GetPostsByUserQuerry request, CancellationToken cancellationToken)
        {
            var userId = GetGuidOrThrow("user", request.UserId);
            var requestorId = GetGuidOrThrow("requestor", request.RequestorId);
            var posts = await _postService.GetByUserAsync(userId, requestorId);

            return _mapper.Map<IEnumerable<PostForViewDto>>(posts);
        }
    }
}
