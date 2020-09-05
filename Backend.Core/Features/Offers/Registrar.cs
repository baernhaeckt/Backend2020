using Backend.Core.Extensions;
using Backend.Core.Features.Offers.Data;
using Backend.Core.Features.Offers.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Core.Features.Offers
{
    public static class Registrar
    {
        public static IServiceCollection AddFeatureOffers(this IServiceCollection services)
        {
            return services
                .AddTransient<IOfferService, OfferService>()
                .AddStartupTask<OffersStartupTask>();
        }
    }
}
