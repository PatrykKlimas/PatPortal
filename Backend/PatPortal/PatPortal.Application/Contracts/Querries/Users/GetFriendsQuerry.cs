using MediatR;
using PatPortal.Application.DTOs.Response.Users;

namespace PatPortal.Application.Contracts.Querries.Users
{
    public class GetFriendsQuerry : IRequest<IEnumerable<UserForViewDto>>
    {
        public string Id { get; }
        public GetFriendsQuerry(string id)
        {
            Id = id;
        }

    }
}
