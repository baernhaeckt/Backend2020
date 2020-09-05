using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        // GET: api/offers
        [HttpGet]
        public Task<ICollection<Offer>> Get()
        {
            return Post(new List<Interest> ());
        }

        // POST api/offers/suggest
        [HttpPost("suggest")]
        public Task<ICollection<Offer>> Post([FromBody] ICollection<Interest> interests)
        {
            return Task.FromResult((ICollection<Offer>) new List<Offer>
                {
                    new Offer
                    {
                        Id = Guid.NewGuid(),
                        Name = "Fische im Oeschinensee",
                        IncludedItems = new List<OfferItem>
                        {
                            new OfferItem
                            {
                                Name = "BLS",
                                Price = 15-5
                            }
                        }
                    }
                });
        }

        // POST api/offers
        [HttpPost]
        public Task<Offer> create([FromBody] Offer offer)
        {
            return Task.FromResult(offer);
        }


        // POST api/offers/book
        [HttpPost("book")]
        public Task<Offer> book([FromBody] OfferBookingRequest offerBookingRequest)
        {
            return Task.FromResult(new Offer());
        }
    }
}
