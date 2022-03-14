using AutoMapper;
using MediatR;
using PatPortal.Identity.Application.Contracts.Querries;
using PatPortal.Identity.Application.DTOs.Response;
using PatPortal.Identity.Domain.Services.Interfaces;

namespace PatPortal.Identity.Application.Hnadlers.Querries
{
    public class GetUserQuerryHandler : IRequestHandler<GetUserQuerry, UserForViewDto>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetUserQuerryHandler(
            IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<UserForViewDto> Handle(GetUserQuerry request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetAsync(request.Claims);
            return _mapper.Map<UserForViewDto>(user);
        }
    }
}
