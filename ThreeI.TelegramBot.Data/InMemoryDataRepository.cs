using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot.Types;
using ThreeI.TelegramBot.Core;

namespace ThreeI.TelegramBot.Data
{
    public class InMemoryDataRepository : IDataRepository
    {
        private List<DialogState> _dialogs = new List<DialogState>();
        private List<FaultReport> _reports = new List<FaultReport>();

        public InMemoryDataRepository()
        {
            InitialiseDialogStates();
        }

        public DialogState AddDialogState(DialogState dialogState)
        {
            _dialogs.Add(dialogState);
            return dialogState;
        }

        public FaultReport AddReport(FaultReport fault)
        {
            _reports.Add(fault);
            return fault;
        }

        public DialogState GetDialogStateById(string userId)
        {
            return _dialogs.FirstOrDefault(d => d.UserId == userId);
        }

        public DialogState UpdateDialogState(DialogState dialog)
        {
            var workingDialog = _dialogs.Where(i => i.UserId == dialog.UserId).FirstOrDefault();
            workingDialog = dialog;
            return dialog;
        }

        private void InitialiseDialogStates()
        {
            _dialogs.Add(new DialogState()
            {
                UserId = "111",
                Block = "1404",
                Unit = "1",
                Description = "Water broken",
                Category = 3,
                ChatPhase = 5,
                Confirmation = 1
            });

            _dialogs.Add(new DialogState()
            {
                UserId = "222",
                Block = "1404",
                Unit = "2",
                Category = 3,
                Description = null,
                Confirmation = 0,
                ChatPhase = 3
            });

            _dialogs.Add(new DialogState());


        }
    }
}