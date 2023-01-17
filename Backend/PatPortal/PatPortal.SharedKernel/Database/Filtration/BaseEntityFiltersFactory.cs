using PatPortal.SharedKernel.Database.Filtration.Interfaces;
using PatPortal.SharedKernel.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.SharedKernel.Database.Filtration
{
    public abstract class BaseEntityFiltersFactory<TEntity> : IEntityFiltersFactory<TEntity> where TEntity : class
    {
        protected readonly IDictionary<string, EntityFilter<TEntity>> _filters =
            new Dictionary<string, EntityFilter<TEntity>>();

        public IQueryable<TEntity> Filter(IQueryable<TEntity> entities, IDictionary<string, string> filters)
        {
            foreach (var filter in filters)
            {
                var filterToApply = _filters
                    .Where(f => f.Key.ToLower() == filter.Key.ToLower())
                    .Select(f => f.Key)
                    .FirstOrDefault();

                if (filterToApply is null)
                    throw new EntryPointNotFoundException($"Filter {filter.Key} does not exist");

                var filterExpression = _filters[filterToApply].FilterFunction(filter.Value);
                entities = entities.Where(filterExpression).AsQueryable();
            }

            return entities;
        }

        public IDictionary<string, string> GetFilters()
        {
            return _filters.ToDictionary(filter => filter.Key, filter => filter.Value.Description);
        }
    }
}
