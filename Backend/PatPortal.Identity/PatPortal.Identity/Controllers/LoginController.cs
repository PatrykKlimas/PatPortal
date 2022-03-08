using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatPortal.Identity.Application.Contracts.Commands;
using PatPortal.Identity.Application.DTOs.Request;

namespace PatPortal.Identity.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IMediator _mediator;

        public LoginController(ILogger<LoginController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] UserLoginDto userLogin)
        {
            //intreduce custom exceptions!
            var token = await _mediator.Send(new LoginCommand(userLogin));
            return Ok(token);
        }

    }
}