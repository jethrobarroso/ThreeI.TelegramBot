using System;
using System.Collections.Generic;
using System.Text;
using ThreeI.TelegramBot.Core;
using ThreeI.TelegramBot.Data;

namespace ThreeI.TelegramBot.Windows.Chats
{
    public class CallbackNavigator : DialogNavigator
    {
        public CallbackNavigator(string message, IDataRepository repo, Beneficiary beneficiary, IMessageProvidor messageProvidor) 
            : base(message, repo, beneficiary, messageProvidor)
        {

        }

        public override string ProcessMessage()
        {
            var result = string.Empty;
            switch (_beneficiary.Conversation.ChatPhase)
            {
                case 0:
                    result = _messageProvidor.Block;
                    _beneficiary.Conversation.Block = _message;
                    _beneficiary.Conversation.ChatPhase = 1;
                    _repo.UpdateBeneficiary(_beneficiary);
                    break;
                case 1:
                    result = _messageProvidor.Unit;
                    _beneficiary.Conversation.Unit = _message;
                    _beneficiary.Conversation.ChatPhase = 2;
                    _repo.UpdateBeneficiary(_beneficiary);
                    break;
                case 2:
                    int value;
                    if(int.TryParse(_message, out value) && value > 0 && value < 3)
                    {
                        result = _messageProvidor.Option;
                        _beneficiary.Conversation.Option = value;
                        _beneficiary.Conversation.ChatPhase = 3;
                        _repo.UpdateBeneficiary(_beneficiary);
                    }
                    break;
            }

            return result;
        }
    }
}
