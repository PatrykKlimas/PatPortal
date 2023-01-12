using PatPortal.Application.DTOs.Request.Users;
using PatPortal.Domain.Entities.Users.Requests;

namespace PatPortal.Application.Factories.Interfaces
{
    public interface IUserDtoFactory
    {
        UserUpdate Create(Guid id, UserForUpdateDto userForUpdate);
    }
}
