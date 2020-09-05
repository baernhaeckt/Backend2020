using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Core.Entities;
using Backend.Core.Features.Offers.Models;
using Backend.Infrastructure.Abstraction.Persistence;

namespace Backend.Core.Features.PaidOffers.Services
{
    public class PersistentPaidOffersService : IPaidOffersService
    {
        public PersistentPaidOffersService(IReader reader) => _reader = reader;

        private readonly IReader _reader;

        public Task<IEnumerable<PaidOffer>> All => _reader.GetAllAsync<PaidOffer>();

        public IAsyncEnumerable<PaidOffer> Suggest(OfferResponse selectedOffer)
        {
            throw new NotImplementedException();
        }
    }
}