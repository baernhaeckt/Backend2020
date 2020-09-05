using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestsController : ControllerBase
    {
        InterestsService interestsService;

        public InterestsController(InterestsService interestsService)
        {
            this.interestsService = interestsService;
        }

        /// <summary>
        /// Returns an initial <see cref="Interest"/>.
        /// </summary>
        [HttpGet]
        public Task<Interest> Get()
        {
            return interestsService.GetNextInterest();
        }

        /// <summary>
        /// Returns an <see cref="Interest"/> based on previous selected <see cref="Interest"/>.
        /// </summary>
        [HttpPost("next")]
        public Task<Interest> Post([FromBody] ICollection<Interest> selectedInterests)
        {
            return interestsService.GetNextInterest(selectedInterests);
        }
    }
}
