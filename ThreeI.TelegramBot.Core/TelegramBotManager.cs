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
        private readonly string _apiToken;
        private readonly TelegramBotClient Bot;

        public TelegramBotManager(string apiToken)
        {
            _apiToken = apiToken;
            Bot = new TelegramBotClient(apiToken);

            InitialiseBotHandlers();
        }

        private void InitialiseBotHandlers()
        {
            Bot.OnMessage += Bot_OnMessage;
        }

        private void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Type == MessageType.Text)
            {
                Bot.SendTextMessageAsync(e.Message.Chat.Id, $"Your message: {e.Message.Text}");
                Log.Information("Echoed a message");
            }

        }

        public void StartReceiving() => Bot.StartReceiving();

        public void StopReceiving() => Bot.StopReceiving();
    }
}
