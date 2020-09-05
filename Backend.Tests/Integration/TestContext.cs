using System.Net.Http;
using Backend.Infrastructure.Abstraction.Persistence;
using Backend.Tests.Utilities;
using Backend.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Environments = Backend.Infrastructure.Abstraction.Hosting.Environments;

namespace Backend.Tests.Integration
{
    public class TestContext : WebApplicationFactory<Startup>
    {
        public TestContext()
        {
            NewTestUserHttpClient = CreateClient();
            GuideUserHttpClient = CreateClient();
            AnonymousHttpClient = CreateClient();
        }

        public HttpClient NewTestUserHttpClient { get; }

        public HttpClient GuideUserHttpClient { get; }

        public HttpClient AnonymousHttpClient { get; }

        public string NewTestUser { get; set; }

        public HttpClient CreateNewHttpClient() => CreateClient();


        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment(Environments.IntegrationTest);

            builder.ConfigureServices(s =>
            {

                IWriter writer = new InMemoryWriter();
                s.AddSingleton<IReader>(writer);
                s.AddSingleton(writer);
            });

            return base.CreateHost(builder);
        }
    }
}