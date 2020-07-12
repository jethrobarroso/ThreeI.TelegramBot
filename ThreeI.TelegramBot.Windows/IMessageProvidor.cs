namespace ThreeI.TelegramBot.Windows
{
    public interface IMessageProvidor
    {
        string InitialMessage { get; }
        string Block { get; }
        string Unit { get; }
        string Category { get; }
        string Description { get; }
        string Confirm { get; }
        string Final { get; }
        string BadInput { get; }
        string SupportModeNotActive { get; }
        string SupportFooter { get; }
    }
}
