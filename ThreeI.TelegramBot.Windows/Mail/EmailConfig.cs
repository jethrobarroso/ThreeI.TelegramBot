using System;
using System.Collections.Generic;
using System.Text;

namespace ThreeI.TelegramBot.Windows.Mail
{
    public class EmailConfig
    {
        public string To { get; set; }
        public string ToName { get; set; }
        public string From { get; set; }
        public string FromName { get; set; }
        public string Host { get; set; }
        public int OutgoingPort { get; set; }
        public string Password { get; set; }
    }
}
