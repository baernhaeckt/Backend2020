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
        public async Task Create_Successful()
        {
            _context.NewTestUser = await _context.NewTestUserHttpClient.CreateUserAndSignIn();

            Guid offerId = Guid.Parse("3fbcb9c2-c8c8-4270-ad12-ad4c203c5d31");
            var uri = new Uri($"api/vouchers?offerId={offerId}", UriKind.Relative);
            HttpResponseMessage response = await _context.NewTestUserHttpClient.PostAsync(uri, null);
            var result = await response.OnSuccessDeserialize<VoucherResponse>();

            Assert.True(result.Offer.Id.Equals(offerId));
        }

        [Fact]
        public async Task Retrieve_Successful()
        {
            _context.NewTestUser = await _context.NewTestUserHttpClient.CreateUserAndSignIn();

            Guid offerId = Guid.Parse("3fbcb9c2-c8c8-4270-ad12-ad4c203c5d31");
            var uri = new Uri($"api/vouchers?offerId={offerId}", UriKind.Relative);
            HttpResponseMessage response = await _context.NewTestUserHttpClient.PostAsync(uri, null);
            var result = await response.OnSuccessDeserialize<VoucherResponse>();
            
            var uriGet = new Uri($"api/vouchers?id={result.Id}", UriKind.Relative);
            HttpResponseMessage responseGet = await _context.NewTestUserHttpClient.GetAsync(uriGet);
            result = await responseGet.OnSuccessDeserialize<VoucherResponse>();
            Assert.True(result.Offer.Id.Equals(offerId));

        }
    }
}
