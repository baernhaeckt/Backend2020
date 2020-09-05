using System;
using System.Collections.Generic;
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
            var uri = new Uri($"api/vouchers/{offerId}", UriKind.Relative);
            HttpResponseMessage response = await _context.NewTestUserHttpClient.PostAsync(uri, null);
            var result = await response.OnSuccessDeserialize<VoucherResponse>();

            Assert.True(result.Offer.Id.Equals(offerId));
        }

        [Fact]
        public async Task GetAll_Successful()
        {
            _context.NewTestUser = await _context.NewTestUserHttpClient.CreateUserAndSignIn();

            Guid offerId = Guid.Parse("3fbcb9c2-c8c8-4270-ad12-ad4c203c5d31");
            var uri = new Uri($"api/vouchers/{offerId}", UriKind.Relative);
            HttpResponseMessage response = await _context.NewTestUserHttpClient.PostAsync(uri, null);
            response.EnsureSuccessStatusCode();

            Guid offerId2 = Guid.Parse("8c86f8e7-da3c-480f-ae45-061fce4c0dd1");
            uri = new Uri($"api/vouchers/{offerId}", UriKind.Relative);
            response = await _context.NewTestUserHttpClient.PostAsync(uri, null);
            response.EnsureSuccessStatusCode();

            uri = new Uri("api/vouchers", UriKind.Relative);
            response = await _context.NewTestUserHttpClient.GetAsync(uri);
            var result = await response.OnSuccessDeserialize<List<VouchersResponse>>();

            Assert.True(result.Count == 2);
        }

        [Fact]
        public async Task GetById_Successful()
        {
            _context.NewTestUser = await _context.NewTestUserHttpClient.CreateUserAndSignIn();

            Guid offerId = Guid.Parse("3fbcb9c2-c8c8-4270-ad12-ad4c203c5d31");
            var uri = new Uri($"api/vouchers/{offerId}", UriKind.Relative);
            HttpResponseMessage response = await _context.NewTestUserHttpClient.PostAsync(uri, null);
            var result = await response.OnSuccessDeserialize<VoucherResponse>();
            
            var uriGet = new Uri($"api/vouchers/{result.Id}", UriKind.Relative);
            HttpResponseMessage responseGet = await _context.NewTestUserHttpClient.GetAsync(uriGet);
            result = await responseGet.OnSuccessDeserialize<VoucherResponse>();
            Assert.True(result.Offer.Id.Equals(offerId));
        }

        [Fact]
        public async Task Use_Successful()
        {
            _context.NewTestUser = await _context.NewTestUserHttpClient.CreateUserAndSignIn();
            await _context.GuideUserHttpClient.SignIn("hans.meier@test.ch", "test");

            Guid offerId = Guid.Parse("3fbcb9c2-c8c8-4270-ad12-ad4c203c5d31");
            var uri = new Uri($"api/vouchers/{offerId}", UriKind.Relative);
            HttpResponseMessage response = await _context.NewTestUserHttpClient.PostAsync(uri, null);
            var result = await response.OnSuccessDeserialize<VoucherResponse>();
            
            var useUri = new Uri($"api/vouchers/{result.Id}", UriKind.Relative);
            response = await _context.GuideUserHttpClient.PutAsync(useUri, null);
            response.EnsureSuccessStatusCode();

            var uriGet = new Uri($"api/vouchers/{result.Id}", UriKind.Relative);
            HttpResponseMessage responseGet = await _context.NewTestUserHttpClient.GetAsync(uriGet);
            result = await responseGet.OnSuccessDeserialize<VoucherResponse>();
            Assert.True(result.IsUsed);
        }
    }
}
