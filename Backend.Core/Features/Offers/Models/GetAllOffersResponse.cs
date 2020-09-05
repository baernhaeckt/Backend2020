using System.Collections.Generic;

namespace Backend.Core.Features.Offers.Models
{
    public class GetAllOffersResponse
    {
        public IEnumerable<OfferResponse> Offers { get; set; }
    }
}
