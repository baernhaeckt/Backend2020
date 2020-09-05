using System;
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
        private readonly IRecommendationService recommendationService;

        public OfferService(IWriter writer, IRecommendationService recommendationService)
        {
            _writer = writer;
            this.recommendationService = recommendationService;
        }

        public async IAsyncEnumerable<Offer> All()
        {
            foreach (var dbOffer in await _writer.GetAllAsync<OfferDbItem>())
            {
                var offer = new Offer();
                offer.From(dbOffer);
                yield return offer;
            }
        }

        public async Task<Offer> Store(Offer offer)
        {
            await _writer.InsertAsync(offer.To());
            return offer;
        }

        public async IAsyncEnumerable<Offer> GetSuggested(IEnumerable<Interest> interests)
        {
            var recommendations = await recommendationService
                .GetOfferRecommendation(interests.Where(i => i.Match).Select(i => i.Name).ToList());

            foreach (var recommendation in recommendations)
            {
                yield return await Load(recommendation);
            }
        }

        private async Task<Offer> Load(RecommendationResult recommendation)
        {
            var dbOffer = await _writer.GetByIdOrThrowAsync<OfferDbItem>(recommendation.OfferId);

            var offer = new Offer();
            offer.From(dbOffer);        // TODO: could probably be a static factory method?!
            offer.Guide = await GetGuide(dbOffer.GuideId);
            return offer;
        }

        public Task<OfferBookingResult> Book(OfferBookingRequest offer)
        {
            throw new System.NotImplementedException();
        }

        private async Task<Guide> GetGuide(Guid guid)
        {
            var user = await _writer.GetByIdOrThrowAsync<User>(guid);

            return new Guide
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Description = user.Description,
                Birthday = DateTime.Now,            // TODO: Should be implemented with correct value
                Languages = user.Languages.Select(l => new System.Globalization.CultureInfo(l)).ToList()
            };
        }
    }
}
