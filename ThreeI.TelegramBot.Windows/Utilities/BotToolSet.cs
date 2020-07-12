using System;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using ThreeI.TelegramBot.Core;

namespace ThreeI.TelegramBot.Windows.Utilities
{
    /// <summary>
    /// Useful tools for processing telegram bot data.
    /// </summary>
    public static class BotToolSet
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

        /// <summary>
        /// Generates a commonly used message in the application.
        /// </summary>
        /// <param name="dialog">The <DialogState cref="FaultReport"/> object that
        /// the method will use to extract state information from.</param>
        /// <param name="mainMessage">The main message that will be shown at the top.</param>
        /// <param name="footer">A footer message.</param>
        /// <returns></returns>
        public static string BuildResponseMessage(DialogState dialog, string mainMessage, string footer)
        {
                var result = $"{mainMessage}\n\n" + 
                $"<u><b>Progress</b></u>\n" +
                $"<i>Block</i>: {dialog.Block}\n" +
                $"<i>Unit</i>: {dialog.Unit}\n" +
                $"<i>Category</i>: {((dialog.ChatPhase == 3) ? dialog.Category.Name : string.Empty )}\n" +
                $"<i>Description</i>: {dialog.Description}\n\n" +
                $"{footer}";

            return result;
        }
    }
}
