using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;
using ThreeI.TelegramBot.Core;

namespace ThreeI.TelegramBot.Windows.Utilities
{
    public static class DataToolSet
    {
        public static FaultReport ExtractReportData(DialogState state, MessageEventArgs e)
        {
            var report = new FaultReport()
            {
                Unit = state.Unit,
                Block = state.Block,
                Category = state.Category,
                DateLogged = DateTime.Now,

            };

            return report;
        }
    }
}
