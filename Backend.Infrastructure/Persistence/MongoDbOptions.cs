namespace Backend.Infrastructure.Persistence
{
    internal class MongoDbOptions
    {
        public string ConnectionString { get; set; } = string.Empty;

        public string Database { get; set; } = string.Empty;
    }
}