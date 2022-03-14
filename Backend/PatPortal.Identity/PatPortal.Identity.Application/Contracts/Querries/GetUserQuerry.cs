using MediatR;
using PatPortal.Identity.Application.DTOs.Response;
using System.Security.Claims;

namespace PatPortal.Identity.Application.Contracts.Querries
{
    public class GetUserQuerry : IRequest<UserForViewDto>
    {
        public ClaimsIdentity Claims { get; }
        public GetUserQuerry(ClaimsIdentity claims)
        {
            Claims = claims;
        }
    }
}
