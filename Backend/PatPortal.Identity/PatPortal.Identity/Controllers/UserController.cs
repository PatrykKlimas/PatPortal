using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatPortal.Identity.Domain.Entities;
using PatPortal.Identity.Domain.Enums;
using System.Security.Claims;

namespace PatPortal.Identity.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class UserController : AppControllerBase<UserController>
    {
        public UserController(ILogger<UserController> logger, IMediator mediator) 
            : base(logger, mediator)
        {
        }

        [HttpGet("Public")]
        public ActionResult<string> Public()
        {
            return Ok("Public property");
        }
        
        [HttpGet("ForAdmin")]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminEndpoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Current user {currentUser.FirstName}. You are admin.");
        }

        [HttpGet("ForUser")]
        [Authorize(Roles = "User")]
        public ActionResult UserEndpoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Current user {currentUser.FirstName}. You are user.");
        }

        [HttpGet("ForUserAndAdmin")]
        [Authorize(Roles = "User,Admin")]
        public ActionResult UserAndAdminEndpoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Current user {currentUser.FirstName}. You are admin or admin.");
        }

        private User GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var method = HttpContext.Request.Method;

            if (identity != null)
            {
                var userClaims = identity.Claims;
                var cliamrole = userClaims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value;

                var role = Enum.Parse<Role>(cliamrole);

                return new User()
                {
                    UserName = userClaims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value,
                    Email = new Domain.ValueObjects.Email(userClaims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value),
                    FirstName = userClaims.FirstOrDefault(claim => claim.Type == ClaimTypes.GivenName)?.Value,
                    LastName = userClaims.FirstOrDefault(claim => claim.Type == ClaimTypes.Surname)?.Value,
                    Role = role
                };
            }

            return null;
        }
    }
}
