using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Backend.Infrastructure.Abstraction.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Backend.Web
{
    public class StartupTaskRunner : IHostedService
    {
        private readonly ILogger<StartupTaskRunner> _logger;

        private readonly IServiceProvider _serviceProvider;

        public StartupTaskRunner(IServiceProvider serviceProvider, ILogger<StartupTaskRunner> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Load all tasks from DI
            using IServiceScope scope = _serviceProvider.CreateScope();
            IEnumerable<IStartupTask> startupTasks = scope.ServiceProvider.GetServices<IStartupTask>();

            // Execute all the tasks
            foreach (IStartupTask startupTask in startupTasks)
            {
                _logger.LogInformation("Run StartupTask {startupTask}", startupTask.GetType().FullName);
                await startupTask.ExecuteAsync(cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}