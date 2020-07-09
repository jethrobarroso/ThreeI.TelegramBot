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

        public string InitialMessage => _config["BotMessages:InitialMessage"];

        public string Block => _config["BotMessages:Block"];

        public string Unit => _config["BotMessages:Unit"];

        public string Category => _config["BotMessages:Category"];

        public string Description => _config["BotMessages:Description"];

        public string Confirm => _config["BotMessages:Confirm"];

        public string Final => _config["BotMessages:Final"];

        public string BadInput => _config["BotMessages:BadInput"];

        public string SupportModeNotActive => _config["BotMessages:SupportModeNotActive"];
    }
}
