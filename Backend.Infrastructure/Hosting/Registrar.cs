using Backend.Infrastructure.Abstraction.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Infrastructure.Hosting
{
    public static class Registrar
    {
        public static IServiceCollection AddInfrastructureHosting(this IServiceCollection services)
        {
            services.AddSingleton<IClock, SystemUtcClock>();
            return services;
        }
    }
}