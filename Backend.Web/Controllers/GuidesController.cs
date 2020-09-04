using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
        public List<Guide> Get()
        {
            return new List<Guide>
            {
                new Guide
                {
                    Id = Guid.NewGuid(),
                    Name = "Hans",
                    Languages = new Collection<CultureInfo>
                    {
                        CultureInfo.GetCultureInfo("de")
                    }
                },
                new Guide
                {
                    Id = Guid.NewGuid(),
                    Name = "Hans",
                    Languages = new Collection<CultureInfo>
                    {
                        CultureInfo.GetCultureInfo("en"),
                        CultureInfo.GetCultureInfo("de")
                    }
                }
            };
        }

        // POST api/<GuidesController>
        [HttpPost]
        public Guide Post([FromBody] Guide guide)
        {
            return guide;
        }
    }
}
