using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
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
        public string Response { get; private set; }

        public IMessageProcessor Step(string inputMessage, DialogState dialog)
        {
            if (inputMessage == "/reset" || inputMessage == "/start")
            {
                if (dialog.IsSupportMode)
                {
                    dialog.Reset(true);
                    dialog.LastActive = DateTime.Now;
                    Response = BotToolSet.BuildResponseMessage(dialog, $"Your session has been reset.\n\n{_messageProvidor.Block}\n" +
                        ConfigHelper.GetBlockListInText(_config, "BlockNumbers"), _messageProvidor.SupportFooter);
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
