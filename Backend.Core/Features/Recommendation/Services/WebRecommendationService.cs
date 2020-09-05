using Backend.Core.Features.Offers.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Backend.Core.Features.Recommendation.Services
{
    public class WebRecommendationService : IRecommendationService
    {
        private static string _baseUrl = "https://baernhaecktrecommender.azurewebsites.net";

        private HttpClient HttpClient => new HttpClient() { BaseAddress = new Uri(_baseUrl) };

        private Task<HttpResponseMessage> Query(string type, IEnumerable<string> categories)
            => HttpClient.GetAsync($"/{type}?text={ string.Join(" ", categories) }");


        private async Task<IEnumerable<RecommendationResult>> Get(string type, IEnumerable<string> categories)
        {
            Stream contentStream = await (await Query(type, categories)).Content.ReadAsStreamAsync();
            var recommendations = await JsonSerializer.DeserializeAsync<List<List<object>>>(contentStream);

            return recommendations.Select(parse);
        }

        public async Task<IEnumerable<RecommendationResult>> GetOfferRecommendation(IEnumerable<string> categories)
        {
            return await Get("offers", categories);
        }

        public async Task<IEnumerable<RecommendationResult>> GetPaidOfferRecommendation(Offer offer)
        {
            return await Get("paidOffers", offer.Categories);
        }

        private static RecommendationResult parse(IList<object> responseList)
        {
            return new RecommendationResult
            {
                OfferId = new Guid((string)responseList[0]),
                Rating = (double)responseList[1]
            };
        }
    }
}
