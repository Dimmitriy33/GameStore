using Microsoft.Extensions.DependencyInjection;
using WebApp.BLL.Helpers;
using WebApp.BLL.Interfaces;
using WebApp.BLL.Services;
using WebApp.DAL.Interfaces.Database;
using WebApp.DAL.Interfaces.Redis;
using WebApp.DAL.Redis;
using WebApp.DAL.Repository;
using WebApp.Web.Filters;
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
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IClaimsReader, ClaimsReader>();
            services.AddTransient<ITokenEncodingHelper, TokenEncodingHelper>();
            services.AddTransient<IGameSelectionHelper, GameSelectionHelper>();

            services.AddScoped<ICloudinaryService, CloudinaryService>();

            //Action filters
            services.AddScoped<GamesSelectionFilter>();

            //DAL
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductRatingRepository, ProductRatingRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();

            services.AddTransient<IRedisContext, RedisContext>();

            //AppSettings
            services.AddSingleton(appSettings);

            //Mappers
            services.AddAutoMapper(typeof(Startup));
        }
    }
}
