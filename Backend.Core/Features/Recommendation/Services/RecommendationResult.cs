using System;

namespace Backend.Core.Features.Recommendation.Services
{
    public class RecommendationResult
    {
        public Guid OfferId { get; set; }

        public double Rating { get; set; }
    }
}
