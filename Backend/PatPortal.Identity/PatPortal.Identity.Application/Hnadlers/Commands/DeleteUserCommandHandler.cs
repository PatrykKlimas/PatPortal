using MediatR;
using PatPortal.Identity.Application.Contracts.Commands;
using PatPortal.Identity.Domain.Exceptions;
using PatPortal.Identity.Domain.Repositories;

namespace PatPortal.Identity.Application.Hnadlers.Commands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userId = request.UserId;

            Guid globalId;
            var parsableToGuid = Guid.TryParse(userId, out globalId);

            if (!parsableToGuid)
                throw new InitValidationException($"{userId} is invalid guid.");

            await _userRepository.DeleteAsync(globalId);

            return Unit.Value;
        }
    }
}
