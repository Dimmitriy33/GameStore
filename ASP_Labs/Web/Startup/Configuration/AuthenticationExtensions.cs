using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WebApp.DAL.EF;

namespace ASP_Labs.Startup.Configuration
{
    public static class AuthenticationExtensions
    {
        public static void RegisterIdentity(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                            .AddEntityFrameworkStores<ApplicationDbContext>();
        }
    }
}
