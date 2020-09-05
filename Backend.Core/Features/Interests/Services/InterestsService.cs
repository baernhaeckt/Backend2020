using System.Collections.Generic;
using Backend.Models;

namespace Backend.Core.Features.Interests.Services
{
    public class InterestsService
    {
        public Interest GetNextInterest() => InterestsSystem.Init().NextInterestCheck;

        public Interest GetNextInterest(ICollection<Interest> interests) => InterestsSystem.Of(interests).NextInterestCheck;
    }
}
