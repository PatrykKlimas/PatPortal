using Microsoft.EntityFrameworkCore;
using PatPortal.Database;
using PatPortal.Domain.Entities.Users;
using PatPortal.Domain.Repositories.Interfaces;
using PatPortal.Domain.ValueObjects;
using PatPortal.Infrastructure.Factories.Interfaces;

namespace PatPortal.Infrastructure.Repositories.Mock
{
    public class UserRepository : IUserRepository
    {
        private readonly PatPortalDbContext _context;
        private readonly IUserFactory _userFactory;

        public UserRepository(PatPortalDbContext context, IUserFactory userFactory)
        {
            _context = context;
            _userFactory = userFactory;
        }
        public async Task<User?> AddAsync(User user)
        {
            var userDb = _userFactory.Create(user);

            await _context.AddAsync(userDb);
            await _context.SaveChangesAsync();

            var newUser = _context.Users.FirstOrDefault(userDb => user.Id == userDb.Id);

            if (newUser == null)
                throw new InvalidOperationException("User cannot be saved to the database.");

            return _userFactory.Create(newUser);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = _context.Users;
            return await users.Select(user => _userFactory.Create(user)).ToListAsync();
        }

        public async Task<User> GetOrDefaultAsync(Guid Id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(user => user.Id == Id);

            if (user == null)
                return default;

            return _userFactory.Create(user);
        }

        public async Task<User> GetOrDefaultByEmailAsync(Email email)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(user => user.Email.ToUpper() == email.ToString().ToUpper());

            if (user == null)
                return default;

            return _userFactory.Create(user);
        }

        public Task<User> UpdateAsync(User user)
        {
            //TODO Implement after register Entity Framework
            throw new NotImplementedException();
        }
    }
}
