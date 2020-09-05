using Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Web.Services
{
    public class InterestsService
    {
        public async Task<Interest> GetNextInterest()
        {
            return await Task.Run(() => InterestsSystem.Init().NextInterestCheck);
        }

        public async Task<Interest> GetNextInterest(ICollection<Interest> interests)
        {
            return await Task.Run(() => InterestsSystem.Of(interests).NextInterestCheck);
        }
    }
}
