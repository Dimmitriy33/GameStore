using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.BLL.Constants;
using WebApp.BLL.Models;
using WebApp.DAL;
using WebApp.DAL.Entities;
using WebApp.Web.Startup.Settings;

namespace WebApp.Web.Startup.Configuration
{
    public static class AuthenticationExtensions
    {
        public static void RegisterAuthenticationSettings(this IServiceCollection services, AppSettings appSettings)
            => services.AddAuthentication(cfg =>
                {
                    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    cfg.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(
                    opt =>
                    {
                        opt.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = appSettings.JwtSettings.ValidateIssuerSigningKey,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JwtSettings.TokenKey)),
                            ValidateAudience = appSettings.JwtSettings.ValidateAudience,
                            ValidateIssuer = appSettings.JwtSettings.ValidateIssuer,
                            ValidIssuer = appSettings.JwtSettings.Issuer,
                            ValidAudience = appSettings.JwtSettings.Audience
                        };
                    });

        public static void RegisterIdentity(this IServiceCollection services, AppSettings appSettings)
            => services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedEmail = appSettings.IdentitySettings.SignInRequireConfirmedEmail;
                    options.Password.RequireDigit = appSettings.IdentitySettings.PasswordRequireDigit;
                    options.Password.RequireNonAlphanumeric = appSettings.IdentitySettings.PasswordRequireNonAlphanumeric;
                    options.Password.RequireUppercase = appSettings.IdentitySettings.PasswordRequireUppercase;
                    options.Password.RequireLowercase = appSettings.IdentitySettings.PasswordRequireLowercase;
                })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

        public static async Task SeedRoles(IServiceProvider serviceProvider, ICollection<string> roles)
        {
            if (roles is null || !roles.Any())
            {
                throw new CustomExceptions(ServiceResultType.InternalServerError, "Roles not found");
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

        public static async Task SeedAdmin(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var user = new ApplicationUser
            {
                Email = "Admin123@gmail.com",
                UserName = "Admin123",
                AddressDelivery = "TestAddressDelivery, 12",
                PhoneNumber = "+375291111111",
                EmailConfirmed = true
            };

            var tryRegister = await userManager.CreateAsync(user, "Admin123");
            //if admin already exist
            if (!tryRegister.Succeeded)
            {
                return;
            }
            await userManager.AddToRoleAsync(user, RolesConstants.Admin);
        }

        // Just for test
        public static async Task SeedProducts(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var testProducts = new List<Product>();
            try
            {
                //docker
                using (StreamReader r = new StreamReader(Directory.GetCurrentDirectory() + "/testProducts.json"))
                //windows
                /* using (StreamReader r = new StreamReader(Directory.GetParent(Directory.GetCurrentDirectory()) + "\\testProducts.json"))*/
                {
                    string json = r.ReadToEnd();
                    testProducts = JsonConvert.DeserializeObject<List<Product>>(json);
                }
                context.Products.AddRange(testProducts);
                await context.SaveChangesAsync();
            }
            // to handle cases when data already exist
            catch (Exception e)
            {
                Console.WriteLine(Directory.GetCurrentDirectory() + $"\r\nErr: {e.Message}");
            }
        }
    }
}
