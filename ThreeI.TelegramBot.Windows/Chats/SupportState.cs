namespace ThreeI.TelegramBot.Windows.Chats
{
    public class SupportState
    {
        public SupportState(string response, bool supportSubmitted)
        {
            Response = response;
            SupportSubmitted = supportSubmitted;
        }

        public string Response { get; }
        public bool SupportSubmitted { get; }
    }
}