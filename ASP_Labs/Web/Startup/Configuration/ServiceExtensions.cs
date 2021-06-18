using Microsoft.Extensions.DependencyInjection;
using WebApp.BLL.Interfaces;
using WebApp.BLL.Services;

namespace WebApp.Web.Startup.Configuration
{
    public static class ServiceExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IJwtGenerator, JwtGenerator>();
        }
    }
}
