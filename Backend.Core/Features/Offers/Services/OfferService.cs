using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Core.Entities;
using Backend.Core.Features.Offers.Models;
using Backend.Infrastructure.Abstraction.Persistence;
using Backend.Models;

namespace Backend.Core.Features.Offers.Services
{
    public class OfferService : IOfferService
    {
        private readonly IWriter _writer;

        public OfferService(IWriter writer) => _writer = writer;

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
