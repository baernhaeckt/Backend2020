using Backend.Core.Features.Recommendation.Services;
using System.Linq;
using Xunit;

namespace Backend.Tests.Integration
{
    public class WebRecommendationFixture
    {

        WebRecommendationService sut = new WebRecommendationService();

        [Fact]
        void ShouldConsumeSuggestionsFromWebService()
        {

            var results = sut.GetOfferRecommendation(new[] { "Stadt" });
            results.Wait();

            Assert.NotNull(results.Result);
            Assert.True(results.Result.Count() > 0);
        }
    }
}
