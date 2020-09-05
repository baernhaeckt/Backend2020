using Backend.Core.Entities;
using Backend.Infrastructure.Abstraction.Hosting;
using Backend.Infrastructure.Abstraction.Persistence;
using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Core.Features.Offers.PaidOffers
{
    class PaidOffersStartupTask : IStartupTask
    {
        private const string FILE_PATH = "Features.PaidOffers.Resources.data.json";

        public IWriter Writer { get; }

        public PaidOffersStartupTask(IWriter writer)
        {
            Writer = writer;
        }

        private Stream FileContentStream
         => new EmbeddedFileProvider(Assembly.GetExecutingAssembly())
                .GetFileInfo(FILE_PATH)
                .CreateReadStream();

        private async Task<IEnumerable<PaidOffer>> GetOffersFromFile()
        {
            using (var stream = FileContentStream)
            {
                return await JsonSerializer.DeserializeAsync<IEnumerable<PaidOffer>>(stream);
            }
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            if (await Writer.CountAsync<PaidOffer>() > 1)
            {
                return;
            }

            await Writer.InsertManyAsync(await GetOffersFromFile());
        }
    }
}
