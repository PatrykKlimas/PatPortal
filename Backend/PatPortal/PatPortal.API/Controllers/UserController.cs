using MediatR;
using Microsoft.AspNetCore.Mvc;
using PatPortal.Application.Contracts.Commands.Users;
using PatPortal.Application.Contracts.Querries.Users;
using PatPortal.Application.DTOs.Request.Users;
using PatPortal.Application.DTOs.Response.Users;

namespace PatPortal.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : AppControllerBase<UserController>
    {
        public UserController(ILogger<UserController> logger, IMediator mediator)
            : base(logger, mediator)
        {
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<UserForViewDto>> GetAsync(string id)
        {
            return await ExecuteResult<GetUserQuerry, UserForViewDto>(new GetUserQuerry(id), HttpMethod.Get);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<string>> CreateAsync([FromBody] UserForCreationDto user)
        {
            return await ExecuteResult<CreateUserCommand, string>(new CreateUserCommand(user), HttpMethod.Post);
        }

        [HttpGet]
        [Route("{id}/friends")]
        public async Task<ActionResult<IEnumerable<UserForViewDto>>> GetFriends(string id)
        {
            return await ExecuteResult<GetFriendsQuerry, IEnumerable<UserForViewDto>>(new GetFriendsQuerry(id), HttpMethod.Get);
        }
        [HttpGet]
        [Route("{id}/invitations")]
        public async Task<ActionResult<IEnumerable<UserForViewDto>>> GetInvitations(string id)
        {
            return await ExecuteResult<GetInvitationsQuerry, IEnumerable<UserForViewDto>>(new GetInvitationsQuerry(id), HttpMethod.Get);
        }
        [HttpGet]
        [Route("{id}/sentInvitations")]
        public async Task<ActionResult<IEnumerable<UserForViewDto>>> GetSentInvitations(string id)
        {
            return await ExecuteResult<GetSentInvitationsQuerry, IEnumerable<UserForViewDto>>(new GetSentInvitationsQuerry(id), HttpMethod.Get);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult> UpdateAsync([FromBody] UserForUpdateDto user)
        {
            return await ExecuteResult<UpdateUserCommand>(new UpdateUserCommand(user));
        }


    }
}