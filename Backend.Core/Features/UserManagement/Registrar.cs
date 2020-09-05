using Backend.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Core.Features.UserManagement
{
    public static class Registrar
    {
        public static IServiceCollection AddFeatureUserManagement(this IServiceCollection services)
        {
            return services.AddStartupTask<Data.UsersStartupTask>();
        }
    }
}
