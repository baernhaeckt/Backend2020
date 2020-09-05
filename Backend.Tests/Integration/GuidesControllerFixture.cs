using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Backend.Core.Entities;
using Backend.Tests.Utilities;
using Xunit;

namespace Backend.Tests.Integration
{
    [Trait("Category", "Integration")]
    public class GuidesControllerFixture : IClassFixture<TestContext>
    {
        private readonly TestContext _context;

        public GuidesControllerFixture(TestContext context) => _context = context;

        [Fact]
        public async Task GetGuides_Successful()
        {
            _context.NewTestUser = await _context.NewTestUserHttpClient.CreateUserAndSignIn();

            var uri = new Uri("api/guides", UriKind.Relative);
            HttpResponseMessage response = await _context.NewTestUserHttpClient.GetAsync(uri);
            var result = await response.OnSuccessDeserialize<List<User>>();
            
            Assert.NotEmpty(result);
            Assert.True(result.All(u => u.Roles.Single()== Roles.Guide));
        }
    }
}