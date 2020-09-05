using System;
using System.Collections.Generic;
using Backend.Core.Entities;

namespace Backend.Core.Features.Offers.Models
{
    public class OfferResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// List of Items which are included in the offer (e.g. transfortation, entry prices).
        /// </summary>
        public ICollection<OfferItem> IncludedItems { get; set; }

        public Guid GuideId { get; set; }

        public string GuideDisplayName { get; set; }

        public ICollection<string> Categories { get; set; }

        public Offer To()
        {
            return new Offer
            {
                Id = Id,
                Name = Name,
                Description = Description,
                Categories = Categories,
                GuideId = GuideId,
                IncludedItems = IncludedItems
            };
        }

        public void From(Offer record)
        {
            Id = record.Id;
            Name = record.Name;
            Description = record.Description;
            Categories = record.Categories;
            GuideId = record.GuideId;
            IncludedItems = record.IncludedItems;
        }
    }
}
