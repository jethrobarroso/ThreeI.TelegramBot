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

        public IReplyMarkup KeyboardStyle { get; private set; }

        public IMessageProcessor Step(string inputMessage, DialogState dialog)
        {
            if (!dialog.IsSupportMode && inputMessage == "/support")
            {
                dialog.LastActive = DateTime.Now;
                dialog.IsSupportMode = true;
                Response = BotToolSet.BuildResponseMessage(dialog,
                    ConfigHelper.GetBlockListInText(_config, "BlockNumbers") +
                    _messageProvidor.Block + Environment.NewLine,
                    _messageProvidor.HelperMessage);

                Response = $"{_messageProvidor.HelperMessage}\n\n" +
                    $"{_messageProvidor.Block}";
                StepHit = true;
                KeyboardStyle = BotToolSet.GetBlockMarkup();
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
