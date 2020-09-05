using Backend.Core.Entities;
using System.Collections.Generic;

namespace Backend.Core.Features.PaidOffers.Controllers
{
    public class GetPaidOffersResponse
    {
        public IEnumerable<PaidOffer> PaidOffers { get; set; }
    }
}
