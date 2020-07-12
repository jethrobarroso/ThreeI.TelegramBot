using Microsoft.Extensions.Configuration;
using System;
using Telegram.Bot.Types;
using ThreeI.TelegramBot.Core;
using ThreeI.TelegramBot.Data;

namespace ThreeI.TelegramBot.Windows.Chats
{
    public class CallbackNavigator : DialogNavigator
    {
        public CallbackNavigator(string message, IDataRepository repo, IMessageProvidor messageProvidor, IConfiguration config)
            : base(message, repo, messageProvidor, config)
        {

        }

        public override (string reponse, bool supportSubmitted) ProcessMessage(DialogState dialogState, Message message)
        {
            throw new NotImplementedException();
        }
    }
}
