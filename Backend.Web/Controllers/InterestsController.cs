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

        private InterestsController(InterestsService interestsService)
        {
            this.interestsService = interestsService;
        }

        // GET: api/interests
        [HttpGet]
        public Task<Interest> Get()
        {
            return interestsService.GetNextInterest();
        }

        // POST api/interests/next
        [HttpPost("next")]
        public Task<Interest> Post([FromBody] ICollection<Interest> selectedInterests)
        {
            return interestsService.GetNextInterest(selectedInterests);
        }
    }
}
