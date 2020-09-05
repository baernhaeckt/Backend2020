using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Core.Features.Offers.Controllers;
using Backend.Models;
using Backend.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Web.Controllers
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
        public async Task<GetAllOffersResponse> Get() 
            => new GetAllOffersResponse
        {
            Offers = await OfferService.All().ToListAsync()
        };


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
