using Microsoft.EntityFrameworkCore;
using PatPortal.Database;
using PatPortal.Domain.Entities.Users;
using PatPortal.Domain.Exceptions;
using PatPortal.Domain.Repositories.Interfaces;
using PatPortal.Domain.ValueObjects;
using PatPortal.Infrastructure.Factories.Interfaces;

namespace PatPortal.Infrastructure.Repositories.Mock
{
    public class UserRepository : IUserRepository
    {
        private readonly PatPortalDbContext _context;
        private readonly IUserMapper _userFactory;

        public UserRepository(PatPortalDbContext context, IUserMapper userFactory)
        {
            _context = context;
            _userFactory = userFactory;
        }
        public async Task<User> AddAsync(User user)
        {
            var userDb = _userFactory.Create(user);

            await _context.AddAsync(userDb);
            await _context.SaveChangesAsync();

            var newUser = await _context.Users.FirstOrDefaultAsync(userDb => user.Id == userDb.Id);

            if (newUser is null)
                throw new InvalidOperationException("User cannot be saved to the database.");

            return _userFactory.Create(newUser);
        }

        public async Task<User> GetOrDefaultAsync(Guid Id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(user => user.Id == Id);

            return user is null ? default : _userFactory.Create(user);
        }

        public async Task<User> GetOrDefaultByEmailAsync(Email email)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(user => user.Email.ToUpper() == email.ToString().ToUpper());

            return user is null ? default : _userFactory.Create(user);
        }

        public async Task<User> UpdateAsync(User user)
        {
            var userDb = _context.Users.FirstOrDefault(u => u.Id == user.Id);

            if (userDb is null)
                throw new EntityNotFoundException($"User with id {user.Id} not found.");

            userDb = _userFactory.Create(user);
            await SaveChangesAsync();

            return _userFactory.Create(userDb);
        }

        private async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }catch
            {
                throw new InvalidOperationException("Unable to save changes. Please try leater.");
            }
        }
    }
}
