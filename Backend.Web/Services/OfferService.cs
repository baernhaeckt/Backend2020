using Backend.Infrastructure.Abstraction.Persistence;
using Backend.Models;
using Backend.Web.MongoDB;
using System.Collections.Generic;

namespace Backend.Web.Services
{
    public class OfferService
    {
        private IWriter Writer { get; }

        public OfferService(IWriter writer)
        {
            Writer = writer;
        }

        public async IAsyncEnumerable<Offer> All()
        {
            await foreach(var dbOffer in GetAll())
            {
                yield return new Offer
                {
                    Id = dbOffer.Id,
                    Name = dbOffer.Name,
                    Description = dbOffer.Description,
                    Categories = dbOffer.Categories,
                    GuideId = dbOffer.GuideId,
                    IncludedItems = dbOffer.IncludedItems
                };        
            }
        }

        private async IAsyncEnumerable<OfferDbItem> GetAll()
        {
            var allOffers = await Writer.GetAllAsync<OfferDbItem>();

            foreach (var offer in allOffers)
            {
                yield return offer;
            }

        }
    }
}
