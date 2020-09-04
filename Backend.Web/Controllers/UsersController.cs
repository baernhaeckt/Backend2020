using System;
using System.Linq;
using System.Threading.Tasks;
using Backend.Core.Entities;
using Backend.Infrastructure.Abstraction.Persistence;
using Backend.Infrastructure.Abstraction.Security;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Web.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISecurityTokenFactory _securityTokenFactory;

        private readonly IWriter _writer;

        public UsersController(ISecurityTokenFactory securityTokenFactory, IWriter writer)
        {
            _securityTokenFactory = securityTokenFactory;
            _writer = writer;
        }

        [HttpPost(nameof(Login))]
        [AllowAnonymous]
        public ActionResult<UserLoginResponse> Login([FromBody] UserLoginRequest request)
        {
            var response = new UserLoginResponse
            {
                Token = _securityTokenFactory.Create(Guid.NewGuid(), "DummyUser", Enumerable.Empty<string>())
            };
            return new ActionResult<UserLoginResponse>(response);
        }

        [HttpPost(nameof(CreateDummy))]
        [AllowAnonymous]
        public async Task<IActionResult> CreateDummy()
        {
            await _writer.InsertAsync(new User
            {
                DisplayName = "Test",
                Email = "test@test.ch",
            });

            return Ok();
        }
    }
}