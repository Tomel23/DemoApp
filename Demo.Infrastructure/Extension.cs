
using Microsoft.Extensions.DependencyInjection;
using Demo.Infrastructure.Database;
using Demo.Application.Interfaces;
using Demo.Infrastructure.Database.Repositories;

namespace Demo.Infrastructure
{
    public static class Extension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<DemoContext>();
            services.AddScoped<IAuthenticationTokenRepository, AuthenticationTokenRepository>();

            return services;
        }
    }
}
