using System;
using System.Collections.Generic;
using System.Text;
using ThreeI.TelegramBot.Core;

namespace ThreeI.TelegramBot.Windows.Dialog
{
    public interface IMessageProcessor
    {
        public bool RequestSubmitted { get; }
        public bool StepHit { get; }
        public string Response { get; }
        IMessageProcessor Step(string inputMessage, DialogState dialog);
    }
}
