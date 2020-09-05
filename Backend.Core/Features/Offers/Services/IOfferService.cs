using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Core.Entities;
using Backend.Core.Features.Offers.Models;

namespace Backend.Core.Features.Offers.Services
{
    public interface IOfferService
    {
        IAsyncEnumerable<OfferResponse> All();

        Task<OfferResponse> Store(OfferResponse offer);

        IAsyncEnumerable<OfferResponse> GetSuggested(IEnumerable<Interest> interests);

        Task<OfferBookingResult> Book(OfferBookingRequest offer);
    }
}