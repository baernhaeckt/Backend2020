using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Core.Features.Offers.Models;
using Backend.Core.Features.Offers.Services;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Core.Features.Offers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        public IOfferService OfferService { get; }

        public OffersController(IOfferService offerService)
        {
            OfferService = offerService;
        }

        // GET: api/offers
        [AllowAnonymous]
        [HttpGet]
        public IAsyncEnumerable<Offer> Get()
        {
            return OfferService.All();
        }

        // POST api/offers/suggest
        [AllowAnonymous]
        [HttpPost("suggest")]
        public IAsyncEnumerable<Offer> Suggest([FromBody] IEnumerable<Interest> interests)
        {
            return OfferService.GetSuggested(interests);
        }

        // POST api/offers
        [HttpPost]
        public Task<Offer> Create([FromBody] Offer offer)
        {
            return OfferService.Store(offer);
        }


        // POST api/offers/book
        [HttpPost("book")]
        public Task<OfferBookingResult> Book([FromBody] OfferBookingRequest offerBookingRequest)
        {
            return OfferService.Book(offerBookingRequest);
        }
    }
}
