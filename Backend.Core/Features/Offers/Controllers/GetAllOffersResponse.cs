using System.Collections.Generic;
using Backend.Core.Features.Offers.Models;

namespace Backend.Core.Features.Offers.Controllers
{
    public class GetAllOffersResponse
    {
        public IEnumerable<Offer> Offers { get; set; }
    }
}
