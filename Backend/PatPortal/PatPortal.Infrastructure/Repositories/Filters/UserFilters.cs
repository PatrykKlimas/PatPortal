using PatPortal.Database.Models;

namespace PatPortal.Infrastructure.Repositories.Filters
{
    public class UserFilters
    {
        public string EmailEqual => "emailEqual";

        private readonly IDictionary<string, FilterProvider<User>> _filters;

        public UserFilters()
        {
            _filters = new Dictionary<string, FilterProvider<User>>()
            {
                {
                    EmailEqual,
                    new FilterProvider<User>(
                        value => user => user.Email == value,
                        "Returns users whoses email equals.")
                }
            };
        }

        public IDictionary<string, string> GetFilters()
        {
            return _filters.ToDictionary(filter => filter.Key, filter => filter.Value.Description);
        }
    }
}
