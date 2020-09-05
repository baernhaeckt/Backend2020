using Backend.Core.Extensions;
using Backend.Core.Features.PaidOffers.Data;
using Backend.Core.Features.PaidOffers.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Core.Features.PaidOffers
{
    public static class Registrar
    {
        public static IServiceCollection AddFeaturePaidOffers(this IServiceCollection services)
        {
            return services
                .AddTransient<IPaidOffersService, PersistentPaidOffersService>()
                .AddStartupTask<PaidOffersStartupTask>();
        }
    }
}
