using Microsoft.Extensions.Options;

namespace Backend.Infrastructure.Persistence
{
    internal class DbContextFactory
    {
        private readonly IOptions<MongoDbOptions> _options;

        public DbContextFactory(IOptions<MongoDbOptions> options) => _options = options;

        public DbContext Create() => new DbContext(_options);
    }
}