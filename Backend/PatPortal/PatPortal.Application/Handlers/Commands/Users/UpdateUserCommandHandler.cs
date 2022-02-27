using AutoMapper;
using MediatR;
using PatPortal.Application.Contracts.Commands.Users;
using PatPortal.Domain.Entities.Users.Requests;
using PatPortal.Domain.Exceptions;
using PatPortal.Domain.Services.Interfaces;
using PatPortal.SharedKernel.Extensions;

namespace PatPortal.Application.Handlers.Commands.Users
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userDto = request.User;
            var id = userDto.Id.ParseToGuidOrEmpty();
            if (id == Guid.Empty)
                throw new InitValidationException($"Inncorect user id: {userDto.Id}");

            var userForUpdate = _mapper.Map<UserUpdate>(userDto);
            await _userService.UpdateAsync(userForUpdate);

            return Unit.Value;
        }
    }
}
