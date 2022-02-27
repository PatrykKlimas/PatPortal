using PatPortal.Domain.Entities.Users;
using PatPortal.Domain.ValueObjects;

namespace PatPortal.Domain.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetOrDefaultAsync(Guid Id);
        Task<User> GetOrDefaultByEmailAsync(Email email);
        Task<User> AddAsync(User user);
        Task<User> UpdateAsync(User user);
    }
}
