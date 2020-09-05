using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Backend.Core.Features.Recommendation.Services
{
    public class WebRecommendationResponse
    {
        [JsonPropertyName("result")]
        public IList<IList<JsonElement>> Result { get; set; }
    }
}
