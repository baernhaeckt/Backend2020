namespace Backend.Core.Features.Offers.Models
{
    /// <summary>
    /// Item included in an Offer (e.g. Transportation, Entry Fees ...).
    /// </summary>
    public class OfferItem
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }
    }
}
