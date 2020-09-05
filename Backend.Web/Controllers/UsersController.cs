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

            var response = new UserLoginResponse
            {
                Token = _securityTokenFactory.Create(user.Id, user.Email, Enumerable.Empty<string>())
            };
            Response.Headers.Add("Authorization", $"Bearer {response.Token}");
            return new ActionResult<UserLoginResponse>(response);
        }

        [HttpPost(nameof(Register))]
        [AllowAnonymous]
        public async Task<ActionResult<UserLoginResponse>> Register([FromBody] RegisterUserRequest request)
        {
            var user = await _writer.InsertAsync(new User {Email = request.Email, PasswordHash = _passwordStorage.Create(request.Password)});

            var response = new UserLoginResponse
            {
                Token = _securityTokenFactory.Create(user.Id, user.Email, Enumerable.Empty<string>())
            };
            Response.Headers.Add("Authorization", $"Bearer {response.Token}");
            return new ActionResult<UserLoginResponse>(response);
        }
    }
}