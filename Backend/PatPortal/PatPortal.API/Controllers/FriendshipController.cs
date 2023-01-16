using MediatR;
using Microsoft.AspNetCore.Mvc;
using PatPortal.Application.Contracts.Commands.Friendships;

namespace PatPortal.API.Controllers
{
    [ApiController]
    [Route("api/friendship")]
    public class FriendshipController : AppControllerBase<FriendshipController> 
    {

        public FriendshipController(ILogger<FriendshipController> logger, IMediator mediator, IHttpContextAccessor httpContextAccessor) 
            : base(logger, mediator, httpContextAccessor)
        {
        }
        [HttpPost]
        [Route("invitation/sent/{id}/{friendId}")]
        public async Task<ActionResult> AddFriendAsync(string id, string friendId)
        {
            return await ExecuteResult<AddFriendCommand>(new AddFriendCommand(id, friendId));
        }

        [HttpPatch]
        [Route("invitation/accept/{id}/{friendId}")]
        public async Task<ActionResult> AcceptInvitationAsync(string id, string friendId)
        {
            return await ExecuteResult<AcceptInvitationCommand>(new AcceptInvitationCommand(id, friendId));
        }

        [HttpPatch]
        [Route("invitation/cancel/{id}/{friendId}")]
        public async Task<ActionResult> CancelInvitationAsync(string id, string friendId)
        {
            return await ExecuteResult<CancelInvitationOrRemoveFriendCommand>(new CancelInvitationOrRemoveFriendCommand(id, friendId));
        }

        [HttpDelete]
        [Route("invitation/refuse/{id}/{friendId}")]
        public async Task<ActionResult> RefuseInvitationAsync(string id, string friendId)
        {
            return await ExecuteResult<CancelInvitationOrRemoveFriendCommand>(new CancelInvitationOrRemoveFriendCommand(id, friendId));
        }

        [HttpDelete]
        [Route("delete/{id}/{friendId}")]
        public async Task<ActionResult> DeleteFriendAsync(string id, string friendId)
        {
            return await ExecuteResult<CancelInvitationOrRemoveFriendCommand>(new CancelInvitationOrRemoveFriendCommand(id, friendId));
        }
    }
}
