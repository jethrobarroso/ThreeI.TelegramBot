﻿using Microsoft.Extensions.Configuration;
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
        public CallbackNavigator(string message, IDataRepository repo, IMessageProvidor messageProvidor, IConfiguration config) 
            : base(message, repo, messageProvidor, config)
        {

        }

        public override string ProcessValidUser(DialogState dialogState)
        {
            throw new NotImplementedException();
        }
    }
}
