using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Core.Entities;
using Backend.Core.Features.Offers.Models;
using Backend.Core.Features.Recommendation.Services;
using Backend.Infrastructure.Abstraction.Persistence;
using Backend.Models;

namespace Backend.Core.Features.Offers.Services
{
    public class OfferService : IOfferService
    {
        private readonly IWriter _writer;

        private readonly IRecommendationService _recommendationService;

        public OfferService(IWriter writer, IRecommendationService recommendationService)
        {
            _writer = writer;
            _recommendationService = recommendationService;
        }

        public async IAsyncEnumerable<OfferResponse> All()
        {
            foreach (var offer in await _writer.GetAllAsync<Offer>())
            {
                var offerResponse = new OfferResponse();
                offerResponse.From(offer);
                var user = await _writer.GetByIdOrThrowAsync<User>(offer.GuideId);
                offerResponse.GuideDisplayName = user.DisplayName;
                yield return offerResponse;
            }
        }

        public async Task<OfferResponse> Store(OfferResponse offer)
        {
            await _writer.InsertAsync(offer.To());
            return offer;
        }

        public async IAsyncEnumerable<OfferResponse> GetSuggested(IEnumerable<Interest> interests)
        {
            var recommendations = await _recommendationService
                .GetOfferRecommendation(interests.Where(i => i.Match).Select(i => i.Name).ToList());

            foreach (var recommendation in recommendations)
            {
                yield return await Load(recommendation);
            }
        }

        private async Task<OfferResponse> Load(RecommendationResult recommendation)
        {
            var offer = await _writer.GetByIdOrThrowAsync<Offer>(recommendation.OfferId);

            var offerResponse = new OfferResponse();
            offerResponse.From(offer);
            var user = await _writer.GetByIdOrThrowAsync<User>(offer.GuideId);
            offerResponse.GuideDisplayName = user.DisplayName;
            return offerResponse;
        }

        public Task<OfferBookingResult> Book(OfferBookingRequest offer)
        {
            throw new System.NotImplementedException();
        }
    }
}
