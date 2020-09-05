using System.Collections.Generic;
using Backend.Core.Entities;
using Backend.Core.Features.Offers.Models;

namespace Backend.Core.Features.PaidOffers.Services
{
    public interface IPaidOffersService
    {
        public IAsyncEnumerable<PaidOffer> All { get; }

        public IAsyncEnumerable<PaidOffer> Suggest(Offer selectedOffer);
    }
}
