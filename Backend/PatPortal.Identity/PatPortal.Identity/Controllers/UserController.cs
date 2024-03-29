﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatPortal.Identity.Application.Contracts.Commands;
using PatPortal.Identity.Application.Contracts.Querries;
using PatPortal.Identity.Application.DTOs.Request;
using PatPortal.Identity.Application.DTOs.Response;
using System.Security.Claims;

namespace PatPortal.Identity.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : AppControllerBase<UserController>
    {
        public UserController(ILogger<UserController> logger, IMediator mediator)
            : base(logger, mediator)
        {
        }

        [HttpPost("Create")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> CreateUserAsync([FromBody] UserForCreationDto user)
        {
            return await ExecuteResult<CreateUserCommand, string>(new CreateUserCommand(user));
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserCredentialsDto>> LoginAsync([FromBody] UserLoginDto userLogin)
        {
            return await ExecuteResult<LoginCommand, UserCredentialsDto>(new LoginCommand(userLogin));
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<UserForViewDto>> GetAsync()
        {
            var claims = HttpContext.User.Identity as ClaimsIdentity;
            return await ExecuteResult<GetUserQuerry, UserForViewDto>(new GetUserQuerry(claims));
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteAsync(string userId)
        {
            return await ExecuteResult<DeleteUserCommand>(new DeleteUserCommand(userId));
        }
            
            
    }
}
