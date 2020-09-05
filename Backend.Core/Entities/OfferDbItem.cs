using System;
using System.Collections.Generic;
using Backend.Core.Features.Offers.Models;

namespace Backend.Core.Entities
{
    public class OfferDbItem : Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// List of Items which are included in the offer (e.g. transfortation, entry prices).
        /// </summary>
        public ICollection<OfferItem> IncludedItems { get; set; }

        public Guid GuideId { get; set; }

        public ICollection<string> Categories { get; set; }
    }
}
