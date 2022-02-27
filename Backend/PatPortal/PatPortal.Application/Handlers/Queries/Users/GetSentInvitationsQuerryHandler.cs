using AutoMapper;
using PatPortal.Application.Contracts.Querries.Users;
using PatPortal.Application.DTOs.Response.Users;
using PatPortal.Application.Handlers.BaseHandlers;
using PatPortal.Domain.Services.Interfaces;

namespace PatPortal.Application.Handlers.Queries.Users
{
    internal class GetSentInvitationsQuerryHandler : BaseFriendsHandler<GetSentInvitationsQuerry>
    {
        public GetSentInvitationsQuerryHandler(
            IUserService userServices, 
            IMapper mapper ) : base( userServices, mapper )
        {
        }

        public override Task<IEnumerable<UserForViewDto>> Handle(GetSentInvitationsQuerry request, CancellationToken cancellationToken)
        {
            return ReturnAsync(_userServices.GetSendInvitationsAsync, request.Id);
        }
    }
}
