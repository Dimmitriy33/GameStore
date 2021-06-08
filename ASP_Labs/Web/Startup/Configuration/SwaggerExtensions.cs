using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace WebApp.Web.Startup.Configuration
{
    public static class SwaggerExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ASP_Labs", Version = "v1" });
            });
        }
    }
}
