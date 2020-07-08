using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ThreeI.TelegramBot.Windows
{
    public class BotMessageDialog : IMessageProvidor
    {
        private readonly IConfiguration _config;

        public BotMessageDialog(IConfiguration config)
        {
            _config = config;
        }

        public string MessageBlock => _config["BotMessages:MessageBlock"];

        public string MessageUnit => _config["BotMessages:MessageUnit"];

        public string MessageOption => _config["BotMessages:MessageOption"];

        public string MessageDescription => _config["BotMessages:MessageDescription"];

        public string MessageConfirm => _config["BotMessages:MessageConfirm"];

        public string MessageFinal => _config["BotMessages:MessageFinal"];
    }
}
