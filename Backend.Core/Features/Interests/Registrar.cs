using Backend.Core.Features.Interests.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Core.Features.Interests
{
    public static class Registrar
    {
        public static IServiceCollection AddFeatureInterests(this IServiceCollection services)
        {
            return services.AddTransient<InterestsService>();
        }
    }
}