using Microsoft.Extensions.DependencyInjection;
using WebApp.BLL.Interfaces;
using WebApp.BLL.Services;
using WebApp.DAL.Interfaces.Database;
using WebApp.DAL.Repository;
using WebApp.Web.Startup.Settings;

namespace WebApp.Web.Startup.Configuration
{
    public static class ServiceExtensions
    {
        public static void RegisterServices(this IServiceCollection services, AppSettings appSettings)
        {
            //Services
            services.AddTransient<IJwtGenerator, JwtGenerator>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEmailService, EmailService>();

            //Repositories
            services.AddTransient<IUserRepository, UserRepository>();

            //AppSettings
            services.AddSingleton(appSettings);

            //Mappers
            services.AddAutoMapper(typeof(Startup));
        }
    }
}
