namespace ThreeI.TelegramBot.Core
{
    public interface IBotManager
    {
        void StartReceiving();
        void StopReceiving();
    }
}