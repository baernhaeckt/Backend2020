using Backend.Infrastructure.Abstraction.Persistence;
using Backend.Models;
using Backend.Web.MongoDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Web.Services
{
    public class OfferService : IOfferService
    {
        private IWriter Writer { get; }

        public OfferService(IWriter writer)
        {
            Writer = writer;
        }

        private Offer Convert(OfferDbItem dbOffer)
        {
            return new Offer
            {
                Id = dbOffer.Id,
                Name = dbOffer.Name,
                Description = dbOffer.Description,
                Categories = dbOffer.Categories,
                GuideId = dbOffer.GuideId,
                IncludedItems = dbOffer.IncludedItems
            };
        }

        public async IAsyncEnumerable<Offer> All()
        {
            await foreach (var dbOffer in GetAllFromReader())
            {
                yield return Convert(dbOffer);
            }
        }

        private async IAsyncEnumerable<OfferDbItem> GetAllFromReader()
        {
            var allOffers = await Writer.GetAllAsync<OfferDbItem>();

            foreach (var offer in allOffers)
            {
                yield return offer;
            }

        }

        public async Task<Offer> Store(Offer offer)
        {
            return Convert(await Writer.InsertAsync(new OfferDbItem
            {
                Name = offer.Name,
                Description = offer.Description,
                Categories = offer.Categories,
                GuideId = offer.GuideId,
                IncludedItems = offer.IncludedItems
            }));
        }

        public IAsyncEnumerable<Offer> GetSuggested(IEnumerable<Interest> interests)
        {
            throw new System.NotImplementedException();
        }

        public Task<OfferBookingResult> Book(OfferBookingRequest offer)
        {
            throw new System.NotImplementedException();
        }
    }
}
