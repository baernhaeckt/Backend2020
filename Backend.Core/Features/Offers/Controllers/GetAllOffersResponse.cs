using Backend.Models;
using System.Collections.Generic;

namespace Backend.Core.Features.Offers.Controllers
{
    public class GetAllOffersResponse
    {
        public IEnumerable<Offer> Offers { get; set; }
    }
}
