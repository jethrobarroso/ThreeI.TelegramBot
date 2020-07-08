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
            throw new NotImplementedException();
        }
    }
}
