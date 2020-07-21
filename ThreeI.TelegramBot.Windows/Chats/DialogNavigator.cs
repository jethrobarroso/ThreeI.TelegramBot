using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using Telegram.Bot.Types;
using ThreeI.TelegramBot.Core;
using ThreeI.TelegramBot.Data;

namespace ThreeI.TelegramBot.Windows.Chats
{
    public abstract class DialogNavigator
    {
        protected readonly string _message;
        protected readonly IDataRepository _repo;
        protected readonly IMessageProvidor _messageProvidor;
        protected readonly IConfiguration _config;

        public DialogNavigator(string message, IDataRepository repo, IMessageProvidor messageProvidor, IConfiguration config)
        {
            _message = message;
            _repo = repo;
            _messageProvidor = messageProvidor;
            _config = config;
        }

        public abstract (string reponse, bool supportSubmitted) ProcessMessage(DialogState dialog, Message message);

        public virtual DialogState ValidateUser(string userId)
        {
            DialogState dialog;

            try
            {
                dialog = _repo.GetDialogStateById(userId);

                if (dialog == null)
                {
                    dialog = new DialogState() { UserId = userId };
                    dialog.Reset(false);
                    dialog.LastActive = DateTime.Now;
                    _repo.AddDialogState(dialog);
                }
                return dialog;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.Message);
                throw;
            }
        }
    }
}
