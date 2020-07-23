using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using ThreeI.TelegramBot.Core;
using ThreeI.TelegramBot.Data;
using ThreeI.TelegramBot.Windows.Utilities;

namespace ThreeI.TelegramBot.Windows.Dialog
{
    public class ResetStartChecker : IMessageProcessor
    {
        protected readonly IDataRepository _repo;
        protected readonly IMessageProvidor _messageProvidor;
        protected readonly IConfiguration _config;

        public ResetStartChecker(IDataRepository repo, IMessageProvidor messageProvidor, IConfiguration config)
        {
            _repo = repo;
            _messageProvidor = messageProvidor;
            _config = config;
        }

        public bool RequestSubmitted { get; private set; } = false;
        public bool StepHit { get; private set; }
        public IReplyMarkup KeyboardStyle { get; private set; }
        public string Response { get; private set; }

        public IMessageProcessor Step(string inputMessage, DialogState dialog)
        {
            if (inputMessage == "/reset" || inputMessage == "/start")
            {
                if (dialog.IsSupportMode)
                {
                    dialog.Reset(true);
                    dialog.LastActive = DateTime.Now;
                    Response = $"Your session has been reset.\n{_messageProvidor.HelperMessage}\n\n{_messageProvidor.Block}";
                    KeyboardStyle = BotToolSet.GetBlockMarkup();
                }
                else
                {
                    dialog.Reset(false);
                    dialog.LastActive = DateTime.Now;
                }

                StepHit = true;
            }

            return this;
        }
    }
}
