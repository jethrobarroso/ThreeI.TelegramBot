using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using ThreeI.TelegramBot.Core;

namespace ThreeI.TelegramBot.Windows.Utilities
{
    /// <summary>
    /// Useful tools for processing telegram bot data
    /// </summary>
    public static class DataToolSet
    {
        /// <summary>
        /// Populates a <FaultReport cref="FaultReport"/> object from a <DialogState cref="DialogState"/> instance.
        /// </summary>
        /// <param name="state">The state of the dialog</param>
        /// <param name="message">The message object received from the <MessageEventArgs cref="MessageEventArgs"/>s</param>
        /// <returns></returns>
        public static FaultReport ExtractReportData(DialogState state, Message message)
        {
            var report = new FaultReport()
            {
                Unit = state.Unit,
                Block = state.Block,
                Category = state.Category,
                DateLogged = DateTime.Now,
                Description = state.Description,
                DialogState = state,
                FirstName = message.From.FirstName,
                LastName = message.From.LastName
            };

            return report;
        }
    }
}
