using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatPortal.Identity.Application.Contracts.Commands;
using PatPortal.Identity.Application.DTOs.Request;

namespace PatPortal.Identity.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : AppControllerBase<LoginController>
    {
        public LoginController(ILogger<LoginController> logger, IMediator mediator) 
            : base(logger, mediator)
        {
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] UserLoginDto userLogin)
        {
            return await ExecuteResult<LoginCommand, string>(new LoginCommand(userLogin));
        }

    }
}