using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using ThreeI.TelegramBot.Core;

namespace ThreeI.TelegramBot.Windows.Dialog
{
    public interface IMessageProcessor
    {
        bool RequestSubmitted { get; }
        bool StepHit { get; }
        string Response { get; }
        IReplyMarkup KeyboardStyle { get; }
        IMessageProcessor Step(string inputMessage, DialogState dialog);
    }
}
