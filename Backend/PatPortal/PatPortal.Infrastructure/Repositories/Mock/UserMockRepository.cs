using PatPortal.Domain.Entities.Users;
using PatPortal.Domain.Repositories.Interfaces;
using PatPortal.Domain.ValueObjects;

namespace PatPortal.Infrastructure.Repositories.Mock
{
    public class UserMockRepository : IUserRepository
    {
        private IEnumerable<User> _users;

        public UserMockRepository()
        {
            _users = MockDataProvider.MockUsers();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await Task.FromResult(_users);
        }

        public async Task<User> GetOrDefaultAsync(Guid Id)
        {
            var user = _users.Where(user => user.Id == Id).FirstOrDefault();
            if (user == default) return default;

            return await Task.FromResult(user);
        }

        public async Task<User> AddAsync(User user)
        {
            var users = _users.ToList();
            users.Add(user);
            _users = users;

            var createdUser = _users.Where(u => user.Id == u.Id).FirstOrDefault();
            return await Task.FromResult(createdUser);
        }

        public async Task<User> UpdateAsync(User user)
        {
            var userToUpdate = _users.Where(u => u.Id == user.Id).FirstOrDefault();
            userToUpdate = user;
            return await Task.FromResult(userToUpdate);
        }

        public async Task<User> GetOrDefaultByEmailAsync(Email email)
        {
            var user = _users.Where(user => user.Email.ToString()
                            .Equals(email.ToString(), StringComparison.OrdinalIgnoreCase))
                            .FirstOrDefault();

            if (user == default) return default;

            return await Task.FromResult(user);
        }
    }
}
