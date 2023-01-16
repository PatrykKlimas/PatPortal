using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PatPortal.API.Controllers
{
    public abstract class AppControllerBase<TController> : ControllerBase 
    {
        protected readonly ILogger<TController> _logger;
        protected readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppControllerBase(
            ILogger<TController> logger, 
            IMediator mediator,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ActionResult<TResult>> ExecuteResult<TQuerry, TResult>(TQuerry request) 
            where TQuerry : IRequest<TResult>
        {
            var result = await _mediator.Send(request);
            var method = _httpContextAccessor.HttpContext.Request.Method;

            if (method == HttpMethod.Post.ToString())
                return Created("Created", result);

            return Ok(result);
        }

        public async Task<ActionResult> ExecuteResult<TQuerry>(TQuerry request)
            where TQuerry : IRequest
        {
            await _mediator.Send(request);
            var method = _httpContextAccessor.HttpContext.Request.Method;

            if (method == HttpMethod.Delete.ToString())
                return NoContent();

            return Ok();
        }
    }
}
