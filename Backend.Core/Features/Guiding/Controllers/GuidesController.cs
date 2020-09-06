using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Core.Entities;
using Backend.Core.Features.Guiding.Models;
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
        public async Task<IEnumerable<GuideResponse>> Get()
        {
            var guides = await _reader.WhereAsync<User, GuideResponse>(u => u.Roles.Contains(Roles.Guide), u => new GuideResponse(u.Id, u.DisplayName, u.Description, u.Languages));
            return guides;
        }
    }
}
