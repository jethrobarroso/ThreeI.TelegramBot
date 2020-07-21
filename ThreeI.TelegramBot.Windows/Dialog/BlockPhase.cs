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
    public class BlockPhase : IMessageProcessor
    {
        protected readonly IDataRepository _repo;
        protected readonly IMessageProvidor _messageProvidor;
        protected readonly IConfiguration _config;

        public BlockPhase(IDataRepository repo, IMessageProvidor messageProvidor, IConfiguration config)
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
            if(dialog.ChatPhase == 1)
            {
                if (ConfigHelper.GetBlockCollection(_config, "BlockNumbers").Contains(inputMessage))
                {
                    dialog.Block = inputMessage;
                    dialog.ChatPhase = 2;
                    Response = BotToolSet.BuildResponseMessage(dialog,
                        _messageProvidor.Unit, _messageProvidor.SupportFooter);
                }
                else
                {
                    var invalidMsg = $"Invalid block number.\n{_messageProvidor.Block}\n" +
                        ConfigHelper.GetBlockListInText(_config, "BlockNumbers");

                    Response = BotToolSet.BuildResponseMessage(dialog, invalidMsg, _messageProvidor.SupportFooter);
                }

                StepHit = true;
            }

            return this;
        }
    }
}
