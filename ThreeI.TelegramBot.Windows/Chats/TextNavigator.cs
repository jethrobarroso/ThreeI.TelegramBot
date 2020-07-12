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

        public override (string reponse, bool supportSubmitted) ProcessMessage(DialogState dialogState, Message message)
        {
            var response = string.Empty;
            var supportSubmitted = false;
            var timeDiff = DateTime.Now.Subtract(dialogState.LastActive);

            // Reset the dialog if the user enters /start or /reset depending 
            // depending on the support mode
            if (_message == "/reset" || _message == "/start")
            {
                if (dialogState.IsSupportMode)
                {
                    dialogState.Reset(true);
                    _repo.UpdateDialogState(dialogState);

                    return ($"Your session has been reset.\n\n{_messageProvidor.Block}\n\n" +
                        $"{ConfigHelper.GetBlockListInText(_config, "BlockNumber")}", false);
                }
                else
                {
                    dialogState.Reset(false);
                    _repo.UpdateDialogState(dialogState);
                    return (_messageProvidor.InitialMessage, false);
                }
            }

            if (!dialogState.IsSupportMode && _message == "/support")
            {
                dialogState.IsSupportMode = true;
                _repo.UpdateDialogState(dialogState);
                return (_messageProvidor.Block + ConfigHelper.GetBlockListInText(_config, "BlockNumbers"), false);
            }

            if (!dialogState.IsSupportMode && _message != "/support")
                return (_messageProvidor.SupportModeNotActive, false);

            if (timeDiff.TotalMinutes > double.Parse(_config["UserSessionExpireTime"]))
            {
                dialogState.LastActive = DateTime.Now;
                response = "The session has expired due to inactivity. " +
                    "The support process has reset to phase 1.\n\n" +
                    _messageProvidor.InitialMessage;
                dialogState.Reset(false);
                _repo.UpdateDialogState(dialogState);
                return (response, false);
            }

            switch (dialogState.ChatPhase)
            {
                // Block phase
                case 1:
                    if (ConfigHelper.GetBlockCollection(_config, "BlockNumbers").Contains(_message))
                    {
                        dialogState.Block = _message;
                        dialogState.ChatPhase = 2;
                        response += _messageProvidor.Unit;
                    }
                    else
                    {
                        response += "Invalid block numbers. Availble blocks are:\n\n" +
                            ConfigHelper.GetBlockListInText(_config, "BlockNumbers");
                    }
                    break;

                // Unit Phase
                case 2:
                    dialogState.Unit = _message;
                    dialogState.ChatPhase = 3;
                    response += _messageProvidor.Category;
                    break;

                // Fault category phase
                case 3:
                    int categoryValue;
                    if (int.TryParse(_message, out categoryValue)
                        && categoryValue > 0 && categoryValue < 7)
                    {
                        dialogState.Category = _repo.GetCategoryById(categoryValue);
                        dialogState.ChatPhase = 4;
                        response += _messageProvidor.Description;
                    }
                    else
                        response = _messageProvidor.BadInput;
                    break;

                // Description phase
                case 4:
                    dialogState.Description = _message;
                    dialogState.ChatPhase = 5;
                    response += _messageProvidor.Confirm;
                    break;

                // Confirmation phase
                case 5:
                    if (int.TryParse(_message, out int confirmOption)
                        && confirmOption >= 1 && confirmOption <= 2)
                    {
                        dialogState.Confirmation = confirmOption;

                        if (confirmOption == 1)
                        {
                            response = $"{_messageProvidor.Final}\n\n{_messageProvidor.InitialMessage}";
                            var report = BotToolSet.ExtractReportData(dialogState, message);
                            dialogState.FaultReports.Add(report);
                            supportSubmitted = true;
                        }
                        else
                        {
                            dialogState.Reset(true);
                            response += "Your session has been reset.\n\n" + _messageProvidor.Block +
                                ConfigHelper.GetBlockListInText(_config, "BlockNumbers");
                        }
                    }
                    else
                        response += "Invalid input. Please enter 1 to confirm, or 2 to restart the process";
                    break;
            }

            dialogState.LastActive = DateTime.Now;

            _repo.UpdateDialogState(dialogState);

            return (response, supportSubmitted);
        }
    }
}
