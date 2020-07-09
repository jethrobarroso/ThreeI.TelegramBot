using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeI.TelegramBot.Core;
using ThreeI.TelegramBot.Data;

namespace ThreeI.TelegramBot.Windows.Chats
{
    public class CallbackNavigator : DialogNavigator
    {
        public CallbackNavigator(string message, IDataRepository repo, IMessageProvidor messageProvidor) 
            : base(message, repo, messageProvidor)
        {

        }

        public override string ProcessValidUser(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
