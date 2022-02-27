using AutoMapper;
using MediatR;
using PatPortal.Application.DTOs.Response.Users;
using PatPortal.Domain.Entities.Users;
using PatPortal.Domain.Exceptions;
using PatPortal.Domain.Services.Interfaces;
using PatPortal.SharedKernel.Extensions;

namespace PatPortal.Application.Handlers.BaseHandlers
{
    public abstract class BaseFriendsHandler<T> : BaseHandler, IRequestHandler<T, IEnumerable<UserForViewDto>> where T : IRequest<IEnumerable<UserForViewDto>>
    {
        protected readonly IUserService _userServices;
        protected readonly IMapper _mapper;
        public BaseFriendsHandler(
            IUserService userServices, 
            IMapper mapper)
        {
            _mapper = mapper;
            _userServices = userServices;
        }
        public abstract Task<IEnumerable<UserForViewDto>> Handle(T request, CancellationToken cancellationToken);

        protected async Task<IEnumerable<UserForViewDto>> ReturnAsync(Func<Guid, Task<IEnumerable<User>>> actionAsync, string userId)
        {
            var id = GetGuidOrThrow("user", userId);
            var users = await actionAsync(id);

            return _mapper.Map<IEnumerable<UserForViewDto>>(users);
        }
    }
}
