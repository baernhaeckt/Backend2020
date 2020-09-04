using Microsoft.Extensions.Options;
using MongoDbGenericRepository;

namespace Backend.Infrastructure.Persistence
{
    internal class DbContext : MongoDbContext
    {
        public DbContext(IOptions<MongoDbOptions> mongoDbOptions)
            : base(mongoDbOptions.Value.ConnectionString, mongoDbOptions.Value.Database)
        {
        }
    }
}