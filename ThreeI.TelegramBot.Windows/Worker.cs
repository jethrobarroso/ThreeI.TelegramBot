using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ThreeI.TelegramBot.Data;
using ThreeI.TelegramBot.Windows.Mail;
using ThreeI.TelegramBot.Windows.Reporting;
using Castle.Core.Internal;

namespace ThreeI.TelegramBot.Windows
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _config;
        private readonly IBotManager _bot;
        private readonly IMailer _mailer;
        private readonly IReport _report;
        private readonly IDataRepository _repo;
        private readonly string _excelPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Temp\snag_report.xlsx";

        public Worker(ILogger<Worker> logger, IConfiguration config, IBotManager bot, 
            IMailer _mailer, IReport report, IDataRepository _repo)
        {
            _logger = logger;
            _config = config;
            _bot = bot;
            this._mailer = _mailer;
            _report = report;
            this._repo = _repo;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            Log.Information("Service started");
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            Log.Information("Commense shutdown. Stopping ThreeITelegramBot service...");
            _bot.StopReceiving();
            Log.Information("All services have been terminated. Shutting down main service...");
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                Log.Information("Starting bot poller...");
                _bot.StartReceiving();
                Log.Information("Polling started. Messages being received");

                while (!stoppingToken.IsCancellationRequested)
                {
                    if (DateTime.Now.Hour == 16 && DateTime.Now.Minute == 00)
                    {
                        _report.CreateExcelReport(_repo.GetDailyReports(), _excelPath);
                        _mailer.SendReportMail(_excelPath, () => _repo.GetDailyReports().IsNullOrEmpty());
                    }
                        
                    await Task.Delay(new TimeSpan(0, 1, 0), stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                Log.Warning("Service is shutting down...");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.Message);
            }
            finally
            {
                Log.CloseAndFlush();
                _bot.StopReceiving();
            }
        }
    }
}
