namespace PatPortal.SharedKernel.Database.Filtration.Interfaces
{
    public interface IEntityFiltersFactory<TEntity> where TEntity : class
    {
        IDictionary<string, string> GetFilters();
        IQueryable<TEntity> Filter(IQueryable<TEntity> users, IDictionary<string, string> filters);
    }
}
