using System;
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


        private async Task<PaidOffer> Load(RecommendationResult recommendation)
        {
            return await Reader.GetByIdOrThrowAsync<PaidOffer>(recommendation.OfferId);
        }


        public async IAsyncEnumerable<PaidOffer> Suggest(Guid selectedOfferId)
        {
            var offer = await Reader.GetByIdOrThrowAsync<Offer>(selectedOfferId);

            var recommendations = await RecommendationService.GetPaidOfferRecommendation(offer.Categories);

            foreach (var recommendation in recommendations)
            {
                yield return await Load(recommendation);
            }
        }
    }
}