using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApp.DAL.EF;
using WebApp.Web.Startup.Settings;

namespace WebApp.Web.Startup.Configuration
{
    public static class DatabaseExtensions
    {
        public static void RegisterDatabase(this IServiceCollection services, DbSettings dbSettings, ILoggerFactory loggerFactory)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(dbSettings.ConnectionString);
                    options.UseLoggerFactory(loggerFactory);
                }
            );
        }
    }
}
