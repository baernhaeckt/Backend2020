using Backend.Core.Entities;
using Backend.Infrastructure.Abstraction.Persistence;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Core.Features.PaidOffers.Services
{
    public class PersistentPaidOffersService : IPaidOffersService
    {
        public PersistentPaidOffersService(IReader reader)
        {
            Reader = reader;
        }

        private async IAsyncEnumerable<PaidOffer> GetAllFromReader()
        {
            var allOffers = await Reader.GetAllAsync<PaidOffer>();

            foreach (var offer in allOffers)
            {
                yield return offer;
            }

        }

        public IAsyncEnumerable<PaidOffer> All => GetAllFromReader();

        public IReader Reader { get; }

        public IAsyncEnumerable<PaidOffer> Suggest(Offer selectedOffer)
        {
            throw new NotImplementedException();
        }
    }
}
