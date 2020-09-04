using System;
using System.Net.Http;
using System.Threading.Tasks;
using Backend.Tests.Utilities;
using Xunit;

namespace Backend.Tests.Integration
{
    [Trait("Category", "Integration")]
    public class HealthCheckFixture : IClassFixture<TestContext>
    {
        private readonly TestContext _context;

        public HealthCheckFixture(TestContext context) => _context = context;

        [Fact]
        public async Task CallHealthCheck()
        {
            var uri = new Uri("/health", UriKind.Relative);
            HttpResponseMessage response = await _context.AnonymousHttpClient.GetAsync(uri);
            response.EnsureNotSuccessStatusCode();
        }
    }
}