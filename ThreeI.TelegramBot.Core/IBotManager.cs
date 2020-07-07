using Telegram.Bot;

namespace ThreeI.TelegramBot.Core
{
    public interface IBotManager
    {
        TelegramBotClient Bot { get; }
        bool IsReceiving { get; }

        void StartReceiving();
        void StopReceiving();
    }
}