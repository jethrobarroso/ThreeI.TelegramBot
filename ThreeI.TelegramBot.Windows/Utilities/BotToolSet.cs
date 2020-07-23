using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using ThreeI.TelegramBot.Core;

namespace ThreeI.TelegramBot.Windows.Utilities
{
    /// <summary>
    /// Useful tools for processing telegram bot data.
    /// </summary>
    public static class BotToolSet
    {
        /// <summary>
        /// Populates a <see cref="FaultReport"/> object from a <see cref="DialogState"/> instance.
        /// </summary>
        /// <param name="state">The state of the dialog</param>
        /// <param name="message">The message object received from the <see cref="MessageEventArgs"/>s</param>
        /// <returns></returns>
        public static FaultReport ExtractReportData(DialogState state)
        {
            var report = new FaultReport()
            {
                Unit = state.Unit,
                Block = state.Block,
                Category = state.Category,
                DateLogged = DateTime.Now,
                Description = state.Description,
                DialogState = state,
                FirstName = state.FirstName,
                LastName = state.LastName
            };
                

            return report;
        }

        /// <summary>
        /// Generates a commonly used message in the application.
        /// </summary>
        /// <param name="dialog">The <see cref="FaultReport"/> object that
        /// the method will use to extract state information from.</param>
        /// <param name="mainMessage">The main message that will be shown at the top.</param>
        /// <param name="helpMessage">A footer message.</param>
        /// <returns></returns>
        public static string BuildResponseMessage(DialogState dialog, string mainMessage, string helpMessage)
        {
            var result =
                $"<u><b>Progress</b></u>\n" +
                $"<i>Block</i>: {dialog.Block}\n" +
                $"<i>Unit</i>: {dialog.Unit}\n" +
                $"<i>Category</i>: {((dialog.ChatPhase >= 3) ? dialog.Category.Name : string.Empty)}\n" +
                $"<i>Description</i>: {dialog.Description}\n\n" +
                $"{helpMessage}\n\n" +
                $"{mainMessage}";

            return result;
        }

        /// <summary>
        /// Create a ticket message from the <see cref="FaultReport"/> object.
        /// </summary>
        /// <param name="report">The fault that was logged</param>
        /// <returns>A string containing the complete ticket body message.</returns>
        public static string CreateTicketMessage(FaultReport report)
        {
            string message = $"<b>Beneficiary</b>: {report.FirstName} {report.LastName}<br>" +
                $"<b>Block</b>: {report.Block}<br>" +
                $"<b>Unit</b>: {report.Unit}<br>" +
                $"<b>Category</b>: {report.Category.Description}<br>" +
                $"<b>Description</b>: {report.Description}";

            return message;
        }

        public static IReplyMarkup GetBlockMarkup()
        {
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("1401", "1401"),
                    InlineKeyboardButton.WithCallbackData("1404", "1404")
                }
            });

            return inlineKeyboard;
        }

        public static IReplyMarkup GetCategoryMarkup()
        {
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
                {
                    new [] 
                    { 
                        InlineKeyboardButton.WithCallbackData("Electrical", "1"),
                        InlineKeyboardButton.WithCallbackData("Water", "2")
                    },
                    new [] 
                    {
                        InlineKeyboardButton.WithCallbackData("Painting", "3"),
                        InlineKeyboardButton.WithCallbackData("Structure", "4")
                    },
                    new [] 
                    { 
                        InlineKeyboardButton.WithCallbackData("Carpentry", "5"),
                        InlineKeyboardButton.WithCallbackData("Other", "6")
                    }
                });

            return inlineKeyboard;
        }

        public static IReplyMarkup GetConfirmMarkup()
        {
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Yes", "1"),
                    InlineKeyboardButton.WithCallbackData("No", "2")
                }
            });

            return inlineKeyboard;
        }
    }
}
