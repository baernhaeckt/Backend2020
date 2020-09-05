using Backend.Core.Entities;
using Backend.Core.Features.Offers.Models;
using Backend.Core.Features.PaidOffers.Services;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

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
        public IAsyncEnumerable<PaidOffer> Get()
        {
            return PaidOffersService.All;
        }

        // GET: api/paidoffers/suggest
        [HttpPost("suggest")]
        public IAsyncEnumerable<PaidOffer> Suggest(Offer offer)
        {
            return PaidOffersService.Suggest(offer);
        }
    }
}
