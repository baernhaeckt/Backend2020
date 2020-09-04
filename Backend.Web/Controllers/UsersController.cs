using System;
using System.Linq;
using System.Threading.Tasks;
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

        public UsersController(ISecurityTokenFactory securityTokenFactory) 
            => _securityTokenFactory = securityTokenFactory;

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
    }
}