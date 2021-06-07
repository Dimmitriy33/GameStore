using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WebApp.DAL.EF;

namespace WebApp.Web.Startup.Configuration
{
    public static class AuthenticationExtensions
    {
        public static void RegisterIdentityServer(this IServiceCollection services)
        {

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
        }
    }
}
