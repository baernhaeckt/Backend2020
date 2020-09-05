using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Core.Entities;
using Backend.Core.Features.Offers.Models;
using Backend.Core.Features.Recommendation.Services;
using Backend.Infrastructure.Abstraction.Persistence;

namespace Backend.Core.Features.PaidOffers.Services
{
    public class PersistentPaidOffersService : IPaidOffersService
    {
        public PersistentPaidOffersService(IReader reader, IRecommendationService recommendationService)
        {
            Reader = reader;
            RecommendationService = recommendationService;
        }

        private IReader Reader { get; }

        private IRecommendationService RecommendationService { get; }

        public Task<IEnumerable<PaidOffer>> All => Reader.GetAllAsync<PaidOffer>();


        public async IAsyncEnumerable<PaidOffer> Suggest(Offer selectedOffer)
        {
            var recommendations = await RecommendationService
                .GetOfferRecommendation(selectedOffer.Categories);

            foreach (var recommendation in recommendations)
            {
                yield return await Load(recommendation);
            }
        }

        private async Task<PaidOffer> Load(RecommendationResult recommendation)
        {
            return await Reader.GetByIdOrThrowAsync<PaidOffer>(recommendation.OfferId);
        }

        public IAsyncEnumerable<PaidOffer> Suggest(OfferResponse selectedOffer)
        {
            throw new System.NotImplementedException();
        }
    }
}