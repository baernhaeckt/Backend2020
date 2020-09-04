using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuidesController : ControllerBase
    {
        // GET: api/<GuidesController>
        [HttpGet]
        public Task<ICollection<Guide>> Get()
        {
            return Task.FromResult((ICollection<Guide>) new List<Guide>
            {
                new Guide
                {
                    Id = Guid.NewGuid(),
                    Firstname = "Hans",
                    Lastname = "Meier",
                    Description = "Cooler Fischer!",
                    Languages = new Collection<CultureInfo>
                    {
                        CultureInfo.GetCultureInfo("de")
                    }
                },
                new Guide
                {
                    Id = Guid.NewGuid(),
                    Firstname = "Hans",
                    Lastname = "Müller",
                    Description = "Voll der Botscha spieler",
                    Languages = new Collection<CultureInfo>
                    {
                        CultureInfo.GetCultureInfo("en"),
                        CultureInfo.GetCultureInfo("de")
                    }
                }
            });
        }

        // POST api/<GuidesController>
        [HttpPost]
        public Task<Guide> Post([FromBody] Guide guide)
        {
            return Task.FromResult(guide);
        }
    }
}
