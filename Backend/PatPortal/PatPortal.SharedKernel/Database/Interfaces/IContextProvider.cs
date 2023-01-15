using Microsoft.EntityFrameworkCore;

namespace PatPortal.SharedKernel.Database.Interfaces
{
    public interface IContextProvider<TContext> where TContext : DbContext
    {
        Task RunAsync(Func<TContext, Task> request);

        Task<T> RunAsync<T>(Func<TContext, Task<T>> request);
    }
}
