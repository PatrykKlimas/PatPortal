using MediatR;
using PatPortal.Application.DTOs.Response.Users;

namespace PatPortal.Application.Contracts.Querries.Users
{
    public class GetSentInvitationsQuerry : IRequest<IEnumerable<UserForViewDto>>
    {
        public string Id { get; }
        public GetSentInvitationsQuerry(string id)
        {
            this.Id = id;
        }
    }
}
