using System.Threading.Tasks;
using Backend.Core;
using Backend.Core.Entities;
using Backend.Infrastructure.Abstraction.Persistence;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Web.Controllers
{
    [Route("api/profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IReader _reader;

        public ProfileController(IReader reader) => _reader = reader;

        [HttpGet]
        public async Task<UserProfileResponse> Get()
        {
            return await _reader.GetByIdOrThrowAsync<User, UserProfileResponse>(
                HttpContext.User.Id(),
                u => new UserProfileResponse(u.DisplayName, u.Email, u.Location.Latitude, u.Location.Longitude, u.Location.City, u.Location.Street, u.Location.PostalCode));
        }
    }
}