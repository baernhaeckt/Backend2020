using System.Threading;
using System.Threading.Tasks;
using Backend.Infrastructure.Abstraction.Hosting;
using Backend.Infrastructure.Abstraction.Persistence;

namespace Backend.Core.Features.Offers.Data
{
    public class PaidOffersStartupTask : IStartupTask
    {
        private readonly IWriter _writer;

        public PaidOffersStartupTask(IWriter writer)
        {
            _writer = writer;
        }

        public Task ExecuteAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}