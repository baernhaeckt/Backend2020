using Backend.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Core.Features.Guiding
{
    public static class Registrar
    {
        public static IServiceCollection AddFeatureGuiding(this IServiceCollection services)
        {
            return services.AddStartupTask<Data.GuidesStartupTask>();
        }
    }
}
