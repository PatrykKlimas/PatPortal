namespace PatPortal.SharedKernel.Database
{
    public class EntityFilter<TEntity> where TEntity : class
    {
        public EntityFilter(Func<string, Func<TEntity, bool>> filterFunction, string description)
        {
            FilterFunction = filterFunction;
            Description = description;
        }

        public Func<string, Func<TEntity, bool>> FilterFunction { get; }
        public string Description { get; }

        public IQueryable<TEntity> Filter(IQueryable<TEntity> query, string value)
        {
            return query.Where(FilterFunction.Invoke(value)).AsQueryable();
        }
    }
}
