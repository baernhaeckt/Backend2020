﻿using System;
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
        private const string BaseUrl = "https://baernhaecktrecommender.azurewebsites.net";

        private static HttpClient HttpClient => new HttpClient { BaseAddress = new Uri(BaseUrl) };

        private static Task<HttpResponseMessage> Query(string type, IEnumerable<string> categories)
            => HttpClient.GetAsync($"/{type}?text={ string.Join(" ", categories) }");

        private async Task<IEnumerable<RecommendationResult>> Get(string type, IEnumerable<string> categories)
        {
            Stream contentStream = await (await Query(type, categories)).Content.ReadAsStreamAsync();
            var response = await JsonSerializer.DeserializeAsync<WebRecommendationResponse>(contentStream);

            return response.Result.Select(parse);
        }

        public async Task<IEnumerable<RecommendationResult>> GetOfferRecommendation(IEnumerable<string> categories) 
            => await Get("offers", categories);

        public async Task<IEnumerable<RecommendationResult>> GetPaidOfferRecommendation(IEnumerable<string> categories)
            => await Get("paidoffers", categories);

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
