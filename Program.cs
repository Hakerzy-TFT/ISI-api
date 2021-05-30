using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using NLog.Extensions.Logging;
using NLog;

namespace gamespace_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                Console.WriteLine("API online ..");
                CreateHostBuilder(args).Build().Run();
            } catch (Exception e)
            {
                logger.Error(e, $"Exception thrown in runtime - {e.Message}");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();

                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls($"{config.GetSection("UrlAdress").Value}:{config.GetSection("Port").Value}");
                })
             .ConfigureLogging((hostingContext, logging) => {
                 logging.AddNLog(hostingContext.Configuration.GetSection("Logging"));
             });
    };
}
