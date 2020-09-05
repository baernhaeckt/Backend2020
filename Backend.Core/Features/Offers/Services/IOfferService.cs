using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Core.Features.Offers.Models;
using Backend.Models;

namespace Backend.Core.Features.Offers.Services
{
    public interface IOfferService
    {
        IAsyncEnumerable<Offer> All();

        Task<Offer> Store(Offer offer);

        IAsyncEnumerable<Offer> GetSuggested(IEnumerable<Interest> interests);

        Task<OfferBookingResult> Book(OfferBookingRequest offer);
    }
}