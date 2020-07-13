using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using Telegram.Bot.Types;
using ThreeI.TelegramBot.Core;
using ThreeI.TelegramBot.Data;
using ThreeI.TelegramBot.Windows.Utilities;

namespace ThreeI.TelegramBot.Windows.Chats
{
    public class TextNavigator : DialogNavigator
    {
        public TextNavigator(string message, IDataRepository repo, IMessageProvidor messageProvidor, IConfiguration config)
            : base(message, repo, messageProvidor, config)
        {

        }

        public override (string reponse, bool supportSubmitted) ProcessMessage(DialogState dialog, Message message)
        {
            var response = string.Empty;
            var progressInfo = string.Empty;
            var supportSubmitted = false;
            var timeDiff = DateTime.Now.Subtract(dialog.LastActive);

            // Reset the dialog if the user enters /start or /reset depending 
            // depending on the support mode
            if (_message == "/reset" || _message == "/start")
            {
                if (dialog.IsSupportMode)
                {
                    dialog.Reset(true);
                    dialog.LastActive = DateTime.Now;
                    _repo.UpdateDialogState(dialog);

                    var tempMsg = BotToolSet.BuildResponseMessage(dialog, $"Your session has been reset.\n\n{_messageProvidor.Block}\n" +
                        ConfigHelper.GetBlockListInText(_config, "BlockNumbers"), _messageProvidor.SupportFooter);

                    return (tempMsg, false);
                }
                else
                {
                    dialog.Reset(false);
                    dialog.LastActive = DateTime.Now;
                    _repo.UpdateDialogState(dialog);
                    return (_messageProvidor.InitialMessage, false);
                }
            }

            if (!dialog.IsSupportMode && _message == "/support")
            {
                dialog.LastActive = DateTime.Now;
                dialog.IsSupportMode = true;
                _repo.UpdateDialogState(dialog);
                response = BotToolSet.BuildResponseMessage(dialog, _messageProvidor.Block +
                    Environment.NewLine + ConfigHelper.GetBlockListInText(_config, "BlockNumbers"),
                    _messageProvidor.SupportFooter);
                return (response, false);
            }

            if (!dialog.IsSupportMode && _message != "/support")
            {
                dialog.LastActive = DateTime.Now;
                return (_messageProvidor.SupportModeNotActive, false);
            }
                

            if (timeDiff.TotalMinutes > double.Parse(_config["UserSessionExpireTime"]))
            {
                dialog.LastActive = DateTime.Now;
                response = "The session has expired due to inactivity.\n\n" +
                    _messageProvidor.InitialMessage;
                dialog.Reset(false);
                _repo.UpdateDialogState(dialog);
                return (response, false);
            }

            switch (dialog.ChatPhase)
            {
                // Block phase
                case 1:
                    if (ConfigHelper.GetBlockCollection(_config, "BlockNumbers").Contains(_message))
                    {
                        dialog.Block = _message;
                        dialog.ChatPhase = 2;
                        response = BotToolSet.BuildResponseMessage(dialog,
                            _messageProvidor.Unit, _messageProvidor.SupportFooter);
                    }
                    else
                    {
                        var invalidMsg = $"Invalid block number.\n{_messageProvidor.Block}\n" +
                            ConfigHelper.GetBlockListInText(_config, "BlockNumbers");

                        response = BotToolSet.BuildResponseMessage(dialog, invalidMsg, _messageProvidor.SupportFooter);
                    }
                    break;

                // Unit Phase
                case 2:
                    dialog.Unit = _message;
                    response = BotToolSet.BuildResponseMessage(dialog,
                        _messageProvidor.Category, _messageProvidor.SupportFooter);
                    dialog.ChatPhase = 3;
                    break;

                // Fault category phase
                case 3:
                    int categoryValue;
                    if (int.TryParse(_message, out categoryValue)
                        && categoryValue > 0 && categoryValue < 7)
                    {
                        dialog.ChatPhase = 4;
                        dialog.Category = _repo.GetCategoryById(categoryValue);
                        response = BotToolSet.BuildResponseMessage(dialog,
                            _messageProvidor.Description, _messageProvidor.SupportFooter);
                    }
                    else
                    {
                        var invalidMsg = $"Invalid category.\n{_messageProvidor.Category}";
                        response = BotToolSet.BuildResponseMessage(dialog,
                            invalidMsg, _messageProvidor.SupportFooter);
                    }
                    break;

                // Description phase
                case 4:
                    dialog.Description = _message;
                    response = BotToolSet.BuildResponseMessage(dialog,
                            _messageProvidor.Confirm, _messageProvidor.SupportFooter);
                    dialog.ChatPhase = 5;
                    break;

                // Confirmation phase
                case 5:
                    if (int.TryParse(_message, out int confirmOption)
                        && confirmOption >= 1 && confirmOption <= 2)
                    {
                        dialog.Confirmation = confirmOption;

                        if (confirmOption == 1)
                        {
                            response = $"{_messageProvidor.Final}\n\n{_messageProvidor.InitialMessage}";
                            var report = BotToolSet.ExtractReportData(dialog, message);
                            dialog.FaultReports.Add(report);
                            supportSubmitted = true;
                        }
                        else
                        {
                            dialog.Reset(true);
                            response += "Your session has been reset.\n\n" + _messageProvidor.Block +
                                ConfigHelper.GetBlockListInText(_config, "BlockNumbers");
                        }
                    }
                    else
                        response += BotToolSet.BuildResponseMessage(dialog, $"Invalid input.\n{_messageProvidor.Confirm}",
                            _messageProvidor.SupportFooter);
                    break;
            }

            dialog.LastActive = DateTime.Now;
            _repo.UpdateDialogState(dialog);

            return (response, supportSubmitted);
        }
    }
}
