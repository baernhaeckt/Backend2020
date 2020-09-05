using Backend.Core.Extensions;
using Backend.Core.Features.Guiding.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Core.Features.Guiding
{
    public static class Registrar
    {
        public static IServiceCollection AddFeatureGuiding(this IServiceCollection services)
        {
            return services.AddStartupTask<GuidesStartupTask>();
        }
    }
}