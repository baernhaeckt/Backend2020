using Backend.Infrastructure.Security;
using Backend.Web.Diagnostics;
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
            services.AddHealthChecks();

            // Infrastructure
            services.AddInfrastructureSecurity(_hostEnvironment);
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{Metadata.ApplicationName} API V1"); });

            app.UseCors(x =>
                x.AllowAnyMethod()
                    .WithOrigins("http://localhost:8080", "http://localhost:5000", "https://baernhaeckt2020.z19.web.core.windows.net/")
                    .AllowAnyHeader()
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