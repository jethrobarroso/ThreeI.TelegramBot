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
        protected readonly IMessageProvidor _messageProvidor;
        protected readonly DialogState _dialogSate;

        public DialogNavigator(string message, IDataRepository repo, IMessageProvidor messageProvidor)
        {
            _message = message;
            _repo = repo;
            _messageProvidor = messageProvidor;
        }

        public abstract string ProcessValidUser(string userId);

        public string ValidateUser(string userId)
        {
            return null;
        }
    }
}
