using System.Threading.Tasks;
using Backend.Core.Entities;
using Backend.Core.Features.UserManagement.Responses;
using Backend.Infrastructure.Abstraction.Persistence;
using Backend.Infrastructure.Abstraction.Security;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Core.Features.UserManagement.Controllers
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