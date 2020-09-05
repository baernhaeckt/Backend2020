using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Core.Features.Recommendation.Services
{
    public class RecommendationResult
    {
        public Guid OfferId { get; set; }

        public double Rating { get; set; }
    }
}
