using Serilog;
using System;
using System.Diagnostics;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace ThreeI.TelegramBot.Core
{
    public class TelegramBotManager : IBotManager
    {
        public TelegramBotManager(string apiToken)
        {
            ApiToken = apiToken;
            Bot = new TelegramBotClient(apiToken);

            InitialiseBotHandlers();
        }

        #region Properties
        public string ApiToken { get; }

        public TelegramBotClient Bot { get; }

        public bool IsReceiving { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Initialize various event handlers for the bot
        /// </summary>
        private void InitialiseBotHandlers()
        {
            Bot.OnMessage += Bot_OnMessage;
        }


        /// <summary>
        /// Registers handlers for the bot to handel regular incoming messages
        /// </summary>
        /// <param name="sender">Message sender</param>
        /// <param name="e">Event arguments containing message details</param>
        private void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Type == MessageType.Text)
            {
                Bot.SendTextMessageAsync(e.Message.Chat.Id, $"Your message: {e.Message.Text}");
                Log.Information("Echoed a message");
            }
        }

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
    }
}
