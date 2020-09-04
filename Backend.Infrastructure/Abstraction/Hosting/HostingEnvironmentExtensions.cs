using System;
using Microsoft.Extensions.Hosting;

namespace Backend.Infrastructure.Abstraction.Hosting
{
    public static class HostingEnvironmentExtensions
    {
        public static bool IsIntegrationTest(this IHostEnvironment hostEnvironment)
        {
            if (hostEnvironment == null)
            {
                throw new ArgumentNullException(nameof(hostEnvironment));
            }

            return hostEnvironment.IsEnvironment(Environments.IntegrationTest);
        }

        public static bool IsIntegrationTestOrDevelopment(this IHostEnvironment hostEnvironment)
        {
            if (hostEnvironment == null)
            {
                throw new ArgumentNullException(nameof(hostEnvironment));
            }

            return hostEnvironment.IsDevelopment() || hostEnvironment.IsIntegrationTest();
        }
    }
}