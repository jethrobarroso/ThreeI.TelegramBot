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
    public class ConfirmPhase : IMessageProcessor
    {
        protected readonly IDataRepository _repo;
        protected readonly IMessageProvidor _messageProvidor;
        protected readonly IConfiguration _config;
        private readonly Message _message;

        public ConfirmPhase(IDataRepository repo, IMessageProvidor messageProvidor, IConfiguration config, Message message)
        {
            _repo = repo;
            _messageProvidor = messageProvidor;
            _config = config;
            _message = message;
        }

        public bool RequestSubmitted { get; private set; } = false;
        public bool StepHit { get; private set; }
        public IReplyMarkup KeyboardStyle { get; private set; }
        public string Response { get; private set; }

        public IMessageProcessor Step(string inputMessage, DialogState dialog)
        {
            if (dialog.ChatPhase == 5)
            {
                if (int.TryParse(inputMessage, out int confirmOption)
                        && confirmOption >= 1 && confirmOption <= 2)
                {
                    dialog.Confirmation = confirmOption;

                    if (confirmOption == 1)
                    {
                        Response = $"{_messageProvidor.Final}\n\n{_messageProvidor.InitialMessage}";
                        var report = BotToolSet.ExtractReportData(dialog);
                        _repo.AddReport(report);
                        RequestSubmitted = true;
                    }
                    else
                    {
                        dialog.Reset(true);
                        Response = "Your session has been reset.\n\n" + _messageProvidor.Block +
                            ConfigHelper.GetBlockListInText(_config, "BlockNumbers");
                    }
                }
                else
                    Response = BotToolSet.BuildResponseMessage(dialog, $"Invalid input.\n{_messageProvidor.Confirm}", _messageProvidor.HelperMessage);

                StepHit = true;
            }

            return this;
        }
    }
}
