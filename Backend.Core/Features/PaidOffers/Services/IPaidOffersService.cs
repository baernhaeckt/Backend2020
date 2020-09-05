using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Core.Entities;
using Backend.Core.Features.Offers.Models;

namespace Backend.Core.Features.PaidOffers.Services
{
    public interface IPaidOffersService
    {
        public Task<IEnumerable<PaidOffer>> All { get; }

        public IAsyncEnumerable<PaidOffer> Suggest(OfferResponse selectedOffer);
    }
}
