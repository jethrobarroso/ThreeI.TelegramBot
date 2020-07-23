using DocumentFormat.OpenXml.Math;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using ThreeI.TelegramBot.Data;
using ThreeI.TelegramBot.Windows.Dialog;
using ThreeI.TelegramBot.Windows.Factory;
using ThreeI.TelegramBot.Windows.Mail;
using ThreeI.TelegramBot.Windows.Reporting;

namespace ThreeI.TelegramBot.Windows
{
    public class TelegramBotManager : IBotManager
    {
        private readonly IConfiguration _config;
        private readonly IDataRepository _repo;
        private readonly IMessageProvidor _messageProvidor;
        private readonly IServiceScopeFactory _scopeFactory;

        public TelegramBotManager(IConfiguration config, IDataRepository repo, IMessageProvidor messageProvidor, IServiceScopeFactory scopeFactory)
        {
            _config = config;
            _repo = repo;
            _messageProvidor = messageProvidor;
            _scopeFactory = scopeFactory;
            ApiToken = _config["TelegramToken"];
            Bot = new TelegramBotClient(ApiToken);

            InitialiseBotHandlers();
        }

        #region Properties
        public string ApiToken { get; }

        public TelegramBotClient Bot { get; }

        public bool IsReceiving { get; private set; }
        #endregion

        #region Methods
        public void StartReceiving()
        {
            Bot.StartReceiving();
            IsReceiving = true;
        }

        public void StopReceiving()
        {
            Bot.StopReceiving();
            IsReceiving = false;
        }
        #endregion

        #region Callback Methods
        private void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            try
            {
                if (e.Message.Type == MessageType.Text)
                {
                    string firstName = e.Message.From.FirstName;
                    string lastName = e.Message.From.LastName;
                    var userId = e.Message.From.Id;
                    using var scope = _scopeFactory.CreateScope();
                    var aggregator = scope.ServiceProvider.GetRequiredService<DialogAggregator>();
                    var processorFactory = new MessageProcessorFactory(_repo, _messageProvidor, _config);
                    processorFactory.Message = e.Message;
                    var dialog = aggregator.ValidateUserFromMessage(userId.ToString(), e.Message);
                    var result = aggregator.GetResult(e.Message.Text, dialog, processorFactory.CreateProcessors());

                    Bot.SendTextMessageAsync(e.Message.Chat.Id, result.Response, ParseMode.Html, replyMarkup: result.KeyboardStyle);
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.Message);
            }
        }

        private void Bot_OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            try
            {
                string firstName = e.CallbackQuery.From.FirstName;
                string lastName = e.CallbackQuery.From.LastName;
                var userId = e.CallbackQuery.From.Id;
                using var scope = _scopeFactory.CreateScope();
                var aggregator = scope.ServiceProvider.GetRequiredService<DialogAggregator>();
                var processorFactory = new MessageProcessorFactory(_repo, _messageProvidor, _config)
                {
                    CallbackData = e.CallbackQuery.Data,
                    Message = e.CallbackQuery.Message
                };
                var dialog = aggregator.ValidateUserFromCallback(userId.ToString(), e.CallbackQuery);
                var result = aggregator.GetResult(e.CallbackQuery.Data, dialog, processorFactory.CreateProcessors());

                Bot.AnswerCallbackQueryAsync(e.CallbackQuery.Id);
                Bot.SendTextMessageAsync(e.CallbackQuery.From.Id, result.Response, ParseMode.Html, replyMarkup: result.KeyboardStyle);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.Message);
            }
        }
        #endregion

        #region Initialiser Methods
        /// <summary>
        /// Initialize various event handlers for the bot
        /// </summary>
        protected virtual void InitialiseBotHandlers()
        {
            Bot.OnMessage += Bot_OnMessage;
            Bot.OnCallbackQuery += Bot_OnCallbackQuery;
            
        }
        #endregion
    }
}
