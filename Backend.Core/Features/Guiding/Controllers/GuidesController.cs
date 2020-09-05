using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Core.Entities;
using Backend.Infrastructure.Abstraction.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Core.Features.Guiding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuidesController : ControllerBase
    {
        private readonly IReader _reader;

        public GuidesController(IReader reader) => _reader = reader;

        // GET: api/<GuidesController>
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            var guides = await _reader.WhereAsync<User>(u => u.Roles.Contains(Roles.Guide));
            return guides;
        }
    }
}
