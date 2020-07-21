using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeI.TelegramBot.Core;
using ThreeI.TelegramBot.Data;
using ThreeI.TelegramBot.Windows.Utilities;

namespace ThreeI.TelegramBot.Windows.Dialog
{
    public class CategoryPhase : IMessageProcessor
    {
        protected readonly IDataRepository _repo;
        protected readonly IMessageProvidor _messageProvidor;
        protected readonly IConfiguration _config;

        public CategoryPhase(IDataRepository repo, IMessageProvidor messageProvidor, IConfiguration config)
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
            if (dialog.ChatPhase == 3)
            {
                int categoryValue;
                if (int.TryParse(inputMessage, out categoryValue)
                    && categoryValue > 0 && categoryValue < 7)
                {
                    dialog.ChatPhase = 4;
                    dialog.Category = _repo.GetCategoryById(categoryValue);
                    Response = BotToolSet.BuildResponseMessage(dialog,
                        _messageProvidor.Description, _messageProvidor.SupportFooter);
                }
                else
                {
                    var invalidMsg = $"Invalid category.\n{_messageProvidor.Category}";
                    Response = BotToolSet.BuildResponseMessage(dialog,
                        invalidMsg, _messageProvidor.SupportFooter);
                }

                StepHit = true;
            }

            return this;
        }
    }
}
