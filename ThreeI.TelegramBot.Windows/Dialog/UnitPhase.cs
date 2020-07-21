using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using ThreeI.TelegramBot.Core;
using ThreeI.TelegramBot.Data;
using ThreeI.TelegramBot.Windows.Utilities;

namespace ThreeI.TelegramBot.Windows.Dialog
{
    public class UnitPhase : IMessageProcessor
    {
        protected readonly IDataRepository _repo;
        protected readonly IMessageProvidor _messageProvidor;
        protected readonly IConfiguration _config;

        public UnitPhase(IDataRepository repo, IMessageProvidor messageProvidor, IConfiguration config)
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
            if (dialog.ChatPhase == 2)
            {
                dialog.Unit = inputMessage;
                Response = BotToolSet.BuildResponseMessage(dialog,
                    _messageProvidor.Category, _messageProvidor.SupportFooter);
                dialog.ChatPhase = 3;
                StepHit = true;
            }

            return this;
        }
    }
}
