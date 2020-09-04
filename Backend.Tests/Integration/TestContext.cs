using System.Net.Http;
using Backend.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Environments = Backend.Infrastructure.Abstraction.Hosting.Environments;

namespace Backend.Tests.Integration
{
    public class TestContext : WebApplicationFactory<Startup>
    {
        public TestContext()
        {
            NewTestUserHttpClient = CreateClient();
            AnonymousHttpClient = CreateClient();
        }

        public HttpClient NewTestUserHttpClient { get; }

        public HttpClient AnonymousHttpClient { get; }

        public HttpClient CreateNewHttpClient() => CreateClient();

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment(Environments.IntegrationTest);
            return base.CreateHost(builder);
        }
    }
}