using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Web.Startup.Settings;

namespace WebApp.Web.Startup.Configuration
{
    public static class ValidationSettingsExtensions
    {
        public static void ValidateSettingParameters(this IServiceCollection services, IConfiguration configuration)
        {
            services.UseConfigurationValidation();
            services.ConfigureValidatableSetting<AppSettings>(configuration);
        }
    }
}
