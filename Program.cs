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
                logger.Debug("XD");
                CreateHostBuilder(args).Build().Run();
            } catch (Exception e)
            {
                logger.Error(e, $"Exception thrown in runtime - {e.Message}");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
             .ConfigureLogging((hostingContext, logging) => {
                 logging.AddNLog(hostingContext.Configuration.GetSection("Logging"));
             });
    };
}
