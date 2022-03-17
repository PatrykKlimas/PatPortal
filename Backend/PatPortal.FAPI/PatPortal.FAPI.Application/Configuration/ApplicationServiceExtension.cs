using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace PatPortal.FAPI.Application.Configuration
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
