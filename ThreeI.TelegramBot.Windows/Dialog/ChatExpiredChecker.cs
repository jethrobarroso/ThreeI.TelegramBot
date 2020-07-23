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
    public class ChatExpiredChecker : IMessageProcessor
    {
        protected readonly IDataRepository _repo;
        protected readonly IMessageProvidor _messageProvidor;
        protected readonly IConfiguration _config;

        public ChatExpiredChecker(IDataRepository repo, IMessageProvidor messageProvidor, IConfiguration config)
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
            var timeDiff = DateTime.Now.Subtract(dialog.LastActive);

            if (timeDiff.TotalMinutes > double.Parse(_config["UserSessionExpireTime"]))
            {
                dialog.LastActive = DateTime.Now;
                Response = "The session has expired due to inactivity.\n\n" +
                    _messageProvidor.InitialMessage;
                dialog.Reset(false);
                StepHit = true;
            }

            return this;
        }
    }
}
