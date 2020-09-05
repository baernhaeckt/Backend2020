using System;
using System.Net.Http;
using System.Threading.Tasks;
using Backend.Tests.Utilities;
using Xunit;

namespace Backend.Tests.Integration
{
    [Trait("Category", "Integration")]
    public class ProfileControllerFixture : IClassFixture<TestContext>
    {
        private readonly TestContext _context;

        public ProfileControllerFixture(TestContext context) => _context = context;

        [Fact]
        public async Task GetProfile_Successful()
        {
            _context.NewTestUser = await _context.NewTestUserHttpClient.CreateUserAndSignIn();

            var uri = new Uri("api/profile", UriKind.Relative);
            HttpResponseMessage response = await _context.NewTestUserHttpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
        }
    }
}