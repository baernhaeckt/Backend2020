using Backend.Core.Features.Guiding;
using Backend.Core.Features.Offers;
using Backend.Core.Features.PaidOffers;
using Backend.Core.Features.UserManagement;
using Backend.Infrastructure.Hosting;
using Backend.Infrastructure.Persistence;
using Backend.Infrastructure.Security;
using Backend.Web.Diagnostics;
using Backend.Web.Middleware;
using Backend.Web.Services;
using Backend.Web.Setup;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Backend.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        private readonly IHostEnvironment _hostEnvironment;

        public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApiDocumentation();
            services.AddMvcWithCors();
            services.AddJwtAuthentication();
            var healthChecksBuilder = services.AddHealthChecks();

            services.AddTransient<InterestsService>();

            // Infrastructure
            services.AddInfrastructurePersistence(_configuration, healthChecksBuilder);
            services.AddInfrastructureSecurity(_hostEnvironment);
            services.AddInfrastructureHosting();
            services.AddHostedService<StartupTaskRunner>();

            // Features
            services.AddFeatureUserManagement();
            services.AddFeatureGuiding();
            services.AddFeatureOffers();
            services.AddFeaturePaidOffers();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{Metadata.ApplicationName} API V1"); });

            app.UseCors(x =>
                x.AllowAnyMethod()
                    .WithOrigins("http://localhost:8080", "http://localhost:5000", "https://baernhaeckt2020.z19.web.core.windows.net/")
                    .AllowAnyHeader()
                    .WithExposedHeaders("Authorization")
                    .AllowCredentials());

            app.UseRouting();

            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = HealthCheckJsonResponseWriter.WriteHealthCheckJsonResponse
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();
            });
        }
    }
}