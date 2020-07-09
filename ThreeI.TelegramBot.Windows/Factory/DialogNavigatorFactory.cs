using System;
using System.Collections.Generic;
using System.Text;
using ThreeI.TelegramBot.Core;
using ThreeI.TelegramBot.Data;
using ThreeI.TelegramBot.Windows.Chats;

namespace ThreeI.TelegramBot.Windows.Factory
{
    /// <summary>
    /// Factory for Telegram bot message navigators
    /// </summary>
    public static class DialogNavigatorFactory
    {
        public static DialogNavigator CreateNavigator(DialogType type, string message, IDataRepository repo, IMessageProvidor messageProvidor)
        {
            switch (type)
            {
                case DialogType.Callback:
                    return new CallbackNavigator(message, repo, messageProvidor);
                case DialogType.Text:
                default:
                    return new TextNavigator(message, repo, messageProvidor);
            }
        }
    }
}
