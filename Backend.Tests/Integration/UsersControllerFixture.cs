using System;
using System.Net.Http;
using System.Threading.Tasks;
using Backend.Core.Features.UserManagement.Requests;
using Backend.Core.Features.UserManagement.Responses;
using Backend.Tests.Utilities;
using Xunit;

namespace Backend.Tests.Integration
{
    [Trait("Category", "Integration")]
    public class UsersControllerFixture : IClassFixture<TestContext>
    {
        private readonly TestContext _context;

        public UsersControllerFixture(TestContext context) => _context = context;

        [Fact]
        public async Task Register_Successful()
        {
            string email = "marc.sallin@outlook.com";
            var url = new Uri("api/users/Register", UriKind.Relative);
            StringContent content = new RegisterUserRequest { Email = email, Password = "test" }.ToStringContent();
            HttpResponseMessage response = await _context.AnonymousHttpClient.PostAsync(url, content);
            UserLoginResponse userLoginResponse = await response.OnSuccessDeserialize<UserLoginResponse>();
        }

        [Fact]
        public async Task SignIn_Successful()
        {
            string email = "t1@test.ch";
            string password = "test";
            var url = new Uri("api/users/Login", UriKind.Relative);
            StringContent content = new UserLoginRequest { Email = email, Password = password }.ToStringContent();
            HttpResponseMessage responseWithJwt = await _context.AnonymousHttpClient.PostAsync(url, content);
            responseWithJwt.EnsureSuccessStatusCode();
        }
    }
}