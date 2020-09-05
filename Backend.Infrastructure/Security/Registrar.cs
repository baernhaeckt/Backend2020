using Backend.Infrastructure.Abstraction.Security;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Backend.Infrastructure.Security
{
    public static class Registrar
    {
        public static IServiceCollection AddInfrastructureSecurity(this IServiceCollection services, IHostEnvironment hostEnvironment)
        {
            // Security Utilities
            services.AddSingleton<ISecurityTokenFactory, JwtSecurityTokenFactory>();
            services.AddSingleton<ISecurityKeyProvider, SymmetricSecurityKeyProvider>();
            services.AddSingleton<IPasswordStorage, HmacSha512PasswordStorage>();

            return services;
        }
    }
}