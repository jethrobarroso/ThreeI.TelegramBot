using System;
using System.Collections.Generic;
using System.Text;
using ThreeI.TelegramBot.Core;
using ThreeI.TelegramBot.Data;

namespace ThreeI.TelegramBot.Windows.Chats
{
    public class TextNavigator : DialogNavigator
    {
        public TextNavigator(string message, IDataRepository repo,Beneficiary beneficiary, IMessageProvidor messageProvidor) 
            : base(message, repo, beneficiary, messageProvidor)
        {

        }

        public override string ProcessMessage()
        {
            int optionValue;
            int confirmOption;
            var result = string.Empty;

            switch (_beneficiary.Conversation.ChatPhase)
            {
                case 0:
                    _beneficiary.Conversation.Block = _message;
                    _beneficiary.Conversation.ChatPhase = 1;
                    result = _messageProvidor.Unit;
                    break;
                case 1:
                    _beneficiary.Conversation.Unit = _message;
                    _beneficiary.Conversation.ChatPhase = 2;
                    result = _messageProvidor.Option;
                    break;
                case 2:

                    if (int.TryParse(_message, out optionValue)
                        && optionValue > 0 && optionValue < 7)
                    {
                        _beneficiary.Conversation.Option = optionValue;
                        _beneficiary.Conversation.ChatPhase = 3;
                        result = _messageProvidor.Description;
                    }
                    else
                        result = _messageProvidor.BadInput;
                    break;
                case 4:
                    _beneficiary.Conversation.Description = _message;
                    _beneficiary.Conversation.ChatPhase = 4;
                    result = _messageProvidor.Confirm;
                    break;
                case 5:
                    if (int.TryParse(_message, out confirmOption)
                        && confirmOption > 0 && confirmOption < 3)
                    {
                        _beneficiary.Conversation.Confirmation = confirmOption;
                        _beneficiary.Conversation.ChatPhase = 5;
                        if (confirmOption == 1)
                            result = _messageProvidor.Final;
                        else
                            _beneficiary.Conversation.Reset();
                    }
                    break;
            }

            if(_beneficiary != null)
            {
                if(_beneficiary.Conversation.ChatPhase == 5)
                {
                    _repo.UpdateBeneficiary(_beneficiary);
                    _beneficiary.Conversation.Reset();
                }
                else
                {
                    _repo.UpdateBeneficiary(_beneficiary);
                }
            }

            return result;
        }
    }
}
