using System;
using System.Net.Http;
using System.Threading.Tasks;
using Backend.Core.Features.Vouchers.Models;
using Backend.Tests.Utilities;
using Xunit;

namespace Backend.Tests.Integration
{
    [Trait("Category", "Integration")]
    public class VouchersControllerFixture : IClassFixture<TestContext>
    {
        private readonly TestContext _context;

        public VouchersControllerFixture(TestContext context) => _context = context;

        [Fact]
        public async Task GetGuides_Successful()
        {
            _context.NewTestUser = await _context.NewTestUserHttpClient.CreateUserAndSignIn();

            Guid offerId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            var uri = new Uri($"api/vouchers?offerId={offerId}", UriKind.Relative);
            HttpResponseMessage response = await _context.NewTestUserHttpClient.GetAsync(uri);
            var result = await response.OnSuccessDeserialize<VoucherResponse>();

            Assert.True(result.Offer.Id.Equals(offerId));
        }
    }
}
