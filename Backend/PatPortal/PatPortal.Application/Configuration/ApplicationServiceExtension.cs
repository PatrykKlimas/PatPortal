using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Application.Configuration
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
