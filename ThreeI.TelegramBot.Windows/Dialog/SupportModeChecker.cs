using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using ThreeI.TelegramBot.Core;
using ThreeI.TelegramBot.Data;
using ThreeI.TelegramBot.Windows.Utilities;

namespace ThreeI.TelegramBot.Windows.Dialog
{
    public class SupportModeChecker : IMessageProcessor
    {
        protected readonly IDataRepository _repo;
        protected readonly IMessageProvidor _messageProvidor;
        protected readonly IConfiguration _config;

        public SupportModeChecker(IDataRepository repo, IMessageProvidor messageProvidor, IConfiguration config)
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
            if (!dialog.IsSupportMode && inputMessage == "/support")
            {
                dialog.LastActive = DateTime.Now;
                dialog.IsSupportMode = true;
                Response = BotToolSet.BuildResponseMessage(dialog, _messageProvidor.Block +
                    Environment.NewLine + ConfigHelper.GetBlockListInText(_config, "BlockNumbers"),
                    _messageProvidor.SupportFooter);
                StepHit = true;
            }

            if (!dialog.IsSupportMode && inputMessage != "/support")
            {
                dialog.LastActive = DateTime.Now;
                Response = _messageProvidor.SupportModeNotActive;
                StepHit = true;
            }

            return this;
        }
    }
}
