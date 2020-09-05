using Backend.Infrastructure.Abstraction.Hosting;
using Backend.Infrastructure.Abstraction.Persistence;
using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Backend.Core.Entities;
using Backend.Core.Features.Offers.Models;

namespace Backend.Core.Features.Offers.Data
{
    public class OffersStartupTask : IStartupTask
    {
        private const string FilePath = "Features.Offers.Resources.offers.json";

        private readonly IWriter _writer;

        public OffersStartupTask(IWriter writer) => _writer = writer;

        private static Stream FileContentStream
         => new EmbeddedFileProvider(Assembly.GetExecutingAssembly())
                .GetFileInfo(FilePath)
                .CreateReadStream();

        private async Task<IEnumerable<Offer>> GetOffersFromFile()
        {
            await using var stream = FileContentStream;
            return await JsonSerializer.DeserializeAsync<IEnumerable<Offer>>(stream);
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            if (await _writer.CountAsync<OfferDbItem>() > 1)
            {
                return;
            }

            var result = (await GetOffersFromFile()).Select(o => o.To());
            await _writer.InsertManyAsync(result);
        }
    }
}
