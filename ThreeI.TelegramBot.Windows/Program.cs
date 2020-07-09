using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using ThreeI.TelegramBot.Core;
using ThreeI.TelegramBot.Data;
using ThreeI.TelegramBot.Windows.Factory;

namespace ThreeI.TelegramBot.Windows
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logFileLocation = $@"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\ThreeITelegramBot\BotLogfile.txt";

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File(logFileLocation)
                .CreateLogger();

            try
            {
                Log.Information("Starting up the service...");
                CreateHostBuilder(args).Build().Run();

                return;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, ex.Message);
                return;
            }
            finally
            {
                Log.Information("Terminating the service...");
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    try
                    {
                        services.AddSingleton<IMessageProvidor, BotMessageDialog>();
                        services.AddSingleton<IDataRepository, InMemoryData>();
                        services.AddSingleton<IBotManager, TelegramBotManager>();
                    }
                    catch (ArgumentException ex)
                    {
                        Log.Fatal(ex, "Invalid Telegram bot token");
                    }
                })
                .UseSerilog();
        }

    }
}
