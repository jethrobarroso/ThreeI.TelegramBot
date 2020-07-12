using Microsoft.Extensions.Configuration;
using ThreeI.TelegramBot.Data;
using ThreeI.TelegramBot.Windows.Chats;

namespace ThreeI.TelegramBot.Windows.Factory
{
    /// <summary>
    /// Factory for Telegram bot message navigators
    /// </summary>
    public static class DialogNavigatorFactory
    {
        public static DialogNavigator CreateNavigator(DialogType type, string message, IDataRepository repo, IMessageProvidor messageProvidor, IConfiguration config)
        {
            switch (type)
            {
                case DialogType.Callback:
                    return new CallbackNavigator(message, repo, messageProvidor, config);
                case DialogType.Text:
                default:
                    return new TextNavigator(message, repo, messageProvidor, config);
            }
        }
    }
}
