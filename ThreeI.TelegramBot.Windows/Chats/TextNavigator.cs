using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public override string ProcessValidUser(DialogState dialogState)
        {
            int categoryValue;
            int confirmOption;
            var result = string.Empty;
            var timeDiff = DateTime.Now.Subtract(dialogState.LastActive);

            // Reset the dialog if the user enters /start or /reset depending 
            // depending on the support mode
            if (_message == "/reset" || _message == "/start")
            {
                if (dialogState.IsSupportMode)
                {
                    dialogState.Reset(true);
                    _repo.UpdateDialogState(dialogState);
                    return $"Your session has been reset.\n\n{_messageProvidor.Block}\n\n" +
                        $"{ConfigHelper.GetBlockListInText(_config,"BlockNumber")}";
                }
                else
                {
                    dialogState.Reset(false);
                    _repo.UpdateDialogState(dialogState);
                    return _messageProvidor.InitialMessage;
                }
            }

            if (!dialogState.IsSupportMode && _message == "/support")
            {
                dialogState.IsSupportMode = true;
                _repo.UpdateDialogState(dialogState);
                return _messageProvidor.Block + ConfigHelper.GetBlockListInText(_config, "BlockNumbers");
            }

            if (!dialogState.IsSupportMode && _message != "/support")
                return _messageProvidor.SupportModeNotActive;

            if (timeDiff.TotalMinutes > double.Parse(_config["UserSessionExpireTime"]))
            {
                dialogState.LastActive = DateTime.Now;
                result = "The session has expired due to inactivity. " +
                    "The support process has reset to phase 1.\n\n" + 
                    _messageProvidor.InitialMessage;
                dialogState.Reset(false);
                _repo.UpdateDialogState(dialogState);
                return result;
            }

            switch (dialogState.ChatPhase)
            {
                // Block phase
                case 1:
                    if (ConfigHelper.GetBlockCollection(_config, "BlockNumbers").Contains(_message))
                    {
                        dialogState.Block = _message;
                        dialogState.ChatPhase = 2;
                        result += _messageProvidor.Unit;
                    }
                    else
                    {
                        result += "Invalid block numbers. Availble blocks are:\n\n" +
                            ConfigHelper.GetBlockListInText(_config, "BlockNumbers");
                    }
                    break;

                // Unit Phase
                case 2:
                    dialogState.Unit = _message;
                    dialogState.ChatPhase = 3;
                    result += _messageProvidor.Category;
                    break;

                // Fault category phase
                case 3:
                    if (int.TryParse(_message, out categoryValue)
                        && categoryValue > 0 && categoryValue < 7)
                    {
                        dialogState.Category = categoryValue;
                        dialogState.ChatPhase = 4;
                        result += _messageProvidor.Description;
                    }
                    else
                        result = _messageProvidor.BadInput;
                    break;

                // Description phase
                case 4:
                    dialogState.Description = _message;
                    dialogState.ChatPhase = 5;
                    result += _messageProvidor.Confirm;
                    break;

                // Confirmation phase
                case 5:
                    if (int.TryParse(_message, out confirmOption)
                        && confirmOption >= 1 && confirmOption <= 2)
                    {
                        dialogState.Confirmation = confirmOption;

                        if (confirmOption == 1)
                        {
                            result = $"{_messageProvidor.Final}\n\n{_messageProvidor.InitialMessage}";
                            dialogState.Reset(false);
                        }
                        else
                        {
                            dialogState.Reset(true);
                            result += "Your session has been reset.\n\n" + _messageProvidor.Block +
                                ConfigHelper.GetBlockListInText(_config, "BlockNumbers");
                        }
                    }
                    else
                        result += "Invalid input. Please enter 1 to confirm, or 2 to restart the process";
                    break;
            }

            dialogState.LastActive = DateTime.Now;

            _repo.UpdateDialogState(dialogState);

            return result;
        }
    }
}
