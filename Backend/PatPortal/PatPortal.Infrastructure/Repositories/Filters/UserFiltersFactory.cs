using PatPortal.Database.Models;
using PatPortal.Domain.Filters;
using PatPortal.SharedKernel.Database.Filtration;
using PatPortal.SharedKernel.Database.Interfaces;

namespace PatPortal.Infrastructure.Repositories.Filters
{
    public class UserFiltersFactory : BaseEntityFiltersFactory<User>, IUserFilters
    {
        public string EmailEqual => "emailEqual";

        public UserFiltersFactory()
        {
            _filters.Add(
                EmailEqual,
                new EntityFilter<User>(
                    value => user => user.Email == value,
                    "Returns users whoses email equals."));
        }
    }
}
