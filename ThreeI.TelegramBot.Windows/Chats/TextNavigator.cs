using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeI.TelegramBot.Core;
using ThreeI.TelegramBot.Data;

namespace ThreeI.TelegramBot.Windows.Chats
{
    public class TextNavigator : DialogNavigator
    {
        public TextNavigator(string message, IDataRepository repo, IMessageProvidor messageProvidor)
            : base(message, repo, messageProvidor)
        {

        }

        public override string ProcessValidUser(string userId)
        {
            int optionValue;
            int confirmOption;
            var result = string.Empty;

            switch (_dialogSate.ChatPhase)
            {
                case 0:
                    _dialogSate.Block = _message;
                    _dialogSate.ChatPhase = 1;
                    result = _messageProvidor.Unit;
                    break;
                case 1:
                    _dialogSate.Unit = _message;
                    _dialogSate.ChatPhase = 2;
                    result = _messageProvidor.Option;
                    break;
                case 2:
                    if (int.TryParse(_message, out optionValue)
                        && optionValue > 0 && optionValue < 7)
                    {
                        _dialogSate.Option = optionValue;
                        _dialogSate.ChatPhase = 3;
                        result = _messageProvidor.Description;
                    }
                    else
                        result = _messageProvidor.BadInput;
                    break;
                case 4:
                    _dialogSate.Description = _message;
                    _dialogSate.ChatPhase = 4;
                    result = _messageProvidor.Confirm;
                    break;
                case 5:
                    if (int.TryParse(_message, out confirmOption)
                        && confirmOption > 0 && confirmOption < 3)
                    {
                        _dialogSate.Confirmation = confirmOption;
                        _dialogSate.ChatPhase = 5;
                        if (confirmOption == 1)
                            result = _messageProvidor.Final;
                        else
                            _dialogSate.Reset();
                    }
                    break;
            }

            if (_dialogSate.ChatPhase == 5)
            {
                _repo.UpdateDialog(_dialogSate);
                _dialogSate.Reset();
            }
            else
            {
                _repo.UpdateDialog(_dialogSate);
            }


            return result;
        }
    }
}
