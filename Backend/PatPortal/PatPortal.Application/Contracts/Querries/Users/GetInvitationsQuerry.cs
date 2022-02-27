using MediatR;
using PatPortal.Application.DTOs.Response.Users;

namespace PatPortal.Application.Contracts.Querries.Users
{
    public  class GetInvitationsQuerry : IRequest<IEnumerable<UserForViewDto>>
    {
        public string Id { get; }
        public GetInvitationsQuerry(string id)
        {
            Id = id;
        }

    }
}
