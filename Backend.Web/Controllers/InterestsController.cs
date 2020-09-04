using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestsController : ControllerBase
    {
        // GET: api/<InterestController>
        [HttpGet]
        public Task<Interest> Get()
        {
            return Task.FromResult(new Interest
            {
                Name = "Ponies",
                Match = false 
            });
        }

        // POST api/<InterestController>
        [HttpPost]
        public Task<ICollection<Interest>> Post([FromBody] ICollection<Interest> selectedInterests)
        {
            selectedInterests.Add(new Interest
            {
                Name = "More Ponies",
                Match = false
            });

            return Task.FromResult(selectedInterests);
        }
    }
}
