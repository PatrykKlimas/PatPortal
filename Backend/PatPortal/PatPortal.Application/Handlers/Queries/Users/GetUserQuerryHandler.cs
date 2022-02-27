using AutoMapper;
using MediatR;
using PatPortal.Application.Contracts.Querries.Users;
using PatPortal.Application.DTOs.Response.Users;
using PatPortal.Application.Handlers.BaseHandlers;
using PatPortal.Domain.Exceptions;
using PatPortal.Domain.Repositories.Interfaces;
using PatPortal.SharedKernel.Extensions;

namespace PatPortal.Application.Handlers.Queries.Users
{
    public class GetUserQuerryHandler : BaseHandler, IRequestHandler<GetUserQuerry, UserForViewDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetUserQuerryHandler(
            IMapper mapper,
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<UserForViewDto> Handle(GetUserQuerry request, CancellationToken cancellationToken)
        {
            var Id = GetGuidOrThrow("user", request.Id);

            var user = await _userRepository.GetOrDefaultAsync(Id);
            if(user == default)
                throw new EntityNotFoundException($"User with Id: {request.Id} deos not exist.");

            return _mapper.Map<UserForViewDto>(user);
        }
    }
}
