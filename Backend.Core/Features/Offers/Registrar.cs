using Backend.Core.Extensions;
using Backend.Web.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Core.Features.Offers
{
    public static class Registrar
    {
        public static IServiceCollection AddFeatureOffers(this IServiceCollection services)
        {
            return services
                .AddTransient<IOfferService, OfferService>()
                .AddStartupTask<Data.PaidOffersStartupTask>();
        }
    }
}
