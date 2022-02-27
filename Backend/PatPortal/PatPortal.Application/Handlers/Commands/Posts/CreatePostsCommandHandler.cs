using AutoMapper;
using MediatR;
using PatPortal.Application.Contracts.Commands.Posts;
using PatPortal.Domain.Entities.Posts.Requests;
using PatPortal.Domain.Exceptions;
using PatPortal.Domain.Services.Interfaces;
using PatPortal.SharedKernel.Extensions;

namespace PatPortal.Application.Handlers.Commands.Posts
{
    public class CreatePostsCommandHandler : IRequestHandler<CreatePostsCommand, string>
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public CreatePostsCommandHandler(
            IPostService postService,
            IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }
        public async Task<string> Handle(CreatePostsCommand request, CancellationToken cancellationToken)
        {
            var postDto = request.Post;
            var id = postDto.OwnerId.ParseToGuidOrEmpty();
            if (id == Guid.Empty)
                throw new InitValidationException($"Inncorect user id: {postDto.OwnerId}");

            var postId = await _postService.CreateAsync(_mapper.Map<PostCreate>(postDto));
            return postId.ToString();
        }
    }
}
