using System;
using System.Collections.Generic;
using System.Text;
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

        public string GetResult(string inputMessage, DialogState dialog, 
            IEnumerable<IMessageProcessor> processors)
        {
            string message = string.Empty;
            foreach(var processor in processors)
            {
                var result = processor.Step(inputMessage, dialog);

                if (result.StepHit)
                {
                    message = result.Response;
                    dialog.LastActive = DateTime.Now;

                    if (result.RequestSubmitted)
                        dialog.Reset(false);

                    _repo.UpdateDialogState(dialog);
                    break;
                }
            }

            return message;
        }
    }
}
