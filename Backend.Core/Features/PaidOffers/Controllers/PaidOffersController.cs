using Backend.Core.Entities;
using Backend.Core.Features.Offers.Models;
using Backend.Core.Features.PaidOffers.Services;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Core.Features.PaidOffers.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class PaidOffersController : ControllerBase
    {
        private IPaidOffersService PaidOffersService { get; }

        public PaidOffersController(IPaidOffersService paidOffersService)
        {
            PaidOffersService = paidOffersService;
        }

        // GET: api/paidoffers
        [HttpGet]
        public async Task<GetPaidOffersResponse> GetAsync()
            => new GetPaidOffersResponse
            {
                PaidOffers = await PaidOffersService.All.ToListAsync()
            };

        // GET: api/paidoffers/suggest
        [HttpPost("suggest")]
        public IAsyncEnumerable<PaidOffer> Suggest(Offer offer)
        {
            return PaidOffersService.Suggest(offer);
        }
    }
}
