using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Core.Entities;
using Backend.Core.Features.Offers.Models;
using Backend.Core.Features.Offers.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Core.Features.Offers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        private readonly IOfferService _offerService;

        public OffersController(IOfferService offerService) 
            => _offerService = offerService;

        // GET: api/offers
        [AllowAnonymous]
        [HttpGet]
        public async Task<GetAllOffersResponse> Get() 
            => new GetAllOffersResponse
        {
            Offers = await _offerService.All().ToListAsync()
        };


        // POST api/offers/suggest
        [AllowAnonymous]
        [HttpPost("suggest")]
        public IAsyncEnumerable<OfferResponse> Suggest([FromBody] IEnumerable<Interest> interests) 
            => interests.Any(i => i.Match) ? _offerService.GetSuggested(interests) : _offerService.All();

        // POST api/offers
        [HttpPost]
        public Task<OfferResponse> Create([FromBody] OfferResponse offer) 
            => _offerService.Store(offer);


        // POST api/offers/book
        [HttpPost("book")]
        public Task<OfferBookingResult> Book([FromBody] OfferBookingRequest offerBookingRequest) 
            => _offerService.Book(offerBookingRequest);
    }
}
