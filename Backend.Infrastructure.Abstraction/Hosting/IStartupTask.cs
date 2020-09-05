using System.Threading;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Abstraction.Hosting
{
    public interface IStartupTask
    {
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}