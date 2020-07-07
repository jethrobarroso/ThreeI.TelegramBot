using System;
using System.Collections.Generic;
using System.Text;

namespace ThreeI.TelegramBot.Core
{
    public interface IMessageProvidor
    {
        string MessageBlock { get; }
        string MessageUnit { get; }
        string MessageOption { get; }
        string MessageDescription { get; }
        string MessageConfirm { get; }
        string MessageFinal { get; }
    }
}
