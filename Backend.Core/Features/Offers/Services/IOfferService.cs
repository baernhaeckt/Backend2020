using Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Web.Services
{
    public interface IOfferService
    {
        IAsyncEnumerable<Offer> All();

        Task<Offer> Store(Offer offer);

        IAsyncEnumerable<Offer> GetSuggested(IEnumerable<Interest> interests);

        Task<OfferBookingResult> Book(OfferBookingRequest offer);
    }
}