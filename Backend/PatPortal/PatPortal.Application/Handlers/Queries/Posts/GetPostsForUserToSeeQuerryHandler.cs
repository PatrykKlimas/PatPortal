using AutoMapper;
using MediatR;
using PatPortal.Application.Contracts.Querries.Posts;
using PatPortal.Application.DTOs.Response.Posts;
using PatPortal.Domain.Exceptions;
using PatPortal.Domain.Services.Interfaces;
using PatPortal.SharedKernel.Extensions;

namespace PatPortal.Application.Handlers.Queries.Posts
{
    internal class GetPostsForUserToSeeQuerryHandler : IRequestHandler<GetPostsForUserToSeeQuerry, IEnumerable<PostForViewDto>>
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public GetPostsForUserToSeeQuerryHandler(
            IPostService postService, 
            IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PostForViewDto>> Handle(GetPostsForUserToSeeQuerry request, CancellationToken cancellationToken)
        {
            var userId = request.UserId.ParseToGuidOrEmpty();
            if (userId == Guid.Empty)
                throw new InitValidationException($"Invalid user id: {userId}.");

            var posts = await _postService.GetForUserToSeeAsync(userId);
            var posstDto = _mapper.Map<IEnumerable<PostForViewDto>>(posts);
            return posstDto.OrderByDescending(post => DateTime.Parse(post.AddedDate));
        }
    }
}
