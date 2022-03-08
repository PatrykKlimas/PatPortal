using MediatR;
using PatPortal.Identity.Application.Contracts.Commands;

namespace PatPortal.Identity.Application.Hnadlers.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand>
    {
        public Task<Unit> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
