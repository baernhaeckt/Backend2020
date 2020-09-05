﻿using Backend.Core.Features.Offers.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Core.Features.Recommendation.Services
{
    interface IRecommendationService
    {
        public Task<IEnumerable<RecommendationResult>> GetOfferRecommendation(IEnumerable<string> categories);

        public Task<IEnumerable<RecommendationResult>> GetPaidOfferRecommendation(Offer offer);
    }
}
