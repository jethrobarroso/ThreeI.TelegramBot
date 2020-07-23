using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using ThreeI.TelegramBot.Data;
using ThreeI.TelegramBot.Windows.Dialog;
using ThreeI.TelegramBot.Windows.Factory;
using ThreeI.TelegramBot.Windows.Mail;
using ThreeI.TelegramBot.Windows.Reporting;

namespace ThreeI.TelegramBot.Windows
{
    public class Program
    {
        static string _localAppdataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        static string _logFileLocation = $@"{_localAppdataPath}\ThreeITelegramBot\BotTestDevLogfile.txt";

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File(_logFileLocation)
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
            Environment.SetEnvironmentVariable("LOCAL_APPDATA", _localAppdataPath);
            return Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddDbContextPool<MySqlDbContext>(options =>
                    {
                        //options.UseMySql(hostContext.Configuration.GetConnectionString("TelebotMySQL"));
                        options.UseMySql(hostContext.Configuration.GetConnectionString("TestDevSql"));
                    }

                    );
                    try
                    {
                        services.AddSingleton<DialogAggregator>();
                        services.AddSingleton<IBotManager, TelegramBotManager>();
                        services.AddSingleton<IReport, ReportExcel>();
                        services.AddSingleton<IMailer, ReportSender>();
                        services.AddSingleton<IMessageProvidor, BotMessageDialog>();
                        services.AddSingleton<IDataRepository, MySqlDataRepository>();
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
