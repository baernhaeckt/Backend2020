using System;
using System.Collections.Generic;
using Backend.Core.Entities;
using Backend.Core.Features.Offers.Models;
using Backend.Infrastructure.Abstraction.Persistence;

namespace Backend.Core.Features.PaidOffers.Services
{
    public class PersistentPaidOffersService : IPaidOffersService
    {
        public PersistentPaidOffersService(IReader reader)
        {
            Reader = reader;
        }

        public IReader Reader { get; }

        public IAsyncEnumerable<PaidOffer> All => GetAllFromReader();

        public IAsyncEnumerable<PaidOffer> Suggest(Offer selectedOffer)
        {
            throw new NotImplementedException();
        }

        private async IAsyncEnumerable<PaidOffer> GetAllFromReader()
        {
            var allOffers = await Reader.GetAllAsync<PaidOffer>();

            foreach (var offer in allOffers)
            {
                yield return offer;
            }
        }
    }
}