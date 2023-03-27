using Demo.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Application
{
    public static class Extension
    {
        public static IServiceCollection AddApplication (this IServiceCollection services)
        {
            services.AddScoped<IDemoDataService, DemoDataService>();
            services.AddScoped<IAuthenticatonService, AuthenticationService>();
            return services;
        }
    }
}
