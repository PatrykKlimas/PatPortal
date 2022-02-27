using MediatR;
using PatPortal.Application.DTOs.Response.Users;

namespace PatPortal.Application.Contracts.Querries.Users
{
    public class GetUserQuerry : IRequest<UserForViewDto>
    {
        public string Id { get; set; }
        public GetUserQuerry(string id)
        {
            Id = id;
        }
    }
}
