using Backend.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Core.Features.Offers
{
    public static class Registrar
    {
        public static IServiceCollection AddFeatureOffers(this IServiceCollection services)
        {
            return services.AddStartupTask<Data.OffersStartupTask>();
        }
    }
}
