using System;
using System.Collections.Generic;
using System.Text;

namespace ThreeI.TelegramBot.Windows
{
    public interface IMessageProvidor
    {
        string Block { get; }
        string Unit { get; }
        string Option { get; }
        string Description { get; }
        string Confirm { get; }
        string Final { get; }
        string BadInput { get; }
    }
}
