using AspNetCore.IServiceCollection.AddIUrlHelper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WebApp.BLL.Interfaces;
using WebApp.BLL.Services;
using WebApp.Web.Startup.Configuration;
using WebApp.Web.Startup.Settings;

namespace WebApp.Web.Startup
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder().AddConfiguration(configuration);
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettings = ReadAppSettings(Configuration);

            services.AddControllers();
            services.AddSwagger();
            services.RegisterDatabase(appSettings.DbSettings);

            services.AddAuthentication();
            services.AddCors();

            services.AddUrlHelper();

            services.RegisterIdentity();
            services.RegisterIdentityServer();

            services.Configure<PasswordHasherOptions>(options =>
    options.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV2
);
            services.AddSingleton(appSettings);
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEmailService, EmailService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RoleManager<IdentityRole> roleManager, IServiceProvider serviceProvider)
        {

            var appSettings = ReadAppSettings(Configuration);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

                #region Swagger
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ASP_Labs v1"));
                #endregion
            }

            app.UseIdentityServer();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            AuthenticationExtensions.SeedRoles(serviceProvider, appSettings.IdentitySettings.Roles).Wait();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static AppSettings ReadAppSettings(IConfiguration configuration)
        {
            var dbSettings = configuration.GetSection(nameof(AppSettings.DbSettings)).Get<DbSettings>();
            var identitySettings = configuration.GetSection(nameof(AppSettings.IdentitySettings)).Get<IdentitySettings>();
            var emailSettings = configuration.GetSection(nameof(AppSettings.EmailSettings)).Get<EmailSettings>();

            return new AppSettings
            {
                DbSettings = dbSettings,
                IdentitySettings = identitySettings,
                EmailSettings = emailSettings
            };
        }

    }

}
