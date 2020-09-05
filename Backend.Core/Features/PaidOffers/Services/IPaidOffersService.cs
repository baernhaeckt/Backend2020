using Backend.Core.Entities;
using Backend.Models;
using System.Collections.Generic;

namespace Backend.Core.Features.PaidOffers.Services
{
    public interface IPaidOffersService
    {

        public IAsyncEnumerable<PaidOffer> All { get; }

        public IAsyncEnumerable<PaidOffer> Suggest(Offer selectedOffer);
    }
}
