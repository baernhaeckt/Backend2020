using Backend.Core.Features.Recommendation.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Core.Features.Recommendation
{
    public static class Registrar
    {
        public static IServiceCollection AddFeatureRecommendation(this IServiceCollection services)
        {
            return services
                .AddTransient<IRecommendationService, WebRecommendationService>();
        }
    }
}
