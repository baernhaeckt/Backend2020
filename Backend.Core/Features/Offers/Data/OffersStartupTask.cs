using Backend.Infrastructure.Abstraction.Hosting;
using Backend.Infrastructure.Abstraction.Persistence;
using Backend.Models;
using Backend.Web.MongoDB;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Core.Features.Offers.Data
{
    class OffersStartupTask : IStartupTask
    {
        private const string FILE_PATH = "Features.Offers.Resources.offers.json";

        public IWriter Writer { get; }
        public IWebHostEnvironment Env { get; }

        public OffersStartupTask(IWriter writer, IWebHostEnvironment env)
        {
            Writer = writer;
            Env = env;
        }

        private Stream FileContentStream
         => new EmbeddedFileProvider(Assembly.GetExecutingAssembly())
                .GetFileInfo(FILE_PATH)
                .CreateReadStream();

        private async Task<IEnumerable<Offer>> GetOffersFromFile()
        {
            using (var stream = FileContentStream)
            {
                return await JsonSerializer.DeserializeAsync<IEnumerable<Offer>>(stream);
            }
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
            => await Task.WhenAll((await GetOffersFromFile())
                .Select(OfferDbItem.Of)
                .Select(Writer.InsertAsync)
                .ToArray());
    }
}
