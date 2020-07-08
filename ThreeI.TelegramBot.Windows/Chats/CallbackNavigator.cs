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
            //switch (_beneficiary.Conversation.ChatPhase)
            //{
            //    case 0:
            //        result = _messageProvidor.MessageBlock;
            //        _beneficiary.Conversation.Block = _message;
            //        _beneficiary.Conversation.ChatPhase = 1;
            //        _repo.UpdateBeneficiary(_beneficiary);
            //        break;
            //    case 1:
            //        result = _messageProvidor.MessageUnit;
                    
            //        break;
            //    case 2:

            //}

            return result;
        }
    }
}
