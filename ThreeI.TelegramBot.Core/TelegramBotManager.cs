using System;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace ThreeI.TelegramBot.Core
{
    public class TelegramBotManager
    {
        private readonly string _apiToken;
        private readonly TelegramBotClient Bot;

        public TelegramBotManager(string apiToken)
        {
            _apiToken = apiToken;
            Bot = new TelegramBotClient(apiToken);
        }

        public void StartBot()
        {
            
        }

        private void InitialiseBot()
        {
            Bot.OnMessage += Bot_OnMessage;
        }

        private void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            
        }
    }
}
