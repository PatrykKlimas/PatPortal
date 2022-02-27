using AutoMapper;
using PatPortal.Application.Contracts.Querries.Users;
using PatPortal.Application.DTOs.Response.Users;
using PatPortal.Application.Handlers.BaseHandlers;
using PatPortal.Domain.Services.Interfaces;

namespace PatPortal.Application.Handlers.Queries.Users
{
    public class GetFriendsQuerryHandler : BaseFriendsHandler<GetFriendsQuerry>
    {

        public GetFriendsQuerryHandler(
            IUserService userServices, 
            IMapper mapper) : 
            base(userServices, mapper)
        {
        }

        public override async Task<IEnumerable<UserForViewDto>> Handle(GetFriendsQuerry request, CancellationToken cancellationToken)
        {
            return await ReturnAsync(_userServices.GetFriendsAsync, request.Id);
        }
    }
}
