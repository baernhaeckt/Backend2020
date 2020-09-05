using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Core.Entities;

namespace Backend.Core.Features.PaidOffers.Services
{
    public interface IPaidOffersService
    {
        public Task<IEnumerable<PaidOffer>> All { get; }

        public IAsyncEnumerable<PaidOffer> Suggest(Guid selectedOfferId);
    }
}
