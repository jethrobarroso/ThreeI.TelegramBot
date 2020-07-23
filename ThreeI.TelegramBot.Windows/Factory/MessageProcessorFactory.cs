using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types;
using ThreeI.TelegramBot.Data;
using ThreeI.TelegramBot.Windows.Dialog;
using ThreeI.TelegramBot.Windows.Mail;

namespace ThreeI.TelegramBot.Windows.Factory
{
    public class MessageProcessorFactory
    {
        private readonly IDataRepository _repo;
        private readonly IMessageProvidor _messageProvider;
        private readonly IConfiguration _config;

        public MessageProcessorFactory(IDataRepository repo,
            IMessageProvidor messageProvider, IConfiguration config)
        {
            _repo = repo;
            _messageProvider = messageProvider;
            _config = config;
        }

        public Message Message { get; set; }

        public string CallbackData { get; set; }

        public IEnumerable<IMessageProcessor> CreateProcessors()
        {
            List<IMessageProcessor> list = new List<IMessageProcessor>()
            {
                new ResetStartChecker(_repo, _messageProvider, _config),
                new SupportModeChecker(_repo, _messageProvider, _config),
                new ChatExpiredChecker(_repo, _messageProvider, _config),
                new BlockPhase(_repo, _messageProvider, _config),
                new UnitPhase(_repo, _messageProvider, _config),
                new CategoryPhase(_repo, _messageProvider, _config),
                new DescriptionPhase(_repo, _messageProvider, _config),
                new ConfirmPhase(_repo, _messageProvider, _config, new ReportSender(_config))
            };

            return list;
        }
    }
}
