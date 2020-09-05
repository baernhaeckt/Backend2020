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
            var response = await JsonSerializer.DeserializeAsync<WebRecommendationResponse>(contentStream);
            return response.Result.Select(parse);
        }

        public async Task<IEnumerable<RecommendationResult>> GetOfferRecommendation(IEnumerable<string> categories)
        {
            return await Get("offers", categories);
        }

        public async Task<IEnumerable<RecommendationResult>> GetPaidOfferRecommendation(OfferResponse offer)
        {
            return await Get("paidOffers", offer.Categories);
        }

        private static RecommendationResult parse(IList<JsonElement> responseList)
        {
            return new RecommendationResult
            {
                OfferId = new Guid(responseList[0].GetString()),
                Rating = responseList[1].GetDouble()
            };
        }
    }
}
