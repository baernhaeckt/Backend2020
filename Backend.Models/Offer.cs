using System;
using System.Collections.ObjectModel;

namespace Backend.Models
{
    class Offer
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// List of Items which are included in the offer (e.g. transfortation, entry prices).
        /// </summary>
        public Collection<OfferItem> IncludedItems { get; set; }

        public Guid GuideId { get; set; }

        public Collection<string> Categories { get; set; }
    }
}
