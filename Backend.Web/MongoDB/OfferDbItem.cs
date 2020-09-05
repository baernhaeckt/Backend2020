using Backend.Core.Entities;
using Backend.Models;
using System;
using System.Collections.Generic;

namespace Backend.Web.MongoDB
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
