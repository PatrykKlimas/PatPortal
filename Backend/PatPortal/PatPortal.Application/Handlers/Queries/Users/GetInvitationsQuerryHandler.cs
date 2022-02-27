using AutoMapper;
using PatPortal.Application.Contracts.Querries.Users;
using PatPortal.Application.DTOs.Response.Users;
using PatPortal.Application.Handlers.BaseHandlers;
using PatPortal.Domain.Services.Interfaces;

namespace PatPortal.Application.Handlers.Queries.Users
{
    public class GetInvitationsQuerryHandler : BaseFriendsHandler<GetInvitationsQuerry>
    {
        public GetInvitationsQuerryHandler(
            IUserService userServices, 
            IMapper mapper) : base(userServices, mapper)
        {
        }

        public override Task<IEnumerable<UserForViewDto>> Handle(GetInvitationsQuerry request, CancellationToken cancellationToken)
        {
            return ReturnAsync(_userServices.GetInvitationsAsync, request.Id);
        }
    }
}
