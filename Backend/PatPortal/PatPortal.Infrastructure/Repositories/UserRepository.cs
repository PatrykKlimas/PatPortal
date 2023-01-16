using Microsoft.EntityFrameworkCore;
using PatPortal.Database;
using PatPortal.Domain.Entities.Users;
using PatPortal.Domain.Exceptions;
using PatPortal.Domain.Repositories.Interfaces;
using PatPortal.Domain.ValueObjects;
using PatPortal.Infrastructure.Factories.Interfaces;
using PatPortal.Infrastructure.Repositories.Filters;
using PatPortal.SharedKernel.Database;
using PatPortal.SharedKernel.Database.Interfaces;

namespace PatPortal.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IContextProvider<PatPortalDbContext> _contextProvider;
        private readonly IUserMapper _userFactory;

        public UserRepository(IContextProvider<PatPortalDbContext> contextProvider, IUserMapper userFactory)
        {
            _contextProvider = contextProvider;
            _userFactory = userFactory;
        }

        public async Task<User> AddAsync(User user)
        {
            return await _contextProvider.RunAsync(async context =>
            {
                var userDb = _userFactory.Create(user);

                context.Add(userDb);
                await SaveChangesAsync(context);

                var newUser = await context.Users.FirstOrDefaultAsync(userDb => user.Id == userDb.Id);

                if (newUser is null)
                    throw new InvalidOperationException("User cannot be saved to the database.");

                return _userFactory.Create(newUser);
            });
        }

        public async Task<User> GetOrDefaultAsync(Guid Id)
        {
            return await _contextProvider.RunAsync(async context =>
            {
                var user = await context.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(user => user.Id == Id);

                return user is null ? default : _userFactory.Create(user);
            });
        }

        //Implement filters
        public async Task<User> GetOrDefaultByEmailAsync(Email email)
        {
            return await _contextProvider.RunAsync(async context =>
            {
                var user = await context.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(user => user.Email.ToUpper() == email.ToString().ToUpper());

                return user is null ? default : _userFactory.Create(user);
            });
        }

        public async Task<User> UpdateAsync(User user)
        {
            return await _contextProvider.RunAsync(async context =>
            {
                var userDb = context.Users.FirstOrDefault(u => u.Id == user.Id);

                if (userDb is null)
                    throw new EntityNotFoundException($"User with id {user.Id} not found.");

                userDb.LastName = user.LastName;
                userDb.Email = user.Email.ToString();
                userDb.Profession = user.Profession;
                userDb.DayOfBirth = user.DayOfBirht;
                userDb.Photo = user.Photo;

                await SaveChangesAsync(context);

                return _userFactory.Create(userDb);
            });
        }

        private async Task SaveChangesAsync(PatPortalDbContext context)
        {
            try
            {
                await context.SaveChangesAsync();
            }catch
            {
                throw new InvalidOperationException("Unable to save changes. Please try leater.");
            }
        }

        //private IQueryable<User> Filter(this IQueryable<User> userQuery, IDictionary<string, string> filters)
        //{
        //    foreach (var filter in filters)
        //    {

        //    }
        //    if (key == UserFilters.EmailEqual)
        //        return userQuery.Where(user => user.Email.ToString() == value);

        //    return userQuery;
        //}
    }
}
