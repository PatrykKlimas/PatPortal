using Microsoft.EntityFrameworkCore;
using PatPortal.SharedKernel.Database.Interfaces;

namespace PatPortal.SharedKernel.Database
{
    public class ContextProvider<TContext> : IContextProvider<TContext> where TContext : DbContext
    {
        private readonly DbContextOptions _dbContextOptions;

        public ContextProvider(DbContextOptions dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }
        public async Task RunAsync(Func<TContext, Task> request)
        {
            var context = CreateInstance();

            await request(context);
        }

        public async Task<T> RunAsync<T>(Func<TContext, Task<T>> request)
        {
            var context = CreateInstance();

            return await request(context);
        }

        TContext CreateInstance() => (TContext)Activator.CreateInstance(typeof(TContext), _dbContextOptions);
    }
}
