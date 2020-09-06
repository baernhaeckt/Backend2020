using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Core.Entities;
using Backend.Core.Features.Recommendation.Services;
using Backend.Infrastructure.Abstraction.Persistence;

namespace Backend.Core.Features.PaidOffers.Services
{
    public class PersistentPaidOffersService : IPaidOffersService
    {
        private readonly IReader _reader;

        private readonly IRecommendationService _recommendationService;

        public PersistentPaidOffersService(IReader reader, IRecommendationService recommendationService)
        {
            _reader = reader;
            _recommendationService = recommendationService;
        }

        public Task<IEnumerable<PaidOffer>> All => _reader.GetAllAsync<PaidOffer>();

        private async Task<PaidOffer> Load(RecommendationResult recommendation)
            => await _reader.GetByIdOrThrowAsync<PaidOffer>(recommendation.OfferId);

        public async IAsyncEnumerable<PaidOffer> Suggest(Guid selectedOfferId)
        {
            var offer = await _reader.GetByIdOrThrowAsync<Offer>(selectedOfferId);

            var recommendations = await _recommendationService.GetPaidOfferRecommendation(offer.Categories);

            foreach (var recommendation in recommendations)
            {
                yield return await Load(recommendation);
            }
        }
    }
}