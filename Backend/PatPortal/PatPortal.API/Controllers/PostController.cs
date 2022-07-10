using MediatR;
using Microsoft.AspNetCore.Mvc;
using PatPortal.Application.Contracts.Commands.Comments;
using PatPortal.Application.Contracts.Commands.Posts;
using PatPortal.Application.Contracts.Querries.Comments;
using PatPortal.Application.Contracts.Querries.Posts;
using PatPortal.Application.DTOs.Request.Comments;
using PatPortal.Application.DTOs.Request.Posts;
using PatPortal.Application.DTOs.Response.Comments;
using PatPortal.Application.DTOs.Response.Posts;

namespace PatPortal.API.Controllers
{
    [ApiController]
    [Route("api/post")]
    public class PostController : AppControllerBase<PostController>
    {
        public PostController(ILogger<PostController> logger, IMediator mediator) :
            base(logger, mediator)
        {
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreatePost([FromBody] PostForCreationDto postToCreate)
        {
            return await ExecuteResult<CreatePostsCommand, string>(new CreatePostsCommand(postToCreate), HttpMethod.Post);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdatePost([FromBody] PostForUpdateDto postToUpdate)
        {
            return await ExecuteResult<UpdatePostsCommand>(new UpdatePostsCommand(postToUpdate));
        }

        //ToDo - move all comments endpoints to the separate endpoint

        [HttpGet("comment/{id}")]
        public async Task<ActionResult<CommentForViewDto>> GetCommentsAsync(string id)
        {
            return await ExecuteResult<GetCommentQuery, CommentForViewDto>(new GetCommentQuery(id), HttpMethod.Get);
        }
        [HttpPost("{id}/comment")]
        public async Task<ActionResult<string>> CreateComment(string id, [FromBody] CommentForCreationDto postToUpdate)
        {
            return await ExecuteResult<CreateCommentCommand, string>(new CreateCommentCommand(id , postToUpdate.Ownerid, postToUpdate.Content), HttpMethod.Post); 
        }

        [HttpGet("{userId}/{requestorId}")]
        public async Task<ActionResult<IEnumerable<PostForViewDto>>> GetByUser(string userId, string requestorId)
        {
            return await ExecuteResult<GetPostsByUserQuerry, IEnumerable<PostForViewDto>>(new GetPostsByUserQuerry(userId, requestorId), HttpMethod.Get);
        }

        [HttpGet("{requestorId}")]
        public async Task<ActionResult<IEnumerable<PostForViewDto>>> GetForUserToSee(string requestorId)
        {
            return await ExecuteResult<GetPostsForUserToSeeQuerry, IEnumerable<PostForViewDto>>(new GetPostsForUserToSeeQuerry(requestorId), HttpMethod.Get);
        }

        [HttpGet("{postId}/comments")]
        public async Task<ActionResult<IEnumerable<CommentForViewDto>>> GetComments(string postId)
        {
            return await ExecuteResult<GetPostCommentsQuerry, IEnumerable<CommentForViewDto>>(new GetPostCommentsQuerry(postId), HttpMethod.Get);
        }


    }
}
