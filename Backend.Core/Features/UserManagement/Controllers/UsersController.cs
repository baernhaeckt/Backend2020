using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Core.Entities;
using Backend.Core.Features.UserManagement.Requests;
using Backend.Core.Features.UserManagement.Responses;
using Backend.Infrastructure.Abstraction.Persistence;
using Backend.Infrastructure.Abstraction.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Core.Features.UserManagement.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISecurityTokenFactory _securityTokenFactory;

        private readonly IPasswordStorage _passwordStorage;

        private readonly IWriter _writer;

        public UsersController(ISecurityTokenFactory securityTokenFactory, IPasswordStorage passwordStorage, IWriter writer)
        {
            _securityTokenFactory = securityTokenFactory;
            _passwordStorage = passwordStorage;
            _writer = writer;
        }

        [HttpPost(nameof(Login))]
        [AllowAnonymous]
        public async Task<ActionResult<UserLoginResponse>> Login([FromBody] UserLoginRequest request)
        {
            var user = await _writer.FirstOrDefaultAsync<User>(u => u.Email == request.Email.ToLowerInvariant());
            if(user == null || !_passwordStorage.Match(request.Password, user.PasswordHash))
            {
                return Unauthorized();
            }

            var response = new UserLoginResponse(_securityTokenFactory.Create(user.Id, user.Email, Enumerable.Empty<string>()));
            Response.Headers.Add("Authorization", $"Bearer {response.Token}");
            return new ActionResult<UserLoginResponse>(response);
        }

        [HttpPost(nameof(Register))]
        [AllowAnonymous]
        public async Task<ActionResult<UserLoginResponse>> Register([FromBody] RegisterUserRequest request)
        {
            var roles = new List<string>{ request.AsGuide ? Roles.Guide : Roles.User };
            var user = new User { Email = request.Email, PasswordHash = _passwordStorage.Create(request.Password), Roles = roles  };
            await _writer.InsertAsync(user);

            var response = new UserLoginResponse(_securityTokenFactory.Create(user.Id, user.Email, Enumerable.Empty<string>()));
            Response.Headers.Add("Authorization", $"Bearer {response.Token}");
            return new ActionResult<UserLoginResponse>(response);
        }

    }
}