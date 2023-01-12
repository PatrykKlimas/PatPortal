using AutoMapper;
using MediatR;
using PatPortal.Application.Contracts.Commands.Users;
using PatPortal.Application.Factories.Interfaces;
using PatPortal.Domain.Entities.Users.Requests;
using PatPortal.Domain.Exceptions;
using PatPortal.Domain.Services.Interfaces;
using PatPortal.SharedKernel.Extensions;

namespace PatPortal.Application.Handlers.Commands.Users
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserService _userService;
        private readonly IUserDtoFactory _userDtoFactory;

        public UpdateUserCommandHandler(IUserService userService, IUserDtoFactory userDtoFactory)
        {
            _userService = userService;
            _userDtoFactory = userDtoFactory;
        }
        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userDto = request.User;
            var id = request.Id.ParseToGuidOrEmpty();

            if (id == Guid.Empty)
                throw new InitValidationException($"Inncorect user id: {request.Id}");

            if (!request.User.DayOfBirht.ParsebleToDateTime())
                throw new InitValidationException($"Unable to parse {request.User.DayOfBirht} to DateTime.");

            var userForUpdate = _userDtoFactory.Create(id, request.User);
            await _userService.UpdateAsync(userForUpdate);

            return Unit.Value;
        }
    }
}
