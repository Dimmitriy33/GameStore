using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.BLL.Models;
using WebApp.DAL.EF;
using WebApp.DAL.Entities;
using WebApp.Web.Startup.Settings;

namespace WebApp.Web.Startup.Configuration
{
    public static class AuthenticationExtensions
    {
        public static void RegisterAuthencticationSettings(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(
                opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = appSettings.JwtSettings.ValidateIssuerSigningKey,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JwtSettings.TokenKey)),
                        ValidateAudience = appSettings.JwtSettings.ValidateAudience,
                        ValidateIssuer = appSettings.JwtSettings.ValidateIssuer
                    };
                });
        }
        public static void RegisterIdentity(this IServiceCollection services, AppSettings appSettings)
        {

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.SignIn.RequireConfirmedEmail = appSettings.IdentitySettings.SignInRequireConfirmedEmail;
                    options.Password.RequireDigit = appSettings.IdentitySettings.PasswordRequireDigit;
                    options.Password.RequireNonAlphanumeric = appSettings.IdentitySettings.PasswordRequireNonAlphanumeric;
                    options.Password.RequireUppercase = appSettings.IdentitySettings.PasswordRequireUppercase;
                    options.Password.RequireLowercase = appSettings.IdentitySettings.PasswordRequireLowercase;
                })
                    .AddRoles<ApplicationRole>()
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
            if (roles is null || !roles.Any())
            {
                throw new CustomExceptions(ServiceResultType.Internal_Server_Error, "Roles not found");
            }

            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new ApplicationRole { Name = role });
                }
            }
        }
    }
}
