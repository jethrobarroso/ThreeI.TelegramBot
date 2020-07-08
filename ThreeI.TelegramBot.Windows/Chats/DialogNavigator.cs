using System;
using System.Collections.Generic;
using System.Text;
using ThreeI.TelegramBot.Core;
using ThreeI.TelegramBot.Data;

namespace ThreeI.TelegramBot.Windows.Chats
{
    public abstract class DialogNavigator
    {
        protected readonly string _message;
        protected readonly IDataRepository _repo;
        protected readonly Beneficiary _beneficiary;
        protected readonly IMessageProvidor _messageProvidor;

        public DialogNavigator(string message, IDataRepository repo, Beneficiary beneficiary, IMessageProvidor messageProvidor)
        {
            _message = message;
            _repo = repo;
            _beneficiary = beneficiary;
            _messageProvidor = messageProvidor;
        }

        public abstract string ProcessMessage();
    }
}
