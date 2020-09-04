using System;
using Backend.Infrastructure.Abstraction.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Infrastructure.Persistence
{
    public static class Registrar
    {
        public static IServiceCollection AddInfrastructurePersistence(this IServiceCollection services, IConfiguration configuration, IHealthChecksBuilder healthChecksBuilder)
        {
            services.Configure<MongoDbOptions>(configuration.GetSection(nameof(MongoDbOptions)));
            var mongoDbOptions = new MongoDbOptions();
            configuration.Bind(nameof(MongoDbOptions), mongoDbOptions);

            healthChecksBuilder.AddMongoDb(mongoDbOptions.ConnectionString, mongoDbOptions.Database, null, null, null, TimeSpan.FromSeconds(2));

            services.AddSingleton<DbContextFactory>();
            services.AddScoped<IWriter, MongoDbWriter>();
            services.AddSingleton<IReader, MongoDbReader>();

            return services;
        }
    }
}