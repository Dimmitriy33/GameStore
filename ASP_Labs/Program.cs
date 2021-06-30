using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using System;
using System.Diagnostics;
using WebApp.Web.Startup;

namespace ASP_Labs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", Serilog.Events.LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .WriteTo.File(new JsonFormatter(), "Log/log.json")
                .WriteTo.File("Log/log.txt")
                .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose)
                .Enrich.FromLogContext()
                .CreateLogger();

            Serilog.Debugging.SelfLog.Enable(msg =>
            {
                Debug.Print(msg);
            });

            try
            {
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
             WebHost.CreateDefaultBuilder(args)
                 .ConfigureAppConfiguration((hostingContext, config) =>
                 {
                     config.AddEnvironmentVariables();
                 })
                 .UseStartup<Startup>()
                 .UseSerilog();
    }
}
