using IdentityServer4.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DAL.EF;
using WebApp.DAL.Entities;
using WebApp.Web.Startup.Settings;

namespace WebApp.Web.Startup.Configuration
{
    public static class AuthenticationExtensions
    {
        public static void RegisterIdentity(this IServiceCollection services, AppSettings appSettings)
        {

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedEmail = appSettings.IdentitySettings.SignInRequireConfirmedEmail;
                    options.Password.RequireDigit = appSettings.IdentitySettings.PasswordRequireDigit;
                    options.Password.RequireNonAlphanumeric = appSettings.IdentitySettings.PasswordRequireNonAlphanumeric;
                    options.Password.RequireUppercase = appSettings.IdentitySettings.PasswordRequireUppercase;
                    options.Password.RequireLowercase = appSettings.IdentitySettings.PasswordRequireLowercase;
                })
                    .AddRoles<IdentityRole>()
                    .AddRoleManager<RoleManager<IdentityRole>>()
                    .AddDefaultTokenProviders()
                    .AddEntityFrameworkStores<ApplicationDbContext>();
        }

        public static void RegisterIdentityServer(this IServiceCollection services)
        {
            services.AddIdentityServer().AddAspNetIdentity<ApplicationUser>()
                .AddInMemoryCaching()
                .AddClientStore<InMemoryClientStore>()
                .AddResourceStore<InMemoryResourcesStore>();
        }

        public static async Task SeedRoles(IServiceProvider serviceProvider, ICollection<string> roles)
        {
            if (roles == null || !roles.Any())
            {
                return;
            }

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                }
            }
        }
    }
}
