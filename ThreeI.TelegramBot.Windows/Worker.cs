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
            Debug.WriteLine(_config["telegramToken"]);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                Log.Information("Starting Telegram bot...");
                _bot.StartReceiving();

                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    await Task.Delay(5000, stoppingToken);
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
