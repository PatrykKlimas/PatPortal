using PatPortal.Domain.Entities.Users;

namespace PatPortal.Domain.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetOrDefaultAsync(Guid Id);
        Task<IEnumerable<User>> GetAsync(IDictionary<string, string> filters);
        Task<User> AddAsync(User user);
        Task<User> UpdateAsync(User user);
    }
}
