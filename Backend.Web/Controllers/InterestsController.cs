using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestsController : ControllerBase
    {
        // GET: api/<InterestController>
        [HttpGet]
        public Interest Get()
        {
            return new Interest
            {
                Name = "Ponies",
                Match = false 
            };
        }

        // POST api/<InterestController>
        [HttpPost]
        public Collection<Interest> Post([FromBody] Collection<Interest> selectedInterests)
        {
            selectedInterests.Add(new Interest
            {
                Name = "More Ponies",
                Match = false
            });

            return selectedInterests;
        }
    }
}
