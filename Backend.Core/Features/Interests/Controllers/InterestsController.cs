using System.Collections.Generic;
using Backend.Core.Features.Interests.Services;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Core.Features.Interests.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class InterestsController : ControllerBase
    {
        private readonly InterestsService _interestsService;

        public InterestsController(InterestsService interestsService) => _interestsService = interestsService;

        /// <summary>
        /// Returns an initial <see cref="Interest"/>.
        /// </summary>
        [HttpGet]
        public Interest Get() => _interestsService.GetNextInterest();

        /// <summary>
        /// Returns an <see cref="Interest"/> based on previous selected <see cref="Interest"/>.
        /// </summary>
        [HttpPost("next")]
        public Interest Post([FromBody] ICollection<Interest> selectedInterests) 
            => _interestsService.GetNextInterest(selectedInterests);
    }
}
