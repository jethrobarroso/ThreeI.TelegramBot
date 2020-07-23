using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using ThreeI.TelegramBot.Core;
using ThreeI.TelegramBot.Data;

namespace ThreeI.TelegramBot.Windows.Dialog
{
    public class DialogAggregator
    {
        private readonly IDataRepository _repo;

        public DialogAggregator(IDataRepository repo)
        {
            _repo = repo;
        }

        public virtual DialogState ValidateUserFromMessage(string userId, Message message)
        {
            var dialog = ValidateUser(userId);
            if(dialog != null)
            {
                dialog.FirstName = message.From.FirstName;
                dialog.LastName = message.From.LastName;
            }
            return dialog;
        }

        public virtual DialogState ValidateUserFromCallback(string userId, CallbackQuery callback)
        {
            var dialog = ValidateUser(userId);
            if (dialog != null)
            {
                dialog.FirstName = callback.From.FirstName;
                dialog.LastName = callback.From.LastName;
            }
            return dialog;
        }

        private DialogState ValidateUser(string userId)
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

        public IMessageProcessor GetResult(string inputMessage, DialogState dialog, 
            IEnumerable<IMessageProcessor> processors)
        {
            IMessageProcessor result = null; 
            foreach(var processor in processors)
            {
                result = processor.Step(inputMessage, dialog);

                if (result.StepHit)
                {
                    dialog.LastActive = DateTime.Now;

                    if (result.RequestSubmitted)
                        dialog.Reset(false);

                    _repo.UpdateDialogState(dialog);
                    break;
                }
            }

            return result;
        }
    }
}
