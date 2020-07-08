using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using ThreeI.TelegramBot.Core;

namespace ThreeI.TelegramBot.Windows
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _config;
        private readonly IBotManager _bot;

        public Worker(ILogger<Worker> logger, IConfiguration config, IBotManager bot)
        {
            _logger = logger;
            _config = config;
            _bot = bot;
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
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    await Task.Delay(10000, stoppingToken);
                }
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
