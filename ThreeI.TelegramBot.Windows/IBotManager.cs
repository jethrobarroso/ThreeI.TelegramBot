using Telegram.Bot;

namespace ThreeI.TelegramBot.Windows
{
    public interface IBotManager
    {
        TelegramBotClient Bot { get; }
        bool IsReceiving { get; }
        void StartReceiving();
        void StopReceiving();
    }
}