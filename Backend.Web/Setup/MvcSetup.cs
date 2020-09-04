using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Web.Setup
{
    public static class MvcSetup
    {
        public static void AddMvcWithCors(this IServiceCollection services)
        {
            services.AddControllers()
                .AddControllersAsServices()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddCors();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(s => s.GetService<IHttpContextAccessor>().HttpContext.User);
        }
    }
}