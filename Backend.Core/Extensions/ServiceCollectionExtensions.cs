using Backend.Infrastructure.Abstraction.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStartupTask<T>(this IServiceCollection services)
            where T : class, IStartupTask => services.AddTransient<IStartupTask, T>();
    }
}