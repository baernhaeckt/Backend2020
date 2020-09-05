using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Backend.Core.Entities;
using Backend.Core.Features.Offers.Models;
using Backend.Infrastructure.Abstraction.Hosting;
using Backend.Infrastructure.Abstraction.Persistence;
using Microsoft.Extensions.FileProviders;

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

        private async Task<IEnumerable<OfferResponse>> GetOffersFromFile()
        {
            await using var stream = FileContentStream;
            return await JsonSerializer.DeserializeAsync<IEnumerable<OfferResponse>>(stream);
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            if (await _writer.CountAsync<Offer>() > 1)
            {
                return;
            }

            var result = (await GetOffersFromFile()).Select(o => o.To());
            await _writer.InsertManyAsync(result);
        }
    }
}
