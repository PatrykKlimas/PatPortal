using AutoMapper;
using MediatR;
using PatPortal.Application.Contracts.Commands.Posts;
using PatPortal.Domain.Entities.Posts.Requests;
using PatPortal.Domain.Exceptions;
using PatPortal.Domain.Services.Interfaces;
using PatPortal.SharedKernel.Extensions;

namespace PatPortal.Application.Handlers.Commands.Posts
{
    public class UpdatePostsCommandHandler : IRequestHandler<UpdatePostsCommand>
    {
        private readonly IMapper _mapper;
        private readonly IPostService _postService;

        public UpdatePostsCommandHandler(
            IMapper mapper, 
            IPostService postService)
        {
            _mapper = mapper;
            _postService = postService;
        }
        public async Task<Unit> Handle(UpdatePostsCommand request, CancellationToken cancellationToken)
        {
            var postDto = request.Post;

            var id = postDto.OwnerId.ParseToGuidOrEmpty();
            if (id == Guid.Empty)
                throw new InitValidationException($"Inncorect user id: {postDto.OwnerId}");

            var postId = postDto.Id.ParseToGuidOrEmpty();
            if(postId == Guid.Empty)
                throw new InitValidationException($"Inncorect post id: {postDto.OwnerId}");

            await _postService.UpdateAsync(_mapper.Map<PostUpdate>(postDto));
            return Unit.Value;
        }
    }
}
