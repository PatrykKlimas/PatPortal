using PatPortal.Database.Models;
using PatPortal.Domain.Filters;
using PatPortal.SharedKernel.Database;
using PatPortal.SharedKernel.Database.Interfaces;
using System.Linq;

namespace PatPortal.Infrastructure.Repositories.Filters
{
    public class UserFiltersFactory : IEntityFiltersFactory<User>, IUserFilters
    {
        public string EmailEqual => "emailEqual";

        private readonly IDictionary<string, EntityFilter<User>> _filters;

        public UserFiltersFactory()
        {
            _filters = new Dictionary<string, EntityFilter<User>>()
            {
                {
                    EmailEqual,
                    new EntityFilter<User>(
                        value => user => user.Email == value,
                        "Returns users whoses email equals.")
                }
            };
        }

        public IDictionary<string, string> GetFilters()
        {
            return _filters.ToDictionary(filter => filter.Key, filter => filter.Value.Description);
        }

        public IQueryable<User> Filter(IQueryable<User> users, IDictionary<string, string> filters)
        {
            foreach (var filter in filters)
            {
                var filterToApply = _filters
                    .Where(f => f.Key.ToLower() == filter.Key.ToLower())
                    .Select(f => f.Key)
                    .FirstOrDefault();

                if (filterToApply is null)
                    throw new EntryPointNotFoundException($"Filter {filter.Key} does not exist");

                var filterFunction = _filters[filterToApply].FilterFunction(filter.Value);
                users = users.Where(filterFunction);
            }

            return users;
        }
    }
}
