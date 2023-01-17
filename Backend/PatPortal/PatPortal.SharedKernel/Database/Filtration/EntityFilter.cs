using System.Linq.Expressions;

namespace PatPortal.SharedKernel.Database.Filtration
{
    public class EntityFilter<TEntity> where TEntity : class
    {
        public EntityFilter(Func<string, Expression<Func<TEntity, bool>>> filterFunction, string description)
        {
            FilterFunction = filterFunction;
            Description = description;
        }

        public Func<string, Expression<Func<TEntity, bool>>> FilterFunction { get; }
        public string Description { get; }

        public IQueryable<TEntity> Filter(IQueryable<TEntity> query, string value)
        {
            return query.Where(FilterFunction.Invoke(value)).AsQueryable();
        }
    }
}
