using Backend.Core.Extensions;
using Backend.Core.Features.UserManagement.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Core.Features.UserManagement
{
    public static class Registrar
    {
        public static IServiceCollection AddFeatureUserManagement(this IServiceCollection services)
        {
            return services.AddStartupTask<UsersStartupTask>();
        }
    }
}