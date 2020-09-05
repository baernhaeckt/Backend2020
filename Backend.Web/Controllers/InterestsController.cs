using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Web.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class InterestsController : ControllerBase
    {
        private readonly InterestsService _interestsService;

        public InterestsController(InterestsService interestsService)
        {
            _interestsService = interestsService;
        }

        /// <summary>
        /// Returns an initial <see cref="Interest"/>.
        /// </summary>
        [HttpGet]
        public Task<Interest> Get()
        {
            return _interestsService.GetNextInterest();
        }

        /// <summary>
        /// Returns an <see cref="Interest"/> based on previous selected <see cref="Interest"/>.
        /// </summary>
        [HttpPost("next")]
        public Task<Interest> Post([FromBody] ICollection<Interest> selectedInterests)
        {
            return _interestsService.GetNextInterest(selectedInterests);
        }
    }
}
