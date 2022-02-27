using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PatPortal.API.Controllers
{
    public abstract class AppControllerBase<TController> : ControllerBase 
    {
        protected readonly ILogger<TController> _logger;
        protected readonly IMediator _mediator;

        public AppControllerBase(ILogger<TController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<ActionResult<TResult>> ExecuteResult<TQuerry, TResult>(TQuerry request) 
            where TQuerry : IRequest<TResult>
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        public async Task<ActionResult> ExecuteResult<TQuerry>(TQuerry request)
            where TQuerry : IRequest
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
